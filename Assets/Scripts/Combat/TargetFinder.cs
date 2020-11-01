using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Magicko.Combat
{
    public class TargetFinder : MonoBehaviour
    {
        [SerializeField] Enemy[] enemiesInRange;
        [SerializeField] float detectRange;

        public Transform target;

        private void Update()
        {
            SetTarget();
        }

        private void SetTarget()
        {
            // Find all object that include component "Enemy"
            enemiesInRange = FindObjectsOfType<Enemy>();
            if (enemiesInRange.Length == 0) return;

            // Sets the first enemy in the list as the closest enemy
            Transform closestEnemyPosition = enemiesInRange[0].transform;
            foreach (Enemy enemy in enemiesInRange)
            {
                // Checks if any of the enemies in the list is closer than the one selected at the start
                closestEnemyPosition = FindClosestEnemy(closestEnemyPosition, enemy.transform);
            }
            // Sets the closest enemy as the target
            if(Vector3.Distance(transform.position,closestEnemyPosition.position) <= detectRange)
                target = closestEnemyPosition;
        }

        Transform FindClosestEnemy(Transform transformA, Transform transformB)
        {
            // Calculates distance from gameObject to the enemies passed in the method arguments
            float distanceToEnemyA = Vector3.Distance(transformA.position, transform.position);
            float distanceToEnemyB = Vector3.Distance(transformB.position, transform.position);

            // Checks which of the two passed enemies is closer to the gameObject and returns it
            if(distanceToEnemyA <= distanceToEnemyB)
                return transformA;
            else
                return transformB;
        }
    } 
}
