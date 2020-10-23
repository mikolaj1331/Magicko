using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Magicko.Movement
{
    public class MovementHandler : MonoBehaviour
    {
        NavMeshAgent navigationAgent;

        private void Start() 
        {
            navigationAgent = GetComponent<NavMeshAgent>();    
        }

        private void Update() 
        {
            UpdateAnimator();    
        }

        void UpdateAnimator()
        {
            //Getting velocity values
            Vector3 playerVelocity = navigationAgent.velocity;
            Vector3 localPlayerVelocity = transform.InverseTransformDirection(playerVelocity);

            //Setting a movement speed variable for animator tree blend
            float moveSpeed = localPlayerVelocity.z;
            GetComponent<Animator>().SetFloat("forwardSpeed", moveSpeed);
        }

        public void MoveTowards(Vector3 destination)
        {
            navigationAgent.destination = destination;
        }
    }
}
