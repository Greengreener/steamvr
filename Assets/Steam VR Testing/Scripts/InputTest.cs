using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTest : MonoBehaviour
{
    public void OnGrabPressed(InputEventArgs _args)
    {
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);

        go.transform.position = Random.insideUnitSphere * 2f;   
    }
}
