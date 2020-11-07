using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Magicko.Combat
{
    public class RangeAttackHandler : MonoBehaviour
    {
        [SerializeField] ProjectileHandler projectilePrefab;

        public void InstantiateProjectile(Vector3 startingPos, Quaternion rotation)
        {
            var projectile = Instantiate(projectilePrefab,startingPos,rotation);
        }
    }
}
