using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectSugarState : AntState
{
    int SugarNum;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
      //  ant.seek.target = ant.sugarController.GetRandomSugar().transform;
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        SugarNum = animator.GetInteger("SugarNum");
        if (ant.SugarHit)
        {
            ant.SugarHit = false;
            animator.SetInteger("SugarNum", ++SugarNum);
            ant.seek.target = ant.sugarController.GetRandomSugar().transform;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ant.seek.target = ant.colonyTransform;
    }
}
