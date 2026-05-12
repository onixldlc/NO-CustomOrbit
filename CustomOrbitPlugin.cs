using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;

namespace CustomOrbit
{
    [BepInPlugin(MyGUID, PluginName, VersionString)]
    public class CustomOrbit : BaseUnityPlugin
    {
        // Mod identification
        private const string MyGUID = "com.onixldlc.customorbit";
        private const string PluginName = "CustomOrbit";
        private const string VersionString = "1.0.0";

        public static ManualLogSource Log { get; private set; }

        // Config entries
        private const string CurrentConfigVersion = VersionString;
        public static ConfigEntry<bool> Enabled { get; set; }
        public static ConfigEntry<float> OrbitAnchorOffsetX { get; set; }
        public static ConfigEntry<float> OrbitAnchorOffsetY { get; set; }
        public static ConfigEntry<float> OrbitAnchorOffsetZ { get; set; }
        public static ConfigEntry<float> OrbitDistance { get; set; }

        private void Awake()
        {
            Log = Logger;
            Logger.LogInfo($"{PluginName} v{VersionString} is loading...");

            var savedVersion = Config.Bind(
                section: "Internal",
                key: "ConfigVersion",
                defaultValue: "",
                "Do not touch - used for auto-updating config."
            ).Value;

            Enabled = Config.Bind(
                section: "General",
                key: "Enabled",
                defaultValue: true,
                configDescription: new ConfigDescription(
                    "Whether the mod is active."
                )
            );

            OrbitAnchorOffsetX = Config.Bind(
                section: "OrbitCamera",
                key: "AnchorOffsetX",
                defaultValue: 0f,
                configDescription: new ConfigDescription(
                    "Orbit anchor X offset (left/right)."
                )
            );

            OrbitAnchorOffsetY = Config.Bind(
                section: "OrbitCamera",
                key: "AnchorOffsetY",
                defaultValue: 2.2f,
                configDescription: new ConfigDescription(
                    "Orbit anchor Y offset (up/down)."
                )
            );

            OrbitAnchorOffsetZ = Config.Bind(
                section: "OrbitCamera",
                key: "AnchorOffsetZ",
                defaultValue: 2f,
                configDescription: new ConfigDescription(
                    "Orbit anchor Z offset (forward/back). Negative = behind."
                )
            );

            OrbitDistance = Config.Bind(
                section: "OrbitCamera",
                key: "OrbitDistance",
                defaultValue: 0.7f,
                configDescription: new ConfigDescription(
                    "Distance from orbit anchor to camera."
                )
            );

            if (savedVersion != CurrentConfigVersion)
            {
                Logger.LogInfo($"Config version changed ({savedVersion} -> {CurrentConfigVersion}). Updating config.");

                var savedVersionConfig = Config.Bind(
                    section: "Internal",
                    key: "ConfigVersion",
                    defaultValue: CurrentConfigVersion,
                    configDescription: new ConfigDescription(
                        "Do not touch - used for auto-updating config."
                    )
                );

                savedVersionConfig.Value = CurrentConfigVersion;

                Config.Save();

                Logger.LogInfo("Config automatically updated.");
            }

            var harmony = new Harmony(MyGUID);
            harmony.PatchAll();

            Logger.LogInfo($"{PluginName} v{VersionString} loaded successfully. State: {(Enabled.Value ? "Enabled" : "Disabled")}");
        }
    }

    internal static class OrbitState
    {
        internal static bool isActive;
        internal static Vector3 originalPivotPosition;
    }

    [HarmonyPatch(typeof(CameraOrbitState), nameof(CameraOrbitState.EnterState))]
    internal static class CameraOrbitState_EnterState_Patch
    {
        static void Postfix(CameraOrbitState __instance, CameraStateManager cam)
        {
            if (!CustomOrbit.Enabled.Value)
                return;

            OrbitState.originalPivotPosition = cam.cameraPivot.localPosition;
            OrbitState.isActive = true;

            Traverse.Create(__instance).Field("viewDistAdjust").SetValue(CustomOrbit.OrbitDistance.Value);
        }
    }

    [HarmonyPatch(typeof(CameraOrbitState), nameof(CameraOrbitState.UpdateState))]
    internal static class CameraOrbitState_UpdateState_Patch
    {
        static void Postfix(CameraOrbitState __instance, CameraStateManager cam)
        {
            if (!CustomOrbit.Enabled.Value || !OrbitState.isActive)
                return;

            var offset = new Vector3(
                CustomOrbit.OrbitAnchorOffsetX.Value,
                CustomOrbit.OrbitAnchorOffsetY.Value,
                CustomOrbit.OrbitAnchorOffsetZ.Value
            );

            cam.cameraPivot.localPosition = offset;
        }
    }

    [HarmonyPatch(typeof(CameraOrbitState), nameof(CameraOrbitState.LeaveState))]
    internal static class CameraOrbitState_LeaveState_Patch
    {
        static void Prefix(CameraOrbitState __instance, CameraStateManager cam)
        {
            if (!OrbitState.isActive)
                return;

            cam.cameraPivot.localPosition = OrbitState.originalPivotPosition;
            OrbitState.isActive = false;
        }
    }
}
