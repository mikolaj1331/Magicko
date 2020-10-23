using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Magicko.Movement;

namespace Magicko.Combat
{
    public class CombatHandler : MonoBehaviour
    {
        [SerializeField] float weaponRange = 20f;
        [SerializeField] float attackCooldown = 1f;

        Transform target;
        float timeBetweenAttacks = 0;

        private void Update() 
        {
            timeBetweenAttacks += Time.deltaTime;
            if(target != null)
            {
                GetComponent<MovementHandler>().MoveTowards(target.position);
                float distanceToTarget = Vector3.Distance(transform.position,target.position);
                if(distanceToTarget < weaponRange)
                {
                    AttackState();
                }
            }   
        }

        private void AttackState()
        {
            if(timeBetweenAttacks >= attackCooldown)
            {
                GetComponent<Animator>().SetTrigger("attack");
            }
        }

        public void Attack(Transform targetPosition)
        {
            target = targetPosition;
            //gameObject.transform.LookAt(target);
        }

        //Animation event
        void Hit()
        {

        }
    }
}
