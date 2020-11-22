using Magicko.Combat;
using Magicko.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Magicko.Control
{
    public class EnemyAIController : MonoBehaviour
    {
        [SerializeField] float detectRange = 10f;

        CombatHandler combatant;
        GameObject playerTarget;
        HealthManager healthManager;

        private void Start()
        {
            combatant = GetComponent<CombatHandler>();
            playerTarget = GameObject.FindWithTag("Player");
            healthManager = GetComponent<HealthManager>();
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
                combatant.Attack(playerTarget);
            }
            else
            {
                combatant.CancelAction();
            }
        }

        private bool InRange(GameObject target)
        {
            float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
            return distanceToTarget <= detectRange;
        }
    }
}
