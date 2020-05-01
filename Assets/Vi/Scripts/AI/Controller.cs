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

        private FSM finiteStateMachine = new FSM();
        private float distanceToTarget = Mathf.Infinity;
        private NavMeshAgent agent = null;
        private static List<HealthComp> targets = new List<HealthComp>();
        private Transform currentTarget = null;

        public float DistanceToTarget { get { return distanceToTarget; } }
        public NavMeshAgent Agent { get { return agent; } }
        public static List<HealthComp> Targets { get { return targets; } }
        public Transform CurrentTarget { get { return currentTarget; } }
        public float ChaseSpeed { get { return chaseSpeed; } }
        public int AttackDamage { get { return attackDamage; } }
        public float AttackRange { get { return attackRange; } }
        public float AttackInterval { get { return attackInterval; } }

        void Start()
        {
            if (targets.Count == 0)
                targets = GetTargets();

            agent = GetComponent<NavMeshAgent>();

            InitFSM();
        }

        void Update()
        {
            finiteStateMachine.Update(Time.deltaTime);

            if (currentTarget)
            {
                distanceToTarget = Vector3.Distance(transform.position, currentTarget.position);
            }
        }

        private void InitFSM()
        {
            // add states
            finiteStateMachine.AddState("Chase", new Chase(this));
            finiteStateMachine.AddState("Attack", new Attack(this));
            finiteStateMachine.AddState("Find Target", new FindTarget(this));

            // add transitions
            finiteStateMachine.AddTransition("Chase", "Attack", ChaseToAttackCondition);
            finiteStateMachine.AddTransition("Chase", "Find Target", ChaseToFindTargetCondition);
            finiteStateMachine.AddTransition("Attack", "Chase", AttackToChaseCondition);
            finiteStateMachine.AddTransition("Attack", "Find Target", AttackToFindTargetCondition);
            finiteStateMachine.AddTransition("Find Target", "Chase", FindTargetToChaseCondition);
            finiteStateMachine.AddTransition("Find Target", "Attack", FindTargetToAttackCondition);
        }

        #region TRANSITIONS
        private bool ChaseToAttackCondition()
        {
            return currentTarget && distanceToTarget <= attackRange;
        }

        private bool ChaseToFindTargetCondition()
        {
            return !currentTarget;
        }

        private bool AttackToChaseCondition()
        {
            return currentTarget && distanceToTarget > attackRange;
        }

        private bool AttackToFindTargetCondition()
        {
            return !currentTarget;
        }

        private bool FindTargetToChaseCondition()
        {
            return currentTarget && distanceToTarget > attackRange;
        }

        private bool FindTargetToAttackCondition()
        {
            return currentTarget && distanceToTarget <= attackRange;
        }
        #endregion

        private List<HealthComp> GetTargets()
        {
            List<HealthComp> targets = new List<HealthComp>();
            HealthComp[] healthComps = FindObjectsOfType<HealthComp>();

            for (int i = 0; i < healthComps.Length; i++)
            {
                if (healthComps[i].myClass == CharacterClass.Player || healthComps[i].myClass == CharacterClass.Caravan)
                {
                    targets.Add(healthComps[i]);
                }
            }

            return targets;
        }

        public void SetCurrentTarget(Transform target)
        {
            currentTarget = target;
        }

        public static void AddToTargetList(HealthComp target)
        {
            if (!targets.Contains(target))
                targets.Add(target);
        }

        public static void RemoveFromTargetList(HealthComp target)
        {
            if (targets.Contains(target))
                targets.Remove(target);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }
    }
}