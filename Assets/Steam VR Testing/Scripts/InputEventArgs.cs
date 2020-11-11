using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

[System.Serializable]
public class InputEventArgs 
{   
    /// <summary>
    /// The controller firing the event
    /// </summary>
    public VrController controller;

    /// <summary>
    /// The input source te event is coming from
    /// </summary>
    public SteamVR_Input_Sources source;

    /// <summary>
    /// The position the player is touching on the touch pad
    /// </summary>
    public Vector2 touchpadAxis;

    public InputEventArgs(VrController _controller, SteamVR_Input_Sources _source, Vector2 _touchpadAxis)
    {
        controller = _controller;
        source = _source;
        touchpadAxis = _touchpadAxis;
    }
}
