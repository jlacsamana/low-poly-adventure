using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnsheatheWpn : StateMachineBehaviour {
    //makes sure code only executes once per animation cycle
    bool isActivated = false;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (animator.transform.gameObject.name == "Humanoid M Template")
        {
            isActivated = false;
        }
        isActivated = false;
    }

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (animator.transform.gameObject.name == "Humanoid M Template" && stateInfo.normalizedTime >= 0.25 && isActivated == false)
        {
            if (animator.transform.root.Find("Equipment/Right Hand").childCount > 0)
            {
                animator.transform.root.Find("Equipment/Right Hand").GetChild(0).GetComponent<ToggleWeaponEnabled>().EnableAnimator();
            }
            if (animator.transform.root.Find("Equipment/Left Hand").childCount > 0)
            {
                animator.transform.root.Find("Equipment/Left Hand").GetChild(0).GetComponent<ToggleWeaponEnabled>().EnableAnimator();
                animator.transform.root.Find("Equipment/Left Hand").GetChild(0).gameObject.GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
            }
            animator.transform.root.gameObject.GetComponent<AttackSystem>().wpnIsDrawn = true;
            foreach (Animator m in animator.transform.root.gameObject.GetComponent<AnimationControllerShell>().animatorGroup)
            {
                m.SetBool("IsWpnDrawn", true);
                if (m.transform.parent.name == "Right Hand" || m.transform.parent.name == "Left Hand")
                {
                    switch (m.transform.root.gameObject.GetComponent<AttackSystem>().currentAttackType)
                    {
                        case "1H Stab":
                            m.Play("1H Unsheathe", -1, 0.25f);
                            break;
                        case "1H NoStab":
                            m.Play("1H Unsheathe", -1, 0.25f);
                            break;
                        case "1H Polearm":
                            m.Play("Polearm Unsheathe", -1, 0.25f);
                            break;
                        case "Staff":
                            m.Play("Staff Unsheathe", -1, 0.25f);
                            break;
                        case "Bow":
                            m.Play("Bow Unsheathe", -1, 0.25f);
                            break;
                        case "Crossbow":
                            m.Play("Crossbow Unsheathe", -1, 0.25f);
                            break;
                        case "2H Stab":
                            m.Play("2H Unsheathe", -1, 0.25f);
                            break;
                        case "2H Nostab":
                            m.Play("2H Unsheathe", -1, 0.25f);
                            break;
                        case "2H Polearm":
                            m.Play("Polearm Unsheathe", -1, 0.25f);
                            break;
                    }
                }
            }
            isActivated = true;
            animator.transform.root.gameObject.GetComponent<AnimationControllerShell>().weaponIsSheathed = false;
        }
    }

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        

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
