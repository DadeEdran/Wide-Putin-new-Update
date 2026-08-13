using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackState : StateMachineBehaviour
{
    Transform Player;
    EnamyInfo Enamy_Info;
     bool Gun=false;
    public float attackRangeOff = 3.5f;

    NavMeshAgent agent;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        Enamy_Info=animator.GetComponent<EnamyInfo>();
        agent = animator.GetComponent<NavMeshAgent>();
        Gun = Enamy_Info.Gun;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Gun = Enamy_Info.Gun;
        //var ts = Player.position;
        //agent.transform.LookAt(Player);
        Enamy_Info.ChangeRotation();
        Enamy_Info.set_rig_aim();
        //animator.transform.LookAt(Player);
        float distance = Vector3.Distance(Player.position, animator.transform.position);
        if (distance > attackRangeOff)
        {
            animator.SetBool("isAttacking", false);
        }
        if (Gun == true)
        {
            Enamy_Info.fire();
        }
        

    }



}
