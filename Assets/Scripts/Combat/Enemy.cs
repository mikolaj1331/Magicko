using System.Collections;
using System.Collections.Generic;
using Magicko.Control;
using UnityEngine;

namespace Magicko.Combat
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] Transform target;
        [SerializeField] float detectRange = 10;
        
        private void Start() 
        {
            target = FindObjectOfType<PlayerController>().transform;
        }

        private void Update() 
        {
            if(Vector3.Distance(transform.position, target.position) <= detectRange)
            {
                GetComponent<CombatHandler>().Attack(target);
            }    
        }
    }
}
