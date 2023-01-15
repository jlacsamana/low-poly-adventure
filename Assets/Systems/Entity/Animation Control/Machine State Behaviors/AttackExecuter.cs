using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackExecuter : StateMachineBehaviour {

    //frame counter
    int frameCounter = 0;

    //reference to combat system
    AttackSystem attackSystem;


	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        frameCounter = 0;
        frameCounter++;
        attackSystem = animator.transform.root.gameObject.GetComponent<AttackSystem>();
        if (animator.gameObject.transform.root.gameObject.GetComponent<Inventory>().entityRightHandSlot.WeaponType == "One Handed" ||
            animator.gameObject.transform.root.gameObject.GetComponent<Inventory>().entityRightHandSlot.WeaponType == "Two Handed")
        {
            animator.SetFloat("WpnSpeed", animator.gameObject.transform.root.gameObject.GetComponent<Inventory>().entityRightHandSlot.WeaponSpeed);
        }
    }

	//OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (Time.timeScale ==1)
        {
            frameCounter++;
            if (animator.gameObject.name == "Humanoid M Template")
            {
                //if attack is with a melee weapon 
                if (attackSystem.currentAttackType == "No Weapon" ||
                attackSystem.currentAttackType == "1H Stab" || attackSystem.currentAttackType == "1H NoStab" || attackSystem.currentAttackType == "1H Polearm" ||
                attackSystem.currentAttackType == "2H Stab" || attackSystem.currentAttackType == "2H Nostab" || attackSystem.currentAttackType == "2H Polearm")
                {
                    switch (animator.GetBool("IsPowerAttack"))
                    {
                        case true:
                            if (frameCounter == (animator.transform.root.gameObject.GetComponent<AttackSystem>().powerAttackTimings / 2))
                            {
                                animator.transform.root.gameObject.GetComponent<AttackHandler>().ExecuteMeleeAttack();
                            }
                            break;
                        case false:
                            if (frameCounter == ((animator.transform.root.gameObject.GetComponent<AttackSystem>().normalAttackTimings / 2)))
                            {
                                animator.transform.root.gameObject.GetComponent<AttackHandler>().ExecuteMeleeAttack();
                            }
                            break;
                    }
                }
                //if attack is with staff
                if (attackSystem.currentAttackType == "Staff")
                {
                    if (attackSystem.entityInventory.entityLeftHandSlot.WeaponType == "Tome")
                    {
                        if (attackSystem.entityInventory.entityLeftHandSlot.WeaponType == "Charge")
                        {
                            if (frameCounter == 2)
                            {
                                animator.transform.root.gameObject.GetComponent<AttackHandler>().ExecuteChargedStaffAttack();
                            }
                        }
                    }
                    else
                    {
                        if (frameCounter == 2)
                        {                          
                            animator.transform.root.gameObject.GetComponent<AttackHandler>().ExecuteChargedStaffAttack();
                        }
                    }
                }
                //if attack is with bow
                if (attackSystem.currentAttackType == "Bow")
                {
                    if (frameCounter == 2)
                    {
                        animator.transform.root.gameObject.GetComponent<AttackHandler>().FireBow();
                    }
                }
                //if attack is with crossbow
                if (attackSystem.currentAttackType == "Crossbow")
                {
                    if (frameCounter == 2)
                    {
                        animator.transform.root.gameObject.GetComponent<AttackHandler>().FireCrossbow();
                    }
                }
            }
        }

    }

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {


    }




}
