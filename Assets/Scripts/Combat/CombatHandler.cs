using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Magicko.Movement;
using Magicko.Core;

namespace Magicko.Combat
{
    public class CombatHandler : MonoBehaviour, IAction
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
                //GetComponent<MovementHandler>().MoveTowards(target.position);
                float distanceToTarget = Vector3.Distance(transform.position,target.position);
                if(distanceToTarget < weaponRange)
                {
                    GetComponent<MovementHandler>().CancelAction();
                    AttackState();
                }
            }   
        }

        private void AttackState()
        {
            if(timeBetweenAttacks >= attackCooldown)
            {
                GetComponent<Animator>().SetTrigger("attack");
                timeBetweenAttacks = 0;
            }
        }

        public void Attack(Transform targetPosition)
        {
            GetComponent<ActionsManager>().BeginAction(this);
            target = targetPosition;
            gameObject.transform.LookAt(new Vector3(target.position.x,transform.position.y,target.position.z));
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
