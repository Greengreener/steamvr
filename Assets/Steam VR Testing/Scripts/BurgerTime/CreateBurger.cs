using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class CreateBurger : MonoBehaviour
{
    [SerializeField] VrControllerInput input;
    [SerializeField] GameObject burgerPrefab;
    [SerializeField] GameObject hand;
    void Start()
    {
        input = GetComponent<VrControllerInput>();
        input.onUse.AddListener(OnInteractPressed);
        hand = this.gameObject;
    }

    private void OnInteractPressed(InputEventArgs _args)
    {
        if (_args.source == SteamVR_Input_Sources.LeftHand)
        {
            Instantiate(burgerPrefab, hand.transform.position, Quaternion.identity);
        }
    }
}
