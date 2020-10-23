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
            HandleMovement();
            HandleCombat();
        }

        private void HandleMovement()
        {
            // Getting input values
            float xPosInput = CrossPlatformInputManager.GetAxis("Horizontal");
            float zPosInput = CrossPlatformInputManager.GetAxis("Vertical");
            //Moving the character
            GetComponent<MovementHandler>().MoveTowards(new Vector3(transform.position.x + xPosInput, transform.position.y, transform.position.z + zPosInput));
        }

        private void HandleCombat()
        {
            var target = GetComponent<TargetFinder>().target;
            GetComponent<CombatHandler>().Attack(target);
        }
    }
}