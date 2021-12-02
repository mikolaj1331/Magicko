using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Magicko.UI;

namespace Magicko.Core
{
    public class HealthManager : MonoBehaviour
    {
        [SerializeField] float maxHitPoints = 100f;   
        public HealthBar healthBar;
        float hitPoints;
        bool isDead = false;

        public bool IsDead { get => isDead; set => isDead = value; }

        private void Start()
        {
            hitPoints = maxHitPoints;
            healthBar.SetMaxHealth(maxHitPoints);
        }

        public void TakeDamage(float damage)
        {
            hitPoints -= damage;
            healthBar.SetHealth(hitPoints);

            if(hitPoints <= 0)
                StartDeathSequence();
        }

        private void StartDeathSequence()
        {
            if (isDead) return;

            isDead = true;
            healthBar.gameObject.SetActive(false);
            GetComponent<Animator>().SetTrigger("die");
            Destroy(GetComponent<Rigidbody>());
            GetComponent<CapsuleCollider>().enabled = false;
            GetComponent<ActionsManager>().CancelCurrentAction();
        }
    }
}

//TODO: (SOLVED) After enemy death player does not update their enemies list 
//and doesnt change the target (possible due to Enemy script being active
