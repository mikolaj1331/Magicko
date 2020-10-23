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
        MovePlayer();
        UpdateAnimator();
    }

    private void MovePlayer()
    {
        // Getting input values
        float xPosInput = CrossPlatformInputManager.GetAxis("Horizontal");
        float zPosInput = CrossPlatformInputManager.GetAxis("Vertical");
        //Moving the character
        GetComponent<NavMeshAgent>().destination = new Vector3(transform.position.x + xPosInput, transform.position.y, transform.position.z + zPosInput);
    }

    void UpdateAnimator()
    {
        //Getting velocity values
        Vector3 playerVelocity = GetComponent<NavMeshAgent>().velocity;
        Vector3 localPlayerVelocity = transform.InverseTransformDirection(playerVelocity);

        //Setting a movement speed variable for animator tree blend
        float moveSpeed = localPlayerVelocity.z;
        GetComponent<Animator>().SetFloat("forwardSpeed",moveSpeed);
    }
}
