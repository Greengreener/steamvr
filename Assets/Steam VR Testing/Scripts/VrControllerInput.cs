using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR;

public class VrControllerInput : MonoBehaviour
{
    [System.Serializable]
    public class InputEvent : UnityEvent<InputEventArgs> { }
    public VrController Controller{get{return controller;}}

    [SerializeField]
    SteamVR_Action_Boolean grab;
    [SerializeField]
    SteamVR_Action_Boolean pointer;
    [SerializeField]
    SteamVR_Action_Boolean use;
    [SerializeField]
    SteamVR_Action_Boolean teleport;
    [SerializeField]
    SteamVR_Action_Vector2 touchpadAxis;

    public InputEvent onGrabbed = new InputEvent();
    public InputEvent onUngrabbed = new InputEvent();

    public InputEvent onPointer = new InputEvent();
    public InputEvent onUnpointer = new InputEvent();

    public InputEvent onUse = new InputEvent();
    public InputEvent onUnuse = new InputEvent();

    public InputEvent onTeleport = new InputEvent();
    public InputEvent onUnteleport = new InputEvent();

    public InputEvent onTouchpadAxisChanged = new InputEvent();

    VrController controller;

    public void Setup(VrController _controller)
    {
        controller = _controller;
        
        grab.AddOnStateDownListener(OnGrabDown, controller.InputSource);
        grab.AddOnStateUpListener(OnGrabUp, controller.InputSource);

        pointer.AddOnStateDownListener(OnPointerDown, controller.InputSource);
        pointer.AddOnStateUpListener(OnPointerUp, controller.InputSource);

        use.AddOnStateDownListener(OnUseDown, controller.InputSource);
        use.AddOnStateUpListener(OnUseUp, controller.InputSource);

        teleport.AddOnStateDownListener(OnTeleportDown, controller.InputSource);
        teleport.AddOnStateUpListener(OnTeleportUp, controller.InputSource);

        touchpadAxis.AddOnChangeListener(OnTouchpadAxisChanged, controller.InputSource);
    }
    InputEventArgs GenerateArgs()
    {
        return new InputEventArgs(controller, controller.InputSource, touchpadAxis.axis);
    }
    void OnGrabDown(SteamVR_Action_Boolean _action, SteamVR_Input_Sources _source)
    {
        onGrabbed.Invoke(GenerateArgs());
    }
    void OnGrabUp(SteamVR_Action_Boolean _action, SteamVR_Input_Sources _source)
    {
        onUngrabbed.Invoke(GenerateArgs());
    }
    void OnPointerDown(SteamVR_Action_Boolean _action, SteamVR_Input_Sources _source)
    {
        onPointer.Invoke(GenerateArgs());
    }
    void OnPointerUp(SteamVR_Action_Boolean _action, SteamVR_Input_Sources _source)
    {
        onUnpointer.Invoke(GenerateArgs());
    }
    void OnUseDown(SteamVR_Action_Boolean _action, SteamVR_Input_Sources _source)
    {
        onUse.Invoke(GenerateArgs());
    }
    void OnUseUp(SteamVR_Action_Boolean _action, SteamVR_Input_Sources _source)
    {
        onUnuse.Invoke(GenerateArgs());
    }
    void OnTeleportDown(SteamVR_Action_Boolean _action, SteamVR_Input_Sources _source)
    {
        onTeleport.Invoke(GenerateArgs());
    }
    void OnTeleportUp(SteamVR_Action_Boolean _action, SteamVR_Input_Sources _source)
    {
        onUnteleport.Invoke(GenerateArgs());
    }
    void OnTouchpadAxisChanged(SteamVR_Action_Vector2 _action, SteamVR_Input_Sources _source, Vector2 _axis, Vector2 _delta)
    {
        onTouchpadAxisChanged.Invoke(GenerateArgs());
    }

}
