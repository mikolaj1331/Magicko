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
        float distance = cameraSpeed * Time.deltaTime;
        Vector3 target_position_z = new Vector3(transform.position.x, transform.position.y, target.position.z);
        transform.position = Vector3.MoveTowards(transform.position, target_position_z, distance);
    }
}
