using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Magicko.Movement;
using Magicko.Core;
using UnityEngine.AI;

namespace Magicko.Combat
{
    public class CombatHandler : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 20f;
        [SerializeField] float attackCooldown = 1f;
        [SerializeField] bool isMelee;

        Transform target;
        float timeBetweenAttacks = 0;

        private void Update() 
        {
            // Each frame increases the delay since last attack
            timeBetweenAttacks += Time.deltaTime;
            if(target != null)
            {
                if(isMelee)
                // If gameObject is set to melee it will automaticly move withing range of the target
                    GetComponent<MovementHandler>().MoveTowards(target.position);
                // Calculates the distance between gameObject.transform.position and target.position
                float distanceToTarget = Vector3.Distance(transform.position,target.position);
                // Checks if the target is withing weapon range
                if(distanceToTarget < weaponRange)
                {
                    // Stops movement
                    GetComponent<MovementHandler>().CancelAction();
                    // Starts attacking sequence
                    AttackState();
                }
            }   
        }

        private void AttackState()
        {
            if(timeBetweenAttacks >= attackCooldown)
            {
                // Triggers the attack state in character animator
                GetComponent<Animator>().SetTrigger("attack");
                // Resets the attack delay
                timeBetweenAttacks = 0;
            }
        }

        public void Attack(Transform targetPosition)
        {
            // Sets this action as active
            GetComponent<ActionsManager>().BeginAction(this);
            target = targetPosition;
            //gameObject.transform.LookAt(new Vector3(target.position.x,0,target.position.z));
            FaceTarget();
        }

        void FaceTarget()
        {
            Vector3 directionToFace = (target.position - transform.position).normalized;
            Quaternion lookingRotation = Quaternion.LookRotation(new Vector3(directionToFace.x,0,directionToFace.z));
            float rotationSpeed = GetComponent<NavMeshAgent>().angularSpeed;
            transform.rotation = Quaternion.Slerp(transform.rotation, lookingRotation, rotationSpeed * Time.deltaTime);
        }

        //Animation event
        void Hit()
        {

        }
        
        public void CancelAction()
        {
            target = null;
        }
    }
}
