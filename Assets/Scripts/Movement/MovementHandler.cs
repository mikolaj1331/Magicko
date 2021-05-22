using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using Magicko.Core;

namespace Magicko.Movement
{
    public class MovementHandler : MonoBehaviour, IAction
    {
        NavMeshAgent navigationAgent;
        HealthManager healthManager;

        private void Start() 
        {
            navigationAgent = GetComponent<NavMeshAgent>();
            healthManager = GetComponent<HealthManager>();
        }

        private void Update() 
        {
            navigationAgent.enabled = !healthManager.IsDead;
            UpdateAnimator();    
        }

        void UpdateAnimator()
        {
            //Getting velocity values
            Vector3 characterVelocity = navigationAgent.velocity;
            Vector3 localCharacterVelocity = transform.InverseTransformDirection(characterVelocity);

            //Setting a movement speed variable for animator tree blend
            float moveSpeed = localCharacterVelocity.z;
            GetComponent<Animator>().SetFloat("forwardSpeed", moveSpeed);
        }

        public void MoveTowards(Vector3 destination)
        {
            // Sets the destination of navigation agent as the one passed in the method argument
            navigationAgent.destination = destination;
            navigationAgent.isStopped = false;
        }

        public void MoveAction(Vector3 destination)
        {
            // Sets this action as active
            GetComponent<ActionsManager>().BeginAction(this);
            MoveTowards(destination);
        }

        public void CancelAction()
        {
            navigationAgent.isStopped = true;
        }
    }
}

//TODO: (SOLVED) NavMeshAgent rotates the player upon arrival at the destination
//Solution: The navigator couldn't reach the exact value of float variable due to specifics of this variable,
//increased stopping distance parameter to stop the navigator from looking for perfect position
