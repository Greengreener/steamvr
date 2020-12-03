using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR;

public class Pointer : MonoBehaviour
{
    [System.Serializable]
    public class TeleportEvent : UnityEvent<Vector3> { }
    ///<summary>
    ///The last position was hitting, used for teleportaiopn
    ///</summary>
    public Vector3 Position { get; private set; } = Vector3.zero;

    [SerializeField]
    SteamVR_Input_Sources source;
    [SerializeField]
    LayerMask pointerLayers;
    [SerializeField]
    float maxPointerLength = 100f;

    [Header("Rendering")]
    [SerializeField]
    GameObject tracer;
    [SerializeField]
    bool stretchTracerAlongRay = true;
    [SerializeField]
    float tracerScaleFactor = 0.01f;
    [SerializeField]
    GameObject cursor;
    [SerializeField]
    float cursorScaleFactor = 0.25f;

    [Space]
    public TeleportEvent onTeleportRequested = new TeleportEvent();

    VrControllerInput input;
    bool isPointerActive = false;

    private void Start()
    {
        if (source == SteamVR_Input_Sources.LeftHand)
        {
            input = VrRig.instance.LeftController.Input;
        }
        if (source == SteamVR_Input_Sources.RightHand)
        {
            input = VrRig.instance.RightController.Input;
        }
        else
        {
            input = VrRig.instance.LeftController.Input;
        }
        input.onPointer.AddListener(OnPointerActivate);
        input.onUnpointer.AddListener(OnPointerDeactivate);
        input.onTeleport.AddListener(OnTeleportPressed);

        tracer.transform.parent = transform;
        cursor.transform.parent = transform;
    }
    private void Update()
    {
        transform.rotation = input.transform.rotation;
        transform.position = input.transform.position;
        if (isPointerActive)
        {
            if (Physics.Raycast(input.transform.position, input.transform.forward, out RaycastHit hit, maxPointerLength, pointerLayers))
            {
                Position = hit.point;

                Vector3 midpoint = Vector3.Lerp(transform.position, hit.point, 0.5f);
                tracer.transform.position = midpoint;
                tracer.transform.rotation = Quaternion.LookRotation(transform.forward);
                tracer.transform.localScale = new Vector3(tracerScaleFactor, tracerScaleFactor, hit.distance);

                cursor.transform.position = hit.point;
                cursor.transform.localScale = Vector3.one * cursorScaleFactor;
            }
            else
            {
                Position = Vector3.zero;

                tracer.transform.position = transform.position + transform.forward * (maxPointerLength * 0.5f);
                tracer.transform.rotation = Quaternion.LookRotation(transform.forward);
                tracer.transform.localScale = new Vector3(tracerScaleFactor, tracerScaleFactor, maxPointerLength);

                cursor.transform.position = transform.position + transform.forward * maxPointerLength;
                cursor.transform.localScale = Vector3.one * cursorScaleFactor;
            }
        }
    }
    void OnPointerActivate(InputEventArgs _args)
    {
        isPointerActive = true;
        tracer.SetActive(true);
        cursor.SetActive(true);
    }
    void OnPointerDeactivate(InputEventArgs _args)
    {
        isPointerActive = false;
        tracer.SetActive(false);
        cursor.SetActive(false);
    }

    private void OnTeleportPressed(InputEventArgs _args)
    {
        if (isPointerActive)
        {
            onTeleportRequested.Invoke(Position);
        }
    }
}
