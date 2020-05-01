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

        // internal variables
        private float timeSinceLastAttack = Mathf.Infinity;
        private Transform target = null;

        public Attack(Controller controller)
        {
            this.controller = controller;
        }

        override public void OnStateEnter()
        {
            if (!agent)
                agent = controller.Agent;

            target = controller.CurrentTarget;
            attackDamage = controller.AttackDamage;
            attackRange = controller.AttackRange;
            attackInterval = controller.AttackInterval;
            controller.currentState = AIState.Attack;
        }

        override public void Update(float deltaTime)
        {
            AttackBehaviour(deltaTime);
        }

        override public void OnStateExit()
        {

        }

        private void AttackBehaviour(float deltaTime)
        {
            if (TimeToAttack())
            {
                timeSinceLastAttack = 0;
                DealDamage(attackDamage);
            }
            
            timeSinceLastAttack += deltaTime;
        }

        private bool TimeToAttack()
        {
            return timeSinceLastAttack >= attackInterval;
        }

        private void DealDamage(int damage)
        {
            HealthComp targetHealth = target.GetComponent<HealthComp>();

            if (!targetHealth.IsDead())
            {
                targetHealth.TakeDamage(damage);
                Debug.Log("[" + controller.name + "] dealt " + damage + " to [" + target.name + "]");
            }
            else
            {
                controller.RemoveFromTargetList(targetHealth);
                controller.SetCurrentTarget(null);
            }
        }
    }
}