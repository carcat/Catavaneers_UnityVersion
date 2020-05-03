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
        FindTarget,
        Chase,
        Attack,
    }

    public enum EnemyType
    {
        Mouse,
        Cat,
        Dog
    }

    public class Controller : MonoBehaviour
    {
        [Header("Core Settings")]
        [SerializeField] private EnemyType type = EnemyType.Cat;

        [Header("Chase Settings")]
        [SerializeField] private float chaseSpeed = 0;

        [Header("Attack Settings")]
        [SerializeField] private int attackDamage = 0;
        [SerializeField] private float attackRange = 0;
        [SerializeField] private float attackInterval = 0;

        private FSM finiteStateMachine = new FSM();
        private float distanceToTarget = Mathf.Infinity;
        private NavMeshAgent agent = null;
        //private static List<HealthComp> targets = new List<HealthComp>();
        private Transform currentTarget = null;

        public float DistanceToTarget { get { return distanceToTarget; } }
        public NavMeshAgent Agent { get { return agent; } }
        public Transform CurrentTarget { get { return currentTarget; } }
        public float ChaseSpeed { get { return chaseSpeed; } }
        public int AttackDamage { get { return attackDamage; } }
        public float AttackRange { get { return attackRange; } }
        public float AttackInterval { get { return attackInterval; } }
        public EnemyType Type { get { return type; } }

        private static List<HealthComp> mouseTargets = new List<HealthComp>();
        private static List<HealthComp> catTargets = new List<HealthComp>();
        private static List<HealthComp> dogTargets = new List<HealthComp>();

        private static HealthComp[] healthComps;
        private Vector3 startPosition;

        public AIState currentState = AIState.FindTarget;

        private void Start()
        {
            if (healthComps == null)
                healthComps = FindObjectsOfType<HealthComp>();

            if (mouseTargets.Count == 0)
                mouseTargets = GetMouseTargets();
            if (catTargets.Count == 0)
                catTargets = GetCatTargets();
            if (dogTargets.Count == 0)
                dogTargets = GetDogTargets();

            startPosition = transform.position;
            agent = GetComponent<NavMeshAgent>();
            agent.Warp(startPosition);

            InitFSM();
        }

        private void Update()
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
            finiteStateMachine.AddState("Find Target", new FindTarget(this));
            finiteStateMachine.AddState("Chase", new Chase(this));
            finiteStateMachine.AddState("Attack", new Attack(this));

            // add transitions
            finiteStateMachine.AddTransition("Find Target", "Chase", FindTargetToChaseCondition);
            finiteStateMachine.AddTransition("Find Target", "Attack", FindTargetToAttackCondition);
            finiteStateMachine.AddTransition("Chase", "Attack", ChaseToAttackCondition);
            finiteStateMachine.AddTransition("Chase", "Find Target", ChaseToFindTargetCondition);
            finiteStateMachine.AddTransition("Attack", "Chase", AttackToChaseCondition);
            finiteStateMachine.AddTransition("Attack", "Find Target", AttackToFindTargetCondition);
        }

        #region TRANSITIONS
        private bool FindTargetToChaseCondition()
        {
            return currentTarget && distanceToTarget > attackRange;
        }

        private bool FindTargetToAttackCondition()
        {
            return currentTarget && distanceToTarget <= attackRange;
        }

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
        #endregion

        public List<HealthComp> GetTargets()
        {
            switch (type)
            {
                case EnemyType.Mouse:
                    return mouseTargets;
                case EnemyType.Cat:
                    return catTargets;
                case EnemyType.Dog:
                    return dogTargets;
            }

            return null;
        }

        private List<HealthComp> GetMouseTargets()
        {
            List<HealthComp> targets = new List<HealthComp>();

            for (int i = 0; i < healthComps.Length; i++)
            {
                if (healthComps[i].myClass == CharacterClass.Caravan)
                {
                    targets.Add(healthComps[i]);
                }
            }

            return targets;
        }

        private List<HealthComp> GetCatTargets()
        {
            List<HealthComp> targets = new List<HealthComp>();

            for (int i = 0; i < healthComps.Length; i++)
            {
                if (healthComps[i].myClass == CharacterClass.Player || healthComps[i].myClass == CharacterClass.Caravan)
                {
                    targets.Add(healthComps[i]);
                }
            }

            return targets;
        }

        private List<HealthComp> GetDogTargets()
        {
            List<HealthComp> targets = new List<HealthComp>();

            for (int i = 0; i < healthComps.Length; i++)
            {
                if (healthComps[i].myClass == CharacterClass.Player)
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

        //public static void AddToTargetList(HealthComp target)
        //{
        //    if (!targets.Contains(target))
        //        targets.Add(target);
        //}

        public void RemoveFromTargetList(HealthComp target)
        {
            switch (type)
            {
                case EnemyType.Mouse:
                    if (mouseTargets.Contains(target))
                        mouseTargets.Remove(target);
                    break;
                case EnemyType.Cat:
                    if (catTargets.Contains(target))
                        catTargets.Remove(target);
                    break;
                case EnemyType.Dog:
                    if (dogTargets.Contains(target))
                        dogTargets.Remove(target);
                    break;
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }
    }
}