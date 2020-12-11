using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class VrHelper : MonoBehaviour
{
    private static List<XRDisplaySubsystem> displays = new List<XRDisplaySubsystem>();

    public static void SetEnabled(bool isEnabled)
    {
        displays.Clear();
        SubsystemManager.GetInstances(displays);

        foreach(XRDisplaySubsystem system in displays)
        {
            if (isEnabled)
            {
                system.Start();
            }
            else
            {
                system.Stop();
            }
        }
    }

    public static bool IsEnabled()
    {
        displays.Clear();
        SubsystemManager.GetInstances(displays);

        foreach(XRDisplaySubsystem system in displays)
        {
            if (system.running)
            {
                return true;
            }
        }

        return false;
    }
}
