using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class ToggleWeaponEnabled : MonoBehaviour {
    //attach to all weapon models 
    //toggles weapon state on and off when sheathed or drawn

    //this item's data
    public ItemData itemInfo;

    //reference to local animation controller shell
    public AnimationControllerShell entityAnimCtrlSHell;

    //refrence to local attack system
    public AttackSystem entityAttackSystem;

	// Use this for initialization
	void Start () {
        entityAnimCtrlSHell = gameObject.transform.root.gameObject.GetComponent<AnimationControllerShell>();
        entityAttackSystem = gameObject.transform.root.gameObject.GetComponent<AttackSystem>();
        DelayedInit();
    }

    private void Update()
    {
        //Debug.Log(gameObject.GetComponent<Animator>().GetBool("IsWpnDrawn"));
        //if (Time.timeScale ==1) { Debug.Log(gameObject.transform.root.gameObject.GetComponent<AttackSystem>().wpnIsDrawn); }
        
    }

    private void LateUpdate()
    {
        
    }
    //enables animator for weapon when unsheathed
    public void DisableAnimator()
    {
        switch (transform.root.gameObject.GetComponent<AttackSystem>().currentAttackType)
        {
            case "1H Stab":
                gameObject.GetComponent<Animator>().Play("1H Unsheathe", -1, 0.25f);
                break;
            case "1H NoStab":
                gameObject.GetComponent<Animator>().Play("1H Unsheathe", -1, 0.25f);
                break;
            case "1H Polearm":
                gameObject.GetComponent<Animator>().Play("Polearm Unsheathe", -1, 0.25f);
                break;
            case "Staff":
                gameObject.GetComponent<Animator>().Play("Staff Unsheathe", -1, 0.25f);
                break;
            case "Bow":
                gameObject.GetComponent<Animator>().Play("Bow Unsheathe", -1, 0.25f);
                break;
            case "Crossbow":
                gameObject.GetComponent<Animator>().Play("Crossbow Unsheathe", -1, 0.25f);
                break;
            case "2H Stab":
                gameObject.GetComponent<Animator>().Play("2H Unsheathe", -1, 0.25f);
                break;
            case "2H Nostab":
                gameObject.GetComponent<Animator>().Play("2H Unsheathe", -1, 0.25f);
                break;
            case "2H Polearm":
                gameObject.GetComponent<Animator>().Play("Polearm Unsheathe", -1, 0.25f);
                break;
        }
        if (itemInfo.CombatType == "Shield" || itemInfo.CombatType == "Tome"||
            itemInfo.CombatType == "1H Polearm" || itemInfo.CombatType == "Staff" ||
            itemInfo.CombatType == "Bow" || itemInfo.CombatType == "Crossbow" ||
            itemInfo.CombatType == "2H Stab" || itemInfo.CombatType == "2H Nostab" ||
            itemInfo.CombatType == "2H Polearm" || itemInfo.CombatType == "1H Stab")
        {
            gameObject.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
        }
        Invoke("ConstrainWpnPosition", 0.02f);
    }

    //disabled animator for when weapon is sheathed
    public void EnableAnimator()
    {
        if (itemInfo.CombatType == "Shield" || itemInfo.CombatType == "Tome" ||
            itemInfo.CombatType == "1H Polearm" || itemInfo.CombatType == "Staff" ||
            itemInfo.CombatType == "Bow" || itemInfo.CombatType == "Crossbow" ||
            itemInfo.CombatType == "2H Stab" || itemInfo.CombatType == "2H Nostab" ||
            itemInfo.CombatType == "2H Polearm" || itemInfo.CombatType == "1H Stab")
        {
            gameObject.GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
        }
        gameObject.GetComponent<Animator>().enabled = true;
    }

    //constrains local position of weapon/ offhand positions and rotations
    public void ConstrainWpnPosition()
    {
        gameObject.GetComponent<Animator>().enabled = false;
    }

    //delayed initializer
    public void DelayedInit()
    {
        if (entityAttackSystem.wpnIsDrawn == false)
        {
            DisableAnimator();
        }
    }


}
