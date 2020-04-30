using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using FiniteStateMachine.StatePolymorphism;

namespace AI.States
{
    public class Attack : State
    {
        // reference from external variables
        private Controller controller = null;
        private NavMeshAgent agent = null;
        private int attackDamage = 0;
        private float attackRange = 0;
        private float attackInterval = 0;

        public Attack(Controller controller)
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
            throw new System.NotImplementedException();
        }

        override public void OnStateExit()
        {
            throw new System.NotImplementedException();
        }
    }
}