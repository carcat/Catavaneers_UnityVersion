using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Enemy;

public class PatrolBehaviour : StateMachineBehaviour
{
    private EnemyBase enemyBase;
    private float visualRange;
    private float moveSpeed;
    private Transform target;
    private NavMeshAgent agent;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemyBase = animator.GetComponent<EnemyBase>();
        visualRange = enemyBase.Specs.visualRange;
        moveSpeed = enemyBase.Specs.moveSpeed;
        agent = animator.GetComponent<NavMeshAgent>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Collider[] colliders = Physics.OverlapSphere(animator.transform.position, visualRange);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
