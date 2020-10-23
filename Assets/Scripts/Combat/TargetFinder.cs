using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Magicko.Combat
{
    public class TargetFinder : MonoBehaviour
    {
        public Transform target;
        [SerializeField] Enemy[] enemiesInRange;

        private void Update()
        {
            SetTarget();
        }

        private void SetTarget()
        {
            enemiesInRange = FindObjectsOfType<Enemy>();
            if (enemiesInRange.Length == 0) return;

            Transform closestEnemyPosition = enemiesInRange[0].transform;
            foreach (Enemy enemy in enemiesInRange)
            {
                closestEnemyPosition = FindClosestEnemy(closestEnemyPosition, enemy.transform);
            }
            target = closestEnemyPosition;
        }

        Transform FindClosestEnemy(Transform transformA, Transform transformB)
        {
            float distanceToEnemyA = Vector3.Distance(transformA.position, transform.position);
            float distanceToEnemyB = Vector3.Distance(transformB.position, transform.position);

            if(distanceToEnemyA <= distanceToEnemyB)
                return transformA;
            else
                return transformB;
        }
    } 
}
