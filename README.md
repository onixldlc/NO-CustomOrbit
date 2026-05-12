# CustomOrbit for Nuclear Option

**A client-side mod that customizes the Orbit camera anchor position in Nuclear Option.**

![Game Banner or Screenshot](preview.mp4) 

>![info]
>ignore the hud thats from [ThirdPersonHUD](https://github.com/OverlordMIke/Nuclear-Option-Mod-ThirdPersonHUD) do check that mod out if you are planing on using this to get that war lighting cam vibe

## About

This mod lets you adjust the **Orbit camera** anchor point so the external view sits exactly where you want it — closer, further, higher, or offset to the side.

By default it repositions the orbit camera to a chase-style anchor with configurable X/Y/Z offsets and distance, giving you a more cinematic third-person feel without switching to Chase mode.

Once installed, the mod is **enabled by default**. You can tweak all values via the auto-generated config file.

## Features

- **Custom orbit anchor position** — Move the orbit camera pivot point with per-axis X/Y/Z offsets.
- **Configurable distance** — Set your preferred orbit distance from the anchor point.
- **Hot-configurable** — All settings are in the BepInEx config file and take effect without restarting.

## Configuration

After first launch, a config file is generated at:
```
BepInEx/config/com.gnol.customorbit.cfg
```

| Setting | Default | Description |
|---------|---------|-------------|
| `Enabled` | `true` | Master toggle for the mod |
| `AnchorOffsetX` | `0` | Left/right offset |
| `AnchorOffsetY` | `2.2` | Up/down offset |
| `AnchorOffsetZ` | `2` | Forward/back offset (negative = behind) |
| `OrbitDistance` | `10` | Distance from anchor to camera |

## Requirements

- [Nuclear Option](https://store.steampowered.com/app/2168680/Nuclear_Option/) (Steam)
- [BepInEx](https://github.com/BepInEx/BepInEx/releases) (latest pack for Unity Mono games — usually drop the BepInEx folder into your game directory)

## Installation

1. Install **BepInEx** if you haven't already:
   - Download the appropriate pack from the [BepInEx GitHub releases](https://github.com/BepInEx/BepInEx/releases).
   - Extract it so `BepInEx` folder is directly inside your Nuclear Option install directory (e.g. `C:\Program Files (x86)\Steam\steamapps\common\Nuclear Option\`).
   - Launch the game once — BepInEx will generate its folders.

2. Download the latest release of this mod from the [Releases page](https://github.com/YourGitHubUsername/CustomOrbit/releases).

3. Extract the contents of the zip file into the `BepInEx/plugins` folder.

## Disclaimer

> This is a **Client-side mod.**
> not sure how this would get you banned... since all it does is to allow you to modify the center of the orbit position. but tbf this thing is still a mod and not vanila so **at your own risk**
> oh yeah... if this code base looked alot like [ThirdPersonHUD](https://github.com/OverlordMIke/Nuclear-Option-Mod-ThirdPersonHUD) that's because i basically uses his project as a steping stone to build this as this is actually my first time making a mod for unity game, and you should use the ThirdPersonHUD as this will make it look alot more like war... lightning... camera + hud