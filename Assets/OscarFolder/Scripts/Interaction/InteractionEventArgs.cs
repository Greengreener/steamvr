using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[System.Serializable]
public class InteractionEvent : UnityEvent<InteractionEventArgs>{}
[System.Serializable]
public class InteractionEventArgs
{
    ///<summary>
    /// the conroller that is sending the event associated with these args
    ///</summary>
    public VrController controller;
    ///<summary>
    /// the rigidbody of the object that is being interacted with
    ///</summary>
    public Rigidbody rigidbody;
    ///<summary>
    ///the collider of the object being interacted with
    ///</summary>
    public Collider collider;

    public InteractionEventArgs(VrController _controller, Rigidbody _rigidbody, Collider _collider)
    {
        controller = _controller;
        rigidbody = _rigidbody;
        collider = _collider;
    }
}
