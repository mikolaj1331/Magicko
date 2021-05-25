using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Magicko.Control
{
    [ExecuteInEditMode]
    public class PatrolPath : MonoBehaviour
    {
        [SerializeField] float _waypointRadius = 0.5f;

        public List<Vector3> _waypointsCoords = new List<Vector3>();

        private void OnDrawGizmosSelected()
        {
            //for(int i = 0; i < transform.childCount; i++)
            //{
            //    Vector3 positionOfChild = transform.GetChild(i).transform.position;

            //    Gizmos.DrawWireCube(positionOfChild, new Vector3(1,1,1));
            //}

            for(int i = 0; i < _waypointsCoords.Count; i++)
            {
                Gizmos.DrawWireSphere(GetWaypoint(i), _waypointRadius);
                Gizmos.DrawLine(GetWaypoint(i), GetWaypoint(GetNextIndex(i)));
            }
        }

        public int GetNextIndex(int x)
        {
            //int sum = x + 1;
            //if (sum == _waypointsCoords.Count) sum = 0;
            //return sum;

            return (x + 1) % _waypointsCoords.Count;
        }

        public Vector3 GetWaypoint(int x)
        {
            return _waypointsCoords[x];
        }
    }
}
