using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using FiniteStateMachine.StatePolymorphism;

namespace AI.States
{
    public class FindTarget : State
    {
        // reference from external variables
        private Controller controller = null;
        private NavMeshAgent agent = null;
        private List<HealthComp> targets = new List<HealthComp>();
        private Transform target = null;
        private float distanceToTarget = Mathf.Infinity;

        public FindTarget(Controller controller)
        {
            this.controller = controller;
        }

        override public void OnStateEnter()
        {
            distanceToTarget = controller.DistanceToTarget;
            targets = controller.GetTargets();
            target = FindClosestTarget();
            controller.SetCurrentTarget(target);
            controller.currentState = AIState.FindTarget;
        }

        override public void Update(float deltaTime)
        {
            if (target) return;
            FindTargetBehaviour();
        }

        private void FindTargetBehaviour()
        {
            // find target
            target = FindClosestTarget();

            // set target
            controller.SetCurrentTarget(target);
        }

        override public void OnStateExit()
        {

        }

        private Transform FindClosestTarget()
        {
            Transform closestTarget = null;
            float closestDistance = Mathf.Infinity;

            if (targets.Count > 0)
            {
                closestTarget = targets[0].transform;
                closestDistance = Vector3.Distance(controller.transform.position, targets[0].transform.position);

                for (int i = 1; i < targets.Count; i++)
                {
                    float distanceToTarget = Vector3.Distance(controller.transform.position, targets[i].transform.position);

                    if (distanceToTarget < closestDistance)
                    {
                        closestTarget = targets[i].transform;
                        closestDistance = distanceToTarget;
                    }
                }
            }
            else
            {
                Debug.Log("No more target");
            }

            return closestTarget;
        }
    }
}