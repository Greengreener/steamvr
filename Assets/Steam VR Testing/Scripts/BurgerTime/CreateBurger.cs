using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class CreateBurger : MonoBehaviour
{
    [SerializeField] VrController controller;

    void Start()
    {
        controller = GetComponent<VrController>();
    }
}
