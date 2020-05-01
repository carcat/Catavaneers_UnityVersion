using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using FiniteStateMachine.StatePolymorphism;

namespace AI.States
{
    public class Chase : State
    {
        // reference from external variables
        private Controller controller = null;
        private Transform target = null;
        private NavMeshAgent agent = null;
        private float speed = 0;

        public Chase(Controller controller)
        {
            this.controller = controller;
        }

        override public void OnStateEnter()
        {
            if (!controller)
            {
                Debug.LogWarning("Controller is not set in Patrol state");
            }

            if (!agent)
            {
                agent = controller.Agent;
            }

            agent.isStopped = false;
            
            target = controller.CurrentTarget;

            speed = controller.ChaseSpeed;
        }

        override public void Update(float deltaTime)
        {
            ChaseBehaviour();
        }

        override public void OnStateExit()
        {
            agent.isStopped = true;
        }

        private void ChaseBehaviour()
        {
            if (!target) return;

            HealthComp targetHealth = target.GetComponent<HealthComp>();

            if (!targetHealth.IsDead())
            {
                agent.speed = speed;
                agent.SetDestination(target.position);
            }
            else
            {
                Controller.RemoveFromTargetList(targetHealth);
                controller.SetCurrentTarget(null);
            }
        }
    }
}