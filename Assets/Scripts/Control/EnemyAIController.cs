using Magicko.Combat;
using Magicko.Core;
using Magicko.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Magicko.Control
{
    public class EnemyAIController : MonoBehaviour
    {
        [SerializeField] float detectRange = 10f;
        [SerializeField] float suspicionThreshold = 5f;

        CombatHandler combatant;
        GameObject playerTarget;
        HealthManager healthManager;
        MovementHandler mover;

        Vector3 guardingPosition;
        float suspicionTimer = Mathf.Infinity;
        

        private void Start()
        {
            combatant = GetComponent<CombatHandler>();
            playerTarget = GameObject.FindWithTag("Player");
            healthManager = GetComponent<HealthManager>();
            mover = GetComponent<MovementHandler>();

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
                suspicionTimer = 0;
                AttackBehaviour();
            }
            else if(suspicionTimer < suspicionThreshold)
            {
                SuspicionBehaviour();
            }
            else
            {
                ReturningBehaviour();
            }

            suspicionTimer += Time.deltaTime;
        }

        private void ReturningBehaviour()
        {
            mover.MoveAction(new Vector3(guardingPosition.x, transform.position.y, guardingPosition.z));
        }

        private void SuspicionBehaviour()
        {
            mover.CancelAction();
            GetComponent<ActionsManager>().CancelCurrentAction();
        }

        private void AttackBehaviour()
        {
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
