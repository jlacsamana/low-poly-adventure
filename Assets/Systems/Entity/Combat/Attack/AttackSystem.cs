using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSystem : MonoBehaviour {
    //attach this to an entity that can attack
    //manages attacks and attack animations based on weapon type

    //reference to this entity's inventory
    public Inventory entityInventory;

    //reference to this entity's animation controller shell
    public AnimationControllerShell entityAnimControllerShell;

    //reference to this entity's attack executer
    public AttackHandler entityAttackHandler;

    //reference to this entity's shield system
    public ShieldSystem entityShieldSys;

    //reference to this entity's combat listener
    public CombatListener entityCombatListener;

    //current attack type
    public string currentAttackType = "No Weapon";
    public string currentOffhandAttackType = "None";

    //timings for attacks
    public int powerAttackTimings = 60;
    public int normalAttackTimings = 40;

    //is weapon sheathed or drawn
    public bool wpnIsDrawn = false;

    //is attacking; disables weapon switching
    public bool isAttacking = false;

    //current charge 
    public float weaponCharge = 0;

    private void Start()
    {
        entityInventory = gameObject.GetComponent<Inventory>();
        entityAnimControllerShell = gameObject.GetComponent<AnimationControllerShell>();
        entityAttackHandler = gameObject.GetComponent<AttackHandler>();
        entityShieldSys = gameObject.GetComponent<ShieldSystem>();
        entityCombatListener = gameObject.GetComponent<CombatListener>();
        Invoke("AttackTypeListener", 0.01f);
    }

    private void Update()
    {

    }

    //checks current weapon type 
    public void AttackTypeListener()
    {
        //checks for weapon type of equipped weapon
        switch (entityInventory.entityRightHandSlot.CombatType)
        {
            case "No Weapon":
                currentAttackType = "No Weapon";
                break;
            case "1H Stab":
                currentAttackType = "1H Stab";
                break;
            case "1H NoStab":
                currentAttackType = "1H NoStab";
                break;
            case "1H Polearm":
                currentAttackType = "1H Polearm";
                break;
            case "Staff":
                currentAttackType = "Staff";
                break;
            case "Bow":
                currentAttackType = "Bow";
                break;
            case "Crossbow":
                currentAttackType = "Crossbow";
                break;
            case "2H Stab":
                currentAttackType = "2H Stab";
                break;
            case "2H Nostab":
                currentAttackType = "2H Nostab";
                break;
            case "2H Polearm":
                currentAttackType = "2H Polearm";
                break;
        }
        //checks for weapon type of offhand equipped weapon
        switch (entityInventory.entityLeftHandSlot.CombatType)
        {
            case "None":
                currentOffhandAttackType = "None";
                break;
            case "Shield":
                currentOffhandAttackType = "Shield";
                break;
            case "Tome":
                currentOffhandAttackType = "Tome";
                break;

        }
    }

    //sheathes and unsheathes weapon
    public void WpnSheatheToggle()
    {
        if (currentAttackType == "No Weapon")
        {
            switch (wpnIsDrawn)
            {
                case true:
                    wpnIsDrawn = false;
                    entityAnimControllerShell.weaponIsSheathed = true;
                    break;
                case false:
                    wpnIsDrawn = true;
                    entityAnimControllerShell.weaponIsSheathed = false;
                    break;
            }
            entityAnimControllerShell.ChangeBoolVal("IsWpnDrawn", wpnIsDrawn);
        }
        else
        {
            switch (wpnIsDrawn)
            {
                case true:
                    entityAnimControllerShell.ActivateTrigger("SheatheWpn");
                    break;
                case false:
                    entityAnimControllerShell.ActivateTrigger("UnsheatheWpn");
                    break;
            }
            
        }
    }

    //MELEE ATTACKS
    //melee attack
    public void RegularMeleeAttack()
    {
        entityAnimControllerShell.ActivateTrigger("IsAttacking");
        entityAnimControllerShell.ChangeBoolVal("IsPowerAttack", false);
        entityAnimControllerShell.ChangeBoolVal("IsCurrentlyAttacking", true);

    }
    //melee power attack
    public void PowerMeleeAttack()
    {
        entityAnimControllerShell.ActivateTrigger("IsAttacking");
        entityAnimControllerShell.ChangeBoolVal("IsPowerAttack", true);
        entityAnimControllerShell.ChangeBoolVal("IsCurrentlyAttacking", true);
    }

    //CROSSBOW ATTACKS
    //starts crossbow attack
    public void StartCrossbow()
    {
        if (entityInventory.AmmoQuiver.Count > 0)
        {
            entityAnimControllerShell.ActivateTrigger("IsAttacking");
            entityAnimControllerShell.ChangeBoolVal("IsCurrentlyAttacking", true);
            weaponCharge = 0;
            entityAnimControllerShell.SetFloat("Charge", weaponCharge);
            //instantiates crossbow bolt
            entityAttackHandler.StartCrossbowAttackShell();
            //syncs bow animation with the rest of entity
            entityAnimControllerShell.projectileWpnAnim.SetTrigger("IsAttacking");
        }
    }
    //continued crossbow attack
    public void ContinueCrossbowAttack()
    {
        weaponCharge += Time.deltaTime;
        //moves bolt 
        entityAttackHandler.PullbackCrossbowBolt();
    }
    //fire crossbow
    public void FireCrossbowAttack()
    {
        weaponCharge += Time.deltaTime;
        entityAnimControllerShell.ActivateTrigger("IsFiring");
        entityAnimControllerShell.projectileWpnAnim.SetTrigger("IsFiring");
    }
    //sheathes bolt
    public void SheatheCrossbowBolt()
    {
        entityAnimControllerShell.ActivateTrigger("SheatheProjectile");
        entityAnimControllerShell.projectileWpnAnim.SetTrigger("SheatheProjectile");
        //puts away bolt
        entityAttackHandler.SheatheCrossbowBolt();
    }

    //STAFF ATTACKS
    //starts charged staff attack
    public void StartStaffChargeAttack()
    {
        entityAnimControllerShell.ActivateTrigger("IsAttacking");
        entityAnimControllerShell.ChangeBoolVal("IsCasting", true);
        entityAnimControllerShell.ChangeBoolVal("IsCurrentlyAttacking", true);
        weaponCharge = 0;
        entityAnimControllerShell.SetFloat("Charge", weaponCharge);
    }
    //charges up staff attack
    public void ChargeStaffAttack()
    {
        weaponCharge += Time.deltaTime;
    }
    //fire if charged
    public void FireChargedStaffAttack()
    {
        weaponCharge += Time.deltaTime;
        entityAnimControllerShell.ActivateTrigger("IsFiring");
        entityAnimControllerShell.ChangeBoolVal("IsCasting", false);
    }
    //stop charged attack
    public void StopChargingStaff()
    {
        entityAnimControllerShell.ActivateTrigger("SheatheProjectile");
        entityAnimControllerShell.ChangeBoolVal("IsCasting", false);
    }
    //fires a constant staff attack
    public void ConstantStaffFire()
    {
        entityAnimControllerShell.ActivateTrigger("IsAttacking");
        entityAnimControllerShell.ChangeBoolVal("Constant Fire", true);
        entityAnimControllerShell.ChangeBoolVal("IsCasting", true);
        entityAnimControllerShell.ChangeBoolVal("IsCurrentlyAttacking", true);
    }
    //stops firing a constant staff attack
    public void StopConstantStaffFire()
    {
        entityAnimControllerShell.ChangeBoolVal("Constant Fire", false);
        entityAnimControllerShell.ChangeBoolVal("IsCasting", false);
    }

    //BOW ATTACKS
    //starts firing bow
    public void StartBowAttack()
    {
        if (entityInventory.AmmoQuiver.Count > 0)
        {
            weaponCharge = 0;
            entityAnimControllerShell.SetFloat("Charge", weaponCharge);
            entityAnimControllerShell.ActivateTrigger("IsAttacking");
            entityAnimControllerShell.ChangeBoolVal("IsCurrentlyAttacking", true);
            //instantiates arrow
            entityAttackHandler.StartBowAttackShell();
            //syncs bow animation with the rest of entity
            entityAnimControllerShell.projectileWpnAnim.SetTrigger("IsAttacking");
            entityAnimControllerShell.projectileWpnAnim.SetFloat("Charge", weaponCharge);
        }
    }
    //continue charging bow
    public void ChargeBow()
    {
        if (weaponCharge < 1)
        {
            weaponCharge += Time.deltaTime;
        }
        else
        {
            weaponCharge = 1;
        }
        entityAnimControllerShell.SetFloat("Charge", (weaponCharge * entityInventory.entityRightHandSlot.WeaponSpeed));
        entityAnimControllerShell.projectileWpnAnim.SetFloat("Charge", (weaponCharge * entityInventory.entityRightHandSlot.WeaponSpeed));
        //pull back arrow
        entityAttackHandler.PullBackArrow();
    }
    //fire bow
    public void FireBow()
    {
        if (weaponCharge < 1)
        {
            weaponCharge += Time.deltaTime;
        }
        else
        {
            weaponCharge = 1;
        }
        entityAnimControllerShell.SetFloat("Charge", weaponCharge);
        entityAnimControllerShell.projectileWpnAnim.SetFloat("Charge", weaponCharge);
        entityAnimControllerShell.ActivateTrigger("IsFiring");
        entityAnimControllerShell.projectileWpnAnim.SetTrigger("IsFiring");
    }
    //sheathe arrow
    public void SheatheArrow()
    {
        entityAnimControllerShell.ActivateTrigger("SheatheProjectile");
        entityAnimControllerShell.projectileWpnAnim.SetTrigger("SheatheProjectile");
        //puts away arrow
        entityAttackHandler.SheatheArrow();
    }

    //OFFHAND
    //SHIELD
    //raises shield
    public void RaiseShield()
    {
        if (entityShieldSys.isAbleToBlock)
        {
            entityAnimControllerShell.ChangeBoolVal("IsShielding", true);
            entityShieldSys.isBlocking = true;
        }
    }
    //lowers shield
    public void LowerShield()
    {
        entityAnimControllerShell.ChangeBoolVal("IsShielding", false);
        entityShieldSys.isBlocking = false;
    }
    //PARRY
    //parries weapon
    public void ParryWpn()
    {
        if (entityShieldSys.isAbleToParry)
        {
            //Debug.Log("working");
            entityAnimControllerShell.ChangeBoolVal("IsParrying", true);
            entityShieldSys.isParrying = true;
        }     
    }
    //stops parrying weapon
    public void StopParryWpn()
    {
        entityAnimControllerShell.ChangeBoolVal("IsParrying", false);
        entityShieldSys.isParrying = false;
    }

}
