using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Magicko.Core
{
    public class LockedCamera : MonoBehaviour
    {
        [SerializeField] Transform target;

        // Sets the position of the gameObject directly where the target is
        void Awake() => transform.position = target.position;

        // Responsible for following the target along the z axis
        void Update() => transform.position = new Vector3(transform.position.x, transform.position.y, target.position.z);
    }
}