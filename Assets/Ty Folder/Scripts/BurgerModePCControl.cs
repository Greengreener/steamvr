using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurgerModePCControl : MonoBehaviour
{
    private bool burgerMode = false;
    public bool BurgerMode { get { return burgerMode; } }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            burgerMode = true;
        }
        else
        {
            burgerMode = false;
        }
    }
}
