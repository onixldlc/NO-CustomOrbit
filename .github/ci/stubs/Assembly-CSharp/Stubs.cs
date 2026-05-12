// =============================================================================
// AUTO-GENERATED STUB by csstubgen — DO NOT EDIT
// =============================================================================

using UnityEngine;

public abstract class CameraBaseState
{
    public abstract void EnterState(CameraStateManager cam);
    public abstract void FixedUpdateState(CameraStateManager cam);
    public abstract void LeaveState(CameraStateManager cam);
    public abstract void UpdateState(CameraStateManager cam);
}

public abstract class SceneSingleton<T> : MonoBehaviour
    where T : SceneSingleton<T>
{
}

public class CameraOrbitState : CameraBaseState
{
    public override void EnterState(CameraStateManager cam) { }
    public override void FixedUpdateState(CameraStateManager cam) { }
    public override void LeaveState(CameraStateManager cam) { }
    public override void UpdateState(CameraStateManager cam) { }
}

public class CameraStateManager : SceneSingleton<CameraStateManager>
{
    public Transform cameraPivot;
}

