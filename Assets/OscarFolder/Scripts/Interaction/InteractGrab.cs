using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(VrControllerInput))]
public class InteractGrab : MonoBehaviour
{
    VrControllerInput input;

    public InteractionEvent grabbed = new InteractionEvent();
    public InteractionEvent ungrabbed = new InteractionEvent();

InteractionObject collidingObject;
InteractionObject heldObject;

    private void Start() {
        input = gameObject.GetComponent<VrControllerInput>();

        input.onGrabbed.AddListener(OnGrabPressed);
        input.onUngrabbed.AddListener(OnGrabbedUnpressed);
    }
    void OnGrabPressed(InputEventArgs _arg){
        if(collidingObject != null){
            GrabObject();
        }
    }

    void OnGrabbedUnpressed(InputEventArgs _args){
        if(heldObject != null){
            UngrabObject();
        }
    }
    void SetCollidingObject(Collider _collider){
        InteractionObject interactable = _collider.GetComponent<InteractionObject>();

        if(collidingObject != null || interactable == null){
            return;
        }
        collidingObject = interactable;
    }
    void OnTriggerEnter(Collider other) {
    SetCollidingObject(other);    
    }
    private void OnTriggerExit(Collider other) {
        if(collidingObject == other.GetComponent<InteractionObject>()){
            collidingObject = null;
        }
    }
    void GrabObject(){
        heldObject = collidingObject;
        collidingObject = null;
        if(heldObject.AttachPoint != null){
            heldObject.transform.position = transform.position - (heldObject.AttachPoint.position - heldObject.transform.position);
            heldObject.transform.rotation = transform.rotation * Quaternion.Euler(heldObject.AttachPoint.localEulerAngles);
        }
        else{
            heldObject.transform.position = transform.position;
        }
        FixedJoint joint = AddJoint();
        joint.connectedBody = heldObject.Rigidbody;
        grabbed.Invoke(new InteractionEventArgs(input.Controller, heldObject.Rigidbody,heldObject.Collider));
        heldObject.OnObjectGrabbed(input.Controller);
    }
    void UngrabObject(){
        FixedJoint joint = gameObject.GetComponent<FixedJoint>();
        if (joint!= null){
            joint.connectedBody = null;
            Destroy(joint);
            
            heldObject.Rigidbody.velocity = input.Controller.Velocity;
            heldObject.Rigidbody.angularVelocity = input.Controller.AngularVelocity;
        }
        ungrabbed.Invoke(new InteractionEventArgs(input.Controller,heldObject.Rigidbody,heldObject.Collider));
        heldObject.OnObjectUngrabbed(input.Controller);
        heldObject = null;
    }
    FixedJoint AddJoint(){
        FixedJoint fx = gameObject.AddComponent<FixedJoint>();
        fx.breakForce = 20000;
        fx.breakTorque = 20000;
        return fx;
    }
}
