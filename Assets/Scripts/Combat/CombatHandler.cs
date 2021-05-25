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
        [SerializeField] bool isMelee;

        [SerializeField] HealthManager target;
        float timeBetweenAttacks = Mathf.Infinity;
        Animator animator;

        void Start()
        {
            animator = GetComponent<Animator>();
        }

        private void Update() 
        {
            // Each frame increases the delay since last attack
            timeBetweenAttacks += Time.deltaTime;
            if(target != null && !target.GetComponent<HealthManager>().IsDead)
            {
                if(isMelee)
                // If gameObject is set to melee it will automaticly move withing range of the target
                    GetComponent<MovementHandler>().MoveTowards(target.transform.position);

                // Calculates the distance between gameObject.transform.position and target.position
                float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);

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
                // Rotates the gameobject towards the target
                gameObject.transform.LookAt(new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z));

                // Triggers the attack state in character animator
                StartAttack();

                // Resets the attack delay
                timeBetweenAttacks = 0;
            }
        }

        private void StartAttack()
        {
            animator.fireEvents = true;
            animator.ResetTrigger("stopAttack");
            animator.SetTrigger("attack");
        }

        public void Attack(GameObject targetToAttack)
        {
            // Sets this action as active
            GetComponent<ActionsManager>().BeginAction(this);
            target = targetToAttack.GetComponent<HealthManager>();
        }

        //Animation event
        void Hit()
        {
            if(!isMelee)
            {
                transform.LookAt(new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z));
                Vector3 offset = new Vector3(transform.forward.x + 0.1f, 1, transform.forward.z + 0.1f);
                GetComponent<RangeAttackHandler>().InstantiateProjectile(transform.position + offset, transform.rotation);
            }
            else
            {
                if (target == null) return;
                target.GetComponent<HealthManager>().TakeDamage(25);
            }
        }

        public bool isTargetable(GameObject target)
        {
            if (target == null) return false;
            HealthManager health = target.GetComponent<HealthManager>();
            return health != null && !health.IsDead;
        }
        
        public void CancelAction()
        {
            CancelAttack();
            target = null;
        }

        private void CancelAttack()
        {
            animator.fireEvents = false;
            animator.ResetTrigger("attack");
            animator.SetTrigger("stopAttack");
        }
    }
}
