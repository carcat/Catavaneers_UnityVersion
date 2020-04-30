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
                Debug.LogWarning("Controller is not set in Patrol state");

            if (!agent)
                agent = controller.Agent;
        }

        override public void Update(float deltaTime)
        {
            ChaseBehaviour();
        }

        override public void OnStateExit()
        {

        }

        private void ChaseBehaviour()
        {
            if (target)
            {
                agent.destination = target.position;
            }
        }
    }
}