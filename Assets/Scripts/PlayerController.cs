using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeedMultiplier;
    //[SerializeField] float moveSpeed;

    void Update()
    {
        // Getting input values
        float xPosInput = CrossPlatformInputManager.GetAxis("Horizontal");
        float zPosInput = CrossPlatformInputManager.GetAxis("Vertical");

        GetComponent<NavMeshAgent>().destination = new Vector3(transform.position.x + xPosInput, transform.position.y, transform.position.z + zPosInput);
    }    
}
