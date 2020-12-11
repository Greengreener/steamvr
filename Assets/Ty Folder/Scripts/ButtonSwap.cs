﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonSwap : MonoBehaviour
{
    public GameObject PCGlobal;
    public GameObject PCChild;
    public GameObject VRGlobal;
    public GameObject VRChild;
    public static ControlType globalControlType = ControlType.PCControl;

    private void Start()
    {
        if (globalControlType == ControlType.PCControl && PCGlobal)
        {
            PCGlobal.SetActive(true);
        }
        else
        {
            VRGlobal.SetActive(true);
        }
    }

    public void SetControlState(int setting)
    {
        switch (setting)
        {
            default:
                globalControlType = ControlType.PCControl;
                break;
            case 1:
                globalControlType = ControlType.VRControl;
                break;
        }
    }

    public void LoadScene(int ID)
    {
        SceneManager.LoadScene(ID);
    }
}

public enum ControlType
{
    VRControl,
    PCControl
}