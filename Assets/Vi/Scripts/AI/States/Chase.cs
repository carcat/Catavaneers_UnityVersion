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
        HealthComp targetHealth = null;
        private NavMeshAgent agent = null;

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
            targetHealth = target.GetComponent<HealthComp>();

            controller.currentState = AIState.Chase;
        }

        override public void Update(float deltaTime)
        {
            ChaseBehaviour();
        }

        override public void OnStateExit()
        {
            agent.isStopped = true;
        }

        /// <summary>
        /// The stuff that will be done in chase mode
        /// </summary>
        private void ChaseBehaviour()
        {
            if (!target) return;

            if (!targetHealth.IsDead())
            {
                agent.speed = controller.ChaseSpeed;
                agent.SetDestination(target.position);
            }
            else
            {
                controller.RemoveFromTargetList(targetHealth);
                controller.SetCurrentTarget(null);
            }
        }
    }
}