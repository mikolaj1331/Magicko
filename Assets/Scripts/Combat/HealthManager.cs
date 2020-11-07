using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Magicko.Movement;
using UnityEngine.AI;

namespace Magicko.Combat
{
    public class HealthManager : MonoBehaviour
    {
        [SerializeField] float hitPoints = 100f;

        public void TakeDamage(float damage)
        {
            hitPoints -= damage;

            if(hitPoints <= 0)
                StartDeathSequence();
        }

        private void StartDeathSequence()
        {
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<CapsuleCollider>().enabled = false;
            GetComponent<MovementHandler>().enabled = false;
            GetComponent<CombatHandler>().enabled = false;
            GetComponent<NavMeshAgent>().enabled = false;
            Destroy(GetComponent<Rigidbody>());
            Destroy(GetComponent<Enemy>());
        }
    }
}
