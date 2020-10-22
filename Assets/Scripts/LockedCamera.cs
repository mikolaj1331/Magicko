using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedCamera : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float cameraSpeed;

    void Awake()
    {
        transform.position = target.position;
    }
    
    void Update()
    {
        transform.position = new Vector3(transform.position.x,transform.position.y,target.position.z);
    }
}
