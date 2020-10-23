using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Magicko.Core
{
    public class LockedCamera : MonoBehaviour
    {
        [SerializeField] Transform target;

        void Awake() => transform.position = target.position;

        void Update() => transform.position = new Vector3(transform.position.x, transform.position.y, target.position.z);
    }
}