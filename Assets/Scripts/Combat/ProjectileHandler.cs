using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

namespace Magicko.Combat
{
    public class ProjectileHandler : MonoBehaviour
    {
        [SerializeField] ParticleSystem particleSystemFx;
        [SerializeField] Transform parent;
        [SerializeField] float projectileSpeed = 0.01f;

        private void Start() 
        {
            Quaternion quat = parent.localRotation;
            quat.eulerAngles -= new Vector3(0,180,0);
            //parent.localRotation
            Instantiate(particleSystemFx,parent.position,quat,parent);  
            particleSystemFx.Play();
        }
        private void Update() 
        {
            MoveTowards();
        }

        private void MoveTowards()
        {
            //transform.position = Vector3.MoveTowards(transform.position,destination,projectileSpeed) * Time.deltaTime;
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

// (SOLVED) TODO: ParticleSystem does not rotate correctly
// (SOLUTION) Correct way was to rotate using euler angles and vector 3 values for rotation and not rotating Quaternion directly