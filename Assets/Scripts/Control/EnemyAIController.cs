using Magicko.Combat;
using Magicko.Core;
using Magicko.Movement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Magicko.Control
{
    public class EnemyAIController : MonoBehaviour
    {
        [SerializeField] float detectRange = 10f;
        [SerializeField] float suspicionThreshold = 5f;
        [SerializeField] float dwellingThreshold = 2f;
        [SerializeField] float patrollingTolerance = 1f;

        CombatHandler combatant;
        GameObject playerTarget;
        HealthManager healthManager;
        MovementHandler mover;
        PatrolPath pather;

        Vector3 guardingPosition;
        float suspicionTimer = Mathf.Infinity;
        float dwellTimer = Mathf.Infinity;
        int currentPathingIndex = 0;

        private void Start()
        {
            combatant = GetComponent<CombatHandler>();
            playerTarget = GameObject.FindWithTag("Player");
            healthManager = GetComponent<HealthManager>();
            mover = GetComponent<MovementHandler>();
            pather = GetComponent<PatrolPath>();

            guardingPosition = transform.position;
        }

        private void Update()
        {
            if (healthManager.IsDead)
            {
                Destroy(GetComponent<Enemy>());
                return;
            }

            if (InRange(playerTarget) && combatant.isTargetable(playerTarget))
            {
                AttackBehaviour();
            }
            else if (suspicionTimer < suspicionThreshold)
            {
                SuspicionBehaviour();
            }
            else
            {
                PatrollingBehaviour();
            }

            UpdateTimers();
        }

        private void UpdateTimers()
        {
            suspicionTimer += Time.deltaTime;
            dwellTimer += Time.deltaTime;
        }
        private void PatrollingBehaviour()
        {
            Vector3 nextPosition;
            if (pather._waypointsCoords.Count == 0)
                nextPosition = new Vector3(guardingPosition.x, transform.position.y, guardingPosition.z);
            else
            {
                if (AtWaypoint())
                {                    
                    CycleWaypoint();
                    dwellTimer = 0;
                }
                nextPosition = GetCurrentWaypoint();
            }
            if (dwellTimer >= dwellingThreshold)
                mover.MoveAction(nextPosition);
        }
        private Vector3 GetCurrentWaypoint()
        {
            return pather._waypointsCoords[currentPathingIndex];
        }
        private void CycleWaypoint()
        {
            currentPathingIndex = pather.GetNextIndex(currentPathingIndex);
            guardingPosition = pather.GetWaypoint(currentPathingIndex);
        }
        private bool AtWaypoint()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
            return distanceToWaypoint <= patrollingTolerance;
        }
        private void SuspicionBehaviour()
        {
            mover.CancelAction();
            GetComponent<ActionsManager>().CancelCurrentAction();
        }
        private void AttackBehaviour()
        {
            suspicionTimer = 0;
            combatant.Attack(playerTarget);
        }
        private bool InRange(GameObject target)
        {
            float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
            return distanceToTarget <= detectRange;
        }
        // Called by Unity
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, detectRange);           
        }
    }
}
