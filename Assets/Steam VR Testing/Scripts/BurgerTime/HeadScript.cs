using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadScript : MonoBehaviour
{
    [SerializeField]
    GameObject rightHand;
    [SerializeField]
    GameObject leftHand;

    [SerializeField]
    bool burgerTime;
    public bool BurgerTime { get { return burgerTime; } }

    bool rightHandIn;
    bool leftHandIn;

    void Update()
    {
        if (rightHandIn && leftHandIn)
        {
            burgerTime = true;
        }
        if (!rightHandIn || !leftHandIn)
        {
            burgerTime = false;
        }
    }
    private void OnTriggerEnter(Collider hand)
    {
        if (hand.gameObject == rightHand)
        {
            rightHandIn = true;
        }
        if (hand.gameObject == leftHand)
        {
            leftHandIn = true;
        }
    }
    private void OnTriggerExit(Collider hand)
    {

        if (hand.gameObject == rightHand)
        {
            rightHandIn = false;
        }
        if (hand.gameObject == leftHand)
        {
            leftHandIn = false;
        }
    }
}
