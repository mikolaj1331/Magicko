using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    Camera mainCamera;

    private void Awake()
    {
        mainCamera = FindObjectOfType<Camera>();
    }

    void LateUpdate()
    {
        transform.LookAt(transform.position + mainCamera.transform.forward);
    }
}
