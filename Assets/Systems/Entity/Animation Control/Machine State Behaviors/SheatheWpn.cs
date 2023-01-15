using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheatheWpn : StateMachineBehaviour {
    //makes sure code only executes once per animation cycle
    bool isActivated = false;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (animator.transform.gameObject.name == "Humanoid M Template")
        {
            isActivated = false;
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (animator.transform.gameObject.name == "Humanoid M Template" && stateInfo.normalizedTime >= 1 && isActivated == false)
        {
            animator.transform.root.gameObject.GetComponent<AttackSystem>().wpnIsDrawn = false;
            foreach (Animator m in animator.transform.root.gameObject.GetComponent<AnimationControllerShell>().animatorGroup)
            {
                m.SetBool("IsWpnDrawn", false);
            }
            if (animator.transform.root.Find("Equipment/Right Hand").childCount > 0)
            {
                animator.transform.root.Find("Equipment/Right Hand").GetChild(0).GetComponent<ToggleWeaponEnabled>().DisableAnimator();
            }
            if (animator.transform.root.Find("Equipment/Left Hand").childCount > 0)
            {
                animator.transform.root.Find("Equipment/Left Hand").GetChild(0).GetComponent<ToggleWeaponEnabled>().DisableAnimator();
                animator.transform.root.Find("Equipment/Left Hand").GetChild(0).gameObject.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;

            }
            isActivated = true;
            animator.transform.root.gameObject.GetComponent<AnimationControllerShell>().weaponIsSheathed = true;
        }
        
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}
}
