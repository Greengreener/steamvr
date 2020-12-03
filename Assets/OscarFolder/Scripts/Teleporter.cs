using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Pointer[] pointers = FindObjectsOfType<Pointer>();
        foreach (Pointer pointer in pointers)
        {
            pointer.onTeleportRequested.AddListener(OnTeleportRequest);
        }
    }
    void OnTeleportRequest(Vector3 _position)
    {
        transform.position = _position;
    }
}
