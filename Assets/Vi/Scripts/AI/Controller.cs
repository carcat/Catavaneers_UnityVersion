using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using FiniteStateMachine.StatePolymorphism;
using AI.PatrolPath;
using AI.States;

namespace AI
{
    public enum AIState
    {
        None,
        Patrol,
        Chase,
        Attack,
        Investigate
    }

    public class Controller : MonoBehaviour
    {
        [Header("Chase Settings")]
        [SerializeField] private float chaseSpeed = 0;

        [Header("Attack Settings")]
        [SerializeField] private int attackDamage = 0;
        [SerializeField] private float attackRange = 0;
        [SerializeField] private float attackInterval = 0;

        private FSM finiteStateMachine = null;
        private NavMeshAgent agent = null;
        private List<Transform> targets;
        private Transform currentTarget = null;
        private float distanceToTarget = Mathf.Infinity;

        public NavMeshAgent Agent { get { return agent; } }
        public float ChaseSpeed { get { return chaseSpeed; } }
        public int AttackDamage { get { return attackDamage; } }
        public float AttackRange { get { return attackRange; } }
        public float AttackInterval { get { return attackInterval; } }

        void Start()
        {
            currentTarget = FindClosestTarget();
        }

        void Update()
        {
            finiteStateMachine.Update(Time.deltaTime);

            // update current target if it's null and still has targets
            if (!currentTarget && targets.Count > 0)
            {
                currentTarget = FindClosestTarget();
            }
        }

        private void Init()
        {
            // add states
            finiteStateMachine.AddState("Chase", new Patrol(this));
            finiteStateMachine.AddState("Attack", new Attack(this));

            // add transitions
            finiteStateMachine.AddTransition("Chase", "Attack", ChaseToAttackCondition);
            finiteStateMachine.AddTransition("Attack", "Chase", AttackToChaseCondition);
        }

        private bool ChaseToAttackCondition()
        {
            return currentTarget && distanceToTarget <= attackRange;
        }

        private bool AttackToChaseCondition()
        {
            return currentTarget && distanceToTarget > attackRange;
        }

        private Transform FindClosestTarget()
        {
            Transform closestTarget = targets[0];
            float closestDistance = Vector3.Distance(transform.position, targets[0].position);

            for (int i = 1; i < targets.Count; i++)
            {
                float distanceToTarget = Vector3.Distance(transform.position, targets[i].position);

                if (distanceToTarget < closestDistance)
                {
                    closestTarget = targets[i];
                    closestDistance = distanceToTarget;
                }
            }

            return closestTarget;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }
    }
}