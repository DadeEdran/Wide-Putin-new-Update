using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class ChaseState : StateMachineBehaviour
{
    NavMeshAgent agent;
    Transform Player;
    public float ChaseSpeed = 7f;
    public float AttackRange = 5f;
    public float ChaseOutLine = 30f;
    EnamyInfo Enamy_Info;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<NavMeshAgent>();
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        agent.speed = ChaseSpeed;
        Enamy_Info = animator.GetComponent<EnamyInfo>();

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Enamy_Info.set_rig_running();
        agent.SetDestination(Player.position);

        float distance = Vector3.Distance(Player.position, animator.transform.position);
        if (distance > ChaseOutLine)
            animator.SetBool("isChasing", false);

        if (distance < AttackRange)
            animator.SetBool("isAttacking", true);

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(animator.transform.position);
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Implement code that processes and affects root motion
    }

    // OnStateIK is called right after Animator.OnAnimatorIK()
    override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Implement code that sets up animation IK (inverse kinematics)
    }
}
