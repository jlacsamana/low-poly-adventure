using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour {
    //handles player input for combat
    //attach only to player entity

    //reference to this entity's inventory
    public Inventory entityInventory;

    //reference to this entity's animation controller shell
    public AnimationControllerShell entityAnimControllerShell;

    //reference to local main animator
    public Animator entityMainAnim;

    //reference to this entity's atatck system
    public AttackSystem entityAttackSys;

    //sheathes and unsheathes weapon
    public void SheatheAndUnsheatheWpn()
    {
        if (Input.GetKeyDown(Keybindings.sheatheAndUnsheathe))
        {
            entityAttackSys.WpnSheatheToggle();
        }
    }

    //checks for state changes due to attacking and blocking
    public void AttackStateChangeListnener()
    {
        if (Input.GetKeyDown(Keybindings.shieldOrParry) || Input.GetKeyUp(Keybindings.shieldOrParry))
        {
            entityAnimControllerShell.ChangeStateTrigger();
        }
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0))
        {
            entityAnimControllerShell.ChangeStateTrigger();
        }
        if (Input.GetMouseButtonDown(1) || Input.GetMouseButtonUp(1))
        {
            entityAnimControllerShell.ChangeStateTrigger();
        }
        if (Input.GetKeyDown(Keybindings.sheatheAndUnsheathe))
        {
            entityAnimControllerShell.ChangeStateTrigger();
        }
    }

    //INPUT LISTENERS
    //checks for primary attacks
    public void AttackInputListener()
    {
        if (entityAttackSys.wpnIsDrawn && Time.timeScale == 1 && entityMainAnim.GetBool("IsShielding") == false &&
            entityMainAnim.GetBool("IsParrying") == false)
        {
            //if power attack is possible with current weapon
            if (entityAttackSys.currentAttackType == "No Weapon" ||
                entityAttackSys.currentAttackType == "1H Stab" || entityAttackSys.currentAttackType == "1H NoStab" || entityAttackSys.currentAttackType == "1H Polearm" ||
                entityAttackSys.currentAttackType == "2H Stab" || entityAttackSys.currentAttackType == "2H Nostab" || entityAttackSys.currentAttackType == "2H Polearm"
                )
            {
                //normal attack; left click
                if (Input.GetMouseButtonDown(0))
                {
                    if (entityAttackSys.currentAttackType == "No Weapon"|| entityAttackSys.currentAttackType == "1H NoStab"||entityAttackSys.currentAttackType == "1H Polearm"|| entityAttackSys.currentAttackType == "2H Polearm")
                    {
                        entityAnimControllerShell.ApplyRandomAttack(Random.Range(1,3));
                    }
                    else if (entityAttackSys.currentAttackType == "1H Stab"|| entityAttackSys.currentAttackType == "2H Stab"||entityAttackSys.currentAttackType == "2H Nostab")
                    {
                        entityAnimControllerShell.ApplyRandomAttack(Random.Range(1, 4));
                    }
                    entityAttackSys.RegularMeleeAttack();
                }
                //power attack; right click
                else if (Input.GetMouseButtonDown(1))
                {
                    if (entityAttackSys.currentAttackType == "No Weapon" || entityAttackSys.currentAttackType == "1H NoStab" || entityAttackSys.currentAttackType == "1H Polearm" || entityAttackSys.currentAttackType == "2H Polearm")
                    {
                        entityAnimControllerShell.ApplyRandomAttack(Random.Range(1, 3));
                    }
                    else if (entityAttackSys.currentAttackType == "1H Stab" || entityAttackSys.currentAttackType == "2H Stab" || entityAttackSys.currentAttackType == "2H Nostab")
                    {
                        entityAnimControllerShell.ApplyRandomAttack(Random.Range(1, 4));
                    }
                    entityAttackSys.PowerMeleeAttack();
                }
            }
            //if weapon must be recharged to fire like a crossbow
            else if (entityAttackSys.currentAttackType == "Crossbow")
            {
                //if entity is idle
                if (entityAnimControllerShell.animatorGroup[0].GetBool("IsCurrentlyAttacking") == false)
                {
                    //charge up crossbow
                    if (Input.GetMouseButtonDown(0))
                    {
                        entityAttackSys.StartCrossbow();
                    }
                }
                //if left click if pressed
                if (entityAnimControllerShell.animatorGroup[0].GetBool("IsCurrentlyAttacking"))
                {
                    if (Input.GetMouseButton(0))
                    {
                        entityAttackSys.ContinueCrossbowAttack();
                    }
                    //fires if reloaded
                    else if (Input.GetMouseButtonUp(0) && entityAttackSys.weaponCharge >= (1.25f * entityInventory.entityRightHandSlot.WeaponSpeed))
                    {
                        entityAttackSys.FireCrossbowAttack();
                    }
                    //sheathes bolt for lack of charge or with right click
                    else if ((Input.GetMouseButtonUp(0) && entityAttackSys.weaponCharge < (1.25f * entityInventory.entityRightHandSlot.WeaponSpeed)))
                    {
                        entityAttackSys.SheatheCrossbowBolt();
                    }
                    //sheathes bolt
                    if (Input.GetMouseButtonDown(1) || Time.timeScale == 0)
                    {
                        entityAttackSys.SheatheCrossbowBolt();
                    }
                }
            }
            //if weapon is a magical staff
            else if (entityAttackSys.currentAttackType == "Staff")
            {
                //if tome equipped
                if (entityAttackSys.currentOffhandAttackType == "Tome")
                {
                    switch (entityInventory.entityLeftHandSlot.MagicType)
                    {
                        //if spell is a charged one
                        case "Charge":
                            //if entity is idle
                            if (entityAnimControllerShell.animatorGroup[0].GetBool("IsCurrentlyAttacking") == false)
                            {
                                //charge up for attack
                                if (Input.GetMouseButtonDown(0))
                                {
                                    entityAttackSys.StartStaffChargeAttack();
                                }
                            }
                            //if left mouse button is held down
                            else if (entityAnimControllerShell.animatorGroup[0].GetBool("IsCurrentlyAttacking"))
                            {
                                if (Input.GetMouseButton(0))
                                {
                                    entityAttackSys.ChargeStaffAttack();
                                }
                                //fire if charged
                                else if (Input.GetMouseButtonUp(0) && entityAttackSys.weaponCharge >= (entityInventory.entityLeftHandSlot.ChargeTime))
                                {
                                    entityAttackSys.FireChargedStaffAttack();
                                }
                                //stop charging
                                else if (Input.GetMouseButtonUp(0) && entityAttackSys.weaponCharge < (entityInventory.entityLeftHandSlot.ChargeTime))
                                {
                                    entityAttackSys.StopChargingStaff();
                                }
                                //stop charging
                                if (Input.GetMouseButtonDown(1) || Time.timeScale == 0)
                                {
                                    entityAttackSys.StopChargingStaff();
                                }

                            }
                            break;
                        //if spell fires constantly
                        case "Constant":
                            if (Input.GetMouseButtonDown(0))
                            {
                                entityAttackSys.ConstantStaffFire();
                            }
                            else if (Input.GetMouseButtonUp(0) || Time.timeScale == 0)
                            {
                                entityAttackSys.StopConstantStaffFire();
                            }
                            break;
                    }
                }
                else
                {
                    //if no tome is equipped, a standard charged spell that takes 1.5 seconds

                    if (entityAnimControllerShell.animatorGroup[0].GetBool("IsCurrentlyAttacking") == false)
                    {
                        //charge up for attack
                        if (Input.GetMouseButtonDown(0))
                        {
                            entityAttackSys.StartStaffChargeAttack();
                        }
                    }
                    //if left mouse button is held down
                    else if (entityAnimControllerShell.animatorGroup[0].GetBool("IsCurrentlyAttacking"))
                    {
                        if (Input.GetMouseButton(0))
                        {
                            entityAttackSys.ChargeStaffAttack();
                        }
                        //fire if charged
                        else if (Input.GetMouseButtonUp(0) && entityAttackSys.weaponCharge >= (1.5f * entityInventory.entityRightHandSlot.WeaponSpeed))
                        {
                            entityAttackSys.FireChargedStaffAttack();
                        }
                        //stop charging
                        else if ((Input.GetMouseButtonUp(0) && entityAttackSys.weaponCharge < (1.5f * entityInventory.entityRightHandSlot.WeaponSpeed)))
                        {
                            entityAttackSys.StopChargingStaff();
                        }
                        //stop charging
                        if (Input.GetMouseButtonDown(1) || Time.timeScale == 0)
                        {
                            entityAttackSys.StopChargingStaff();
                        }
                    }
                }
            }
            //if weapon is bow
            else if (entityAttackSys.currentAttackType == "Bow")
            {
                //if entity is idle
                if (entityAnimControllerShell.animatorGroup[0].GetBool("IsCurrentlyAttacking") == false)
                {
                    //charge up for attack
                    if (Input.GetMouseButtonDown(0))
                    {
                        entityAttackSys.StartBowAttack();
                    }
                }
                //if left click is being held down
                else if (entityAnimControllerShell.animatorGroup[0].GetBool("IsCurrentlyAttacking"))
                {
                    if (Input.GetMouseButton(0))
                    {
                        entityAttackSys.ChargeBow();
                    }
                    //fire if charged
                    else if (Input.GetMouseButtonUp(0) && entityAttackSys.weaponCharge >= (0.5f * entityInventory.entityRightHandSlot.WeaponSpeed))
                    {
                        entityAttackSys.FireBow();
                    }
                    //stop charging and sheathe arrow if not charged long enough
                    else if ((Input.GetMouseButtonUp(0) && entityAttackSys.weaponCharge < (0.5f * entityInventory.entityRightHandSlot.WeaponSpeed)))
                    {
                        entityAttackSys.SheatheArrow();
                    }
                    //stop charging and sheathe arrow
                    if (Input.GetMouseButtonDown(1) || Time.timeScale == 0)
                    {
                        entityAttackSys.SheatheArrow();
                    }

                }

            }
        }
    }

    //checks for offhand input(blocks, parries)
    public void OffhandInputListener()
    {
        if (entityAttackSys.wpnIsDrawn && Time.timeScale == 1)
        {
            //if shield is equipped; can raise it to block
            if (entityAttackSys.currentOffhandAttackType == "Shield")
            {
                if (Input.GetKey(KeyCode.Q))
                {
                    entityAttackSys.RaiseShield();
                }
                else if (Input.GetKeyUp(KeyCode.Q))
                {
                    entityAttackSys.LowerShield();
                }

            }
            //if no shield is equipped but not using tome; can parry with weapon NOTE: unable to parry when unarmed
            else if (entityAttackSys.currentOffhandAttackType != "Tome" && entityAttackSys.currentAttackType != "No Weapon")
            {
                if (Input.GetKey(KeyCode.Q))
                {                  
                    entityAttackSys.ParryWpn();
                }
                else if (Input.GetKeyUp(KeyCode.Q))
                {
                    entityAttackSys.StopParryWpn();
                }
            }
        }
    }

    // Use this for initialization
    void Start()
    {
        entityInventory = gameObject.GetComponent<Inventory>();
        entityAnimControllerShell = gameObject.GetComponent<AnimationControllerShell>();
        entityAttackSys = gameObject.GetComponent<AttackSystem>();
        entityMainAnim = entityAnimControllerShell.animatorGroup[0];
    }

    // Update is called once per frame
    void Update () {
        if (Time.timeScale ==1)
        {
            SheatheAndUnsheatheWpn();
            AttackStateChangeListnener();
            AttackInputListener();
            OffhandInputListener();
        }
	}
}
