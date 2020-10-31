using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using Magicko.Movement;
using Magicko.Combat;
using System;

namespace Magicko.Control
{
    public class PlayerController : MonoBehaviour
    {
        void Update()
        {
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
                GetComponent<MovementHandler>().MoveAction(new Vector3(transform.position.x + xPosInput, transform.position.y, transform.position.z + zPosInput));
                Debug.Log("Character is moving");
                return true;
            }
            else
            {
                Debug.Log("Character is standing");
                return false;
            }
        }

        private bool HandleCombat()
        {
            var target = GetComponent<TargetFinder>().target;
            if(target != null)
            {
                GetComponent<CombatHandler>().Attack(target);
                return true;
            }
            else
                return false;
        }
    }
}

// (DONE) TODO: Change handle movement and handle combat into bool (implemented action manager that cancels actions which shouldnt happen simultaneously) 
// (DONE) TODO: Player does not rotate towards the target it's shooting at (used transform method lookat() in a way that rotates gameobject towards x and z position of target)