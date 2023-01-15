using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldSystem : MonoBehaviour {
    //handles an entity's shield if equipped or weapon if parrying

    //reference to this entity's inventory
    public Inventory entityInventory;
    public Entity entityMain;
    public EntityInfo entityInfo;
    public AttackSystem entityAttackSys;

    //Shield info
    public int maxShieldDurability = 0;
    public int shieldDurability = 0;

    //able to block?
    public bool isAbleToBlock = true;
    public bool isAbleToParry = true;

    //is currently blocking or parrying
    public bool isBlocking = false;
    public bool isParrying = false;

    // Use this for initialization
    void Start () {
        entityInventory = gameObject.GetComponent<Inventory>();
        entityMain = gameObject.GetComponent<Entity>();
        entityInfo = gameObject.GetComponent<EntityInfo>();
        entityAttackSys = gameObject.GetComponent<AttackSystem>();
    }
	
	// Update is called once per frame
	void Update () {
        CheckIfBlockPossible();
        StopBlockChecker();
    }

    //sets shield info
    public void SetShield(int shieldInfo)
    {
        maxShieldDurability = shieldInfo;
        shieldDurability = shieldInfo;
    }

    //regenerates shield durability if not being attacked
    public void RegenerateShield()
    {
        if (shieldDurability < maxShieldDurability)
        {
            shieldDurability += EntityVars.entityShieldRegen;
        }
        else
        {
            shieldDurability = maxShieldDurability;
        }
    }

    //checks if blocking/parrying is possible
    public void CheckIfBlockPossible()
    {
        if (entityInventory.entityLeftHandSlot.WeaponType == "Shield")
        {
            //if shield durability is less than 25%, unable to block
            if (((float)shieldDurability /(float)maxShieldDurability) < 0.25f)
            {
                isAbleToBlock = false;
            }
            else
            {
                isAbleToBlock = true;
            }

        }
        else
        {
            //if stamina is less than 25%, unable to parry
            if (((float)entityMain.currentEntityStamina / (float)entityInfo.maxEntityStamina) < 0.25f)
            {
                isAbleToParry = false;
            }
            else
            {
                isAbleToParry = true;
            }
        }
    }

    //check if shield/parry is broken through
    public void StopBlockChecker()
    {
        if (entityInventory.entityLeftHandSlot.WeaponType == "Shield")
        {
            if (shieldDurability <= 0)
            {
                shieldDurability = 0;
                entityAttackSys.LowerShield();
            }
        }
        else
        {
            if (entityMain.currentEntityStamina <= 0)
            {
                entityMain.currentEntityStamina = 0;
                entityAttackSys.StopParryWpn();
            }
        }
    }
}
