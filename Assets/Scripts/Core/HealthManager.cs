using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Magicko.Core
{
    public class HealthManager : MonoBehaviour
    {
        [SerializeField] float hitPoints = 100f;

        bool isDead = false;

        public bool IsDead { get => isDead; set => isDead = value; }

        public void TakeDamage(float damage)
        {
            hitPoints -= damage;

            if(hitPoints <= 0)
                StartDeathSequence();
        }

        private void StartDeathSequence()
        {
            if (isDead) return;

            isDead = true;
            GetComponent<Animator>().SetTrigger("die");
            Destroy(GetComponent<Rigidbody>());
            GetComponent<CapsuleCollider>().enabled = false;
            GetComponent<ActionsManager>().CancelCurrentAction();
        }
    }
}

//TODO: (SOLVED) After enemy death player does not update their enemies list 
//and doesnt change the target (possible due to Enemy script being active
