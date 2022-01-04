using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float suspicionTime = 2f;
        [SerializeField] PatrolPath patrolPath;
        [SerializeField] float waypointTolerance = 1f;

        Fighter fighter;
        
        GameObject player;
        Health health;
        Mover mover;

        Vector3 guardPosition;
        float timeSinceLastSawPlayer = Mathf.Infinity;
        int currentWaypointIndex = 0;

        void Start()
        {
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
            mover = GetComponent<Mover>();
            player = GameObject.FindWithTag("Player");

            guardPosition = transform.position;
        }

        void Update()
        {
            if (health.IsDead()) { return; }

            if (InAttackRangeOfPlayer() && fighter.CanAttack(player))
            {
                timeSinceLastSawPlayer = 0;
                AttackBehaviour();
            }
            else if (timeSinceLastSawPlayer < suspicionTime)
            {
                SuspicionBehaviour();
            }
            else
            {
                PatrolBehaviour();
            }

            timeSinceLastSawPlayer += Time.deltaTime;
        }
        
        void AttackBehaviour()
        {
            fighter.Attack(player);
        }

        void SuspicionBehaviour()
        {
            mover.Cancel();
        }

        void GuardBehaviour()
        {
            mover.StartMoveAction(guardPosition);
        }

        private void PatrolBehaviour()
        {
            Vector3 nextPosition = guardPosition;

            if (patrolPath != null)
            {
                if (AtWaypoint(nextPosition))
                {
                    CycleWaypoint(nextPosition);
                }
                nextPosition = GetCurrentWaypoint();
            }

            mover.StartMoveAction(nextPosition);
        }

        private bool AtWaypoint(Vector3 nextPosition)
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
            return distanceToWaypoint < waypointTolerance;
        }

        private void CycleWaypoint(Vector3 nextPosition)
        {
            currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
        }

        private Vector3 GetCurrentWaypoint()
        {
            return patrolPath.GetWaypoint(currentWaypointIndex);
        }

        private bool InAttackRangeOfPlayer()
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            return distanceToPlayer < chaseDistance;
        }

        // Called by Unity
        void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}