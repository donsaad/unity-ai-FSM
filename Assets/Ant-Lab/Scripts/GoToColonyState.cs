using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToColonyState : AntState
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ant.seek.target = ant.colonyTransform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Vector3.Distance(ant.colonyTransform.position, ant.transform.position) < AntAnimatorFSM.inRange)
        {
            ant.sugarController.ResetSugar();
            animator.SetInteger("SugarNum", 0);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ant.seek.target = ant.sugarController.GetRandomSugar().transform;
    }

}
