using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    [SerializeField] VrControllerInput input;

    Rigidbody rb;

    float speed = 500;
    float jumpSpeed = 5f;
    public Vector3 moveDirection;

    [SerializeField] GameObject VrCamera;

    public bool VR;

    private void Start()
    {
        if (VR)
        {
            input = GetComponent<VrControllerInput>();
            input.onTouchpadAxisChanged.AddListener(OnTouchAxisPressed);
        }
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        float horizontal = 0;
        float vertical = 0;
        switch (VR)
        {
            case false:
                if (Input.GetKey(KeyCode.W))
                {
                    vertical++;
                }
                if (Input.GetKey(KeyCode.S))
                {
                    vertical--;
                }
                if (Input.GetKey(KeyCode.D))
                {
                    horizontal++;
                }
                if (Input.GetKey(KeyCode.A))
                {
                    horizontal--;
                }
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    rb.AddRelativeForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
                }
                moveDirection = new Vector3(horizontal, 0, vertical);
                rb.AddRelativeForce(moveDirection * speed * Time.deltaTime, ForceMode.Force);
                Mathf.Clamp(rb.velocity.x, 0, 0.5f);
                Mathf.Clamp(rb.velocity.y, 0, 0.5f);
                break;
            case true:
                gameObject.transform.rotation = VrCamera.transform.rotation;
                Mathf.Clamp(rb.velocity.x, 0, 0.5f);
                Mathf.Clamp(rb.velocity.y, 0, 0.5f);
                break;
        }
    }

    private void OnTouchAxisPressed(InputEventArgs _args)
    {
        if (_args.source == SteamVR_Input_Sources.LeftHand)
        {
            Vector3 camForward = VrCamera.transform.forward;
            Vector3 camRight = VrCamera.transform.right;

            rb.AddRelativeForce(((camForward * _args.touchpadAxis.y) + (camRight * _args.touchpadAxis.x)) * speed * Time.deltaTime, ForceMode.Force);
        }
    }
}