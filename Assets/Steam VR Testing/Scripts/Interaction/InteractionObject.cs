using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Valve.VR;
[RequireComponent(typeof(Rigidbody))]
public class InteractionObject : MonoBehaviour
{
public Rigidbody Rigidbody {get{return rigidbody;}}
public Collider Collider {get {return collider;}}
public Transform AttachPoint{get{return attachPoint;}}

    [SerializeField]
    bool isGrabbable = true;
    [SerializeField]
    bool isTouchable;
    [SerializeField]
    bool isUsable;
    [SerializeField]
    SteamVR_Input_Sources allowedSource = SteamVR_Input_Sources.Any;

[Space]

[SerializeField]
Transform attachPoint;

[Space]

    public InteractionEvent onGrabbed = new InteractionEvent();
    public InteractionEvent onUnGrabbed = new InteractionEvent();

    public InteractionEvent onTouched = new InteractionEvent();
    public InteractionEvent onUnTouch = new InteractionEvent();

    public InteractionEvent onUsed = new InteractionEvent();
    public InteractionEvent onUnused = new InteractionEvent();

    new Collider collider;
    new Rigidbody rigidbody;
//this = (aBool)?yes:no;
    InteractionEventArgs GenerateArgs(VrController _controller){
        return new InteractionEventArgs(_controller, rigidbody,collider);

    }
    void Start() {
        collider = gameObject.GetComponent<Collider>();
        if(collider == null){
            collider = gameObject.AddComponent<BoxCollider>();
            Debug.LogError(string.Format("Object: {0} does not have a collider, adding a BoxCollider",name));
        }    
        rigidbody = gameObject.GetComponent<Rigidbody>();
    }
    public void OnObjectGrabbed(VrController _controller){
        if(isGrabbable && _controller.InputSource == allowedSource ||allowedSource == SteamVR_Input_Sources.Any){
            onGrabbed.Invoke(GenerateArgs(_controller));
        }
    }
    public void OnObjectUngrabbed(VrController _controller){
        if(isGrabbable && _controller.InputSource == allowedSource ||allowedSource == SteamVR_Input_Sources.Any){
            onGrabbed.Invoke(GenerateArgs(_controller));
        }
    }



    public void OnObjectUsed(VrController _controller){
        if(isUsable && _controller.InputSource == allowedSource ||allowedSource == SteamVR_Input_Sources.Any){
            onGrabbed.Invoke(GenerateArgs(_controller));
        }
    }
    public void OnObjectUnused(VrController _controller){
        if(isUsable && _controller.InputSource == allowedSource ||allowedSource == SteamVR_Input_Sources.Any){
            onGrabbed.Invoke(GenerateArgs(_controller));
        }
    }

    public void OnObjectTouched(VrController _controller){
        if(isTouchable && _controller.InputSource == allowedSource ||allowedSource == SteamVR_Input_Sources.Any){
            onGrabbed.Invoke(GenerateArgs(_controller));
        }
    }
    public void OnObjectUntouch(VrController _controller){
        if(isTouchable && _controller.InputSource == allowedSource ||allowedSource == SteamVR_Input_Sources.Any){
            onGrabbed.Invoke(GenerateArgs(_controller));
        }
    }
    
}
