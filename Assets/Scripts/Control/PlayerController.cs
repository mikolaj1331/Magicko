using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityStandardAssets.CrossPlatformInput;

using Magicko.Movement;
using Magicko.Combat;
using Magicko.Core;

namespace Magicko.Control
{
    public class PlayerController : MonoBehaviour
    {
        HealthManager healthManager;

        private void Start()
        {
            healthManager = GetComponent<HealthManager>();    
        }

        void Update()
        {
            if (healthManager.IsDead) return;
            if(HandleMovement()) return;
            if(HandleCombat()) return;
        }

        private bool HandleMovement()
        {
            // Getting input values
            float xPosInput = CrossPlatformInputManager.GetAxis("Horizontal");
            float zPosInput = CrossPlatformInputManager.GetAxis("Vertical");

            if(xPosInput != 0 || zPosInput != 0)
            {
                //Moving the character
                GetComponent<TargetFinder>().target = null;
                GetComponent<MovementHandler>().MoveAction(new Vector3(transform.position.x + xPosInput, transform.position.y, transform.position.z + zPosInput));
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool HandleCombat()
        {
            // Getting the closest target from TargetFinder component
            var target = GetComponent<TargetFinder>().target;
            if(target != null)
            {
                // Triggers the attack method of CombatHandler component
                GetComponent<CombatHandler>().Attack(target.gameObject);
                return true;
            }
            else
                return false;
        }
    }
}

// (DONE) TODO: Change handle movement and handle combat into bool (implemented action manager that cancels actions which shouldnt happen simultaneously) 
// (DONE) TODO: Player does not rotate towards the target it's shooting at (used transform method lookat() in a way that rotates gameobject towards x and z position of target)