using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI.PatrolPath;

namespace AI
{
    public class Controller : MonoBehaviour
    {
        [Header("Patrol Settings")]
        [SerializeField] private PatrolPathGraph patrolPath = null;
        [SerializeField] private float patrolSpeed = 0;
        [SerializeField] private float waypointDwellTime = 0;
        [SerializeField] private float waypointTolerance = 0;

        [Header("Chase Settings")]
        [SerializeField] private float chaseSpeed = 0;
        [SerializeField] private float visualRange = 0;

        [Header("Attack Settings")]
        [SerializeField] private float attackDamage = 0;
        [SerializeField] private float attackRange = 0;
        [SerializeField] private float attackInterval = 0;

        // Start is called before the first frame update
        void Start()
        {
            if (!patrolPath)
                patrolPath = GetComponentInChildren<PatrolPathGraph>();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}