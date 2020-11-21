using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

namespace Magicko.Combat
{
    public class ProjectileHandler : MonoBehaviour
    {
        [SerializeField] ParticleSystem particleSystemFx;
        [SerializeField] float projectileSpeed = 0.01f;

        private void Start() 
        {
 
            particleSystemFx.Play();
        }
        private void Update() 
        {
            MoveTowards();
        }

        private void MoveTowards()
        {
            transform.localPosition += transform.forward * projectileSpeed * Time.deltaTime;
        }

        private void OnCollisionEnter(Collision other) 
        {
            Destroy(gameObject, particleSystemFx.main.duration);
        }

        private void OnTriggerEnter(Collider other) 
        {
            Destroy(gameObject, particleSystemFx.main.duration);
            other.GetComponent<HealthManager>().TakeDamage(50);
        }
    }
}