using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Entity : MonoBehaviour {
    //attach to all entities

    //references
    public EntityInfo entityInfo;
    public EntityEffectSystem entityEffectsSystem;
    public Inventory entityInventory;
    public CombatListener entityCombatListener;
    public ShieldSystem entityShieldSystem;
    public OutfitManager entityOutfitManager;

    //reference to death handler
    public DeathHandler entityDeathHandler;

    //current entity stats
    //current stats
    public int currentEntityHealth = 100;
    public int currentEntityMana = 100;
    public int currentEntityStamina = 100;
    public float currentEntitySpeed = 3f;

    public int currentEntityArmor = 0;
    public int currentEntityMagicalArmor = 0;

    public int currentEntityPhysicalDamage = 0;
    public int currentEntityMagicalDamage = 0;
    public float currentEntityCritChance = 0;

    //current accumulated damage
    public float accumulatedDamage = 0;

    //is this entity alive?
    public bool entityIsALive = true;
   
	// Use this for initialization
	void Start () {
        entityInfo = gameObject.GetComponent<EntityInfo>();
        entityEffectsSystem = gameObject.GetComponent<EntityEffectSystem>();
        entityInventory = gameObject.GetComponent<Inventory>();
        entityCombatListener = gameObject.GetComponent<CombatListener>();
        entityShieldSystem = gameObject.GetComponent<ShieldSystem>();
        entityDeathHandler = gameObject.GetComponent<DeathHandler>();
        entityOutfitManager = gameObject.GetComponent<OutfitManager>();
        InvokeRepeating("TakeConstantDamage",0.5f,0.5f);
	}
	
	// Update is called once per frame
	void Update () { 
        //does not update stats if entity is just for testing
        if (gameObject.tag != "Test Entity")
        {
            UpdateStats();
        }
        //this entity has died
        if (currentEntityHealth <= 0 && entityIsALive)
        {
            entityIsALive = false;
            entityDeathHandler.ActivateDeathSequence();
            //if entity has a lootable inventory
            if (gameObject.GetComponent<LootableInventory>())
            {
                gameObject.GetComponent<LootableInventory>().isInventoryLootable = true;
            }
        }      
    }

    //update stats
    void UpdateStats()
    {
        currentEntitySpeed = UpdateStat( entityInfo.baseEntitySpeed, entityInventory.totalSpeedAdded, entityOutfitManager.outfitAddedSpeed, entityEffectsSystem.entitySpeedMult);
        currentEntityPhysicalDamage = UpdateStat( entityInfo.baseEntityPhysicalDamage, entityInventory.totalPhysicalDamageAdded, entityOutfitManager.outfitAddedPhysicalDamage, entityEffectsSystem.physicalDmgMult);
        currentEntityMagicalDamage = UpdateStat( entityInfo.baseEntityMagicalDamage, entityInventory.totalMagicalDamageAdded, entityOutfitManager.outfitAddedMagicalDamage, entityEffectsSystem.magicalDmgMult);
        currentEntityCritChance = UpdateStat(entityInfo.baseEntityCriticalChance, entityInventory.totalCritChanceAdded, entityOutfitManager.outfitAddedCritChance, entityEffectsSystem.criticalChanceMult);

        currentEntityArmor = UpdateStat(entityInfo.baseEntityArmor, entityInventory.totalArmorAdded, entityOutfitManager.outfitAddedArmor, entityEffectsSystem.armorMult);
        currentEntityMagicalArmor = UpdateStat(entityInfo.baseEntityMagicalArmor, entityInventory.totalMagicalArmorAdded, entityOutfitManager.outfitAddedMagicalArmor, entityEffectsSystem.magicalArmorMult);

    }

    //update a stat
    int UpdateStat( float baseStat, float addedByInventory, float addedByOutfit, float statMultiplier)
    {
        int modifiedStat = Mathf.RoundToInt((baseStat + addedByInventory + addedByOutfit) * statMultiplier);
        return modifiedStat;
    }

    //handler for taking physical damage; calculates total taken damage after all resistances are calculated
    public void TakeDamage(int rawDamage, string damageType, bool isPowerAttk, bool isRanged)
    {
        //calculates the multiplier; based on this entity's defence against recieved raw damage
        float dmgMultiplier;
        int damageBase = rawDamage;
        switch (damageType)
        {
            case "Physical":
                dmgMultiplier = rawDamage / currentEntityArmor;                
                break;
            case "Magical":
                dmgMultiplier = rawDamage / currentEntityMagicalArmor;
                break;
            default:
                //error-incorrect damage type string
                dmgMultiplier = 0;
                break;
        }

        //applies multiplier to raw damage
        int calculatedDMG = Mathf.RoundToInt(damageBase * dmgMultiplier);
        //applies slight randomizer to calculcated damage
        float damageRandomizer = (float)Math.Round(UnityEngine.Random.Range(-0.1f, 0.1f), 2);
        //calculates final damage
        int finalDmg = calculatedDMG + Mathf.RoundToInt(calculatedDMG * damageRandomizer);

        //applies modifiers if entity is blocking in some way
        if (entityShieldSystem.isBlocking)
        {
            if ((entityShieldSystem.shieldDurability - finalDmg) >= 0)
            {
                entityShieldSystem.shieldDurability -= finalDmg;
                finalDmg = Mathf.RoundToInt(finalDmg * 0.15f);
            }
            else
            {
                int tempFinalDamage = Mathf.Abs(entityShieldSystem.shieldDurability - finalDmg);
                int tempNullfiedDamage = finalDmg - Mathf.Abs(entityShieldSystem.shieldDurability - finalDmg);
                entityShieldSystem.shieldDurability -= finalDmg;
                finalDmg = tempFinalDamage + Mathf.RoundToInt(tempNullfiedDamage * 0.15f);
            }
        }
        if (entityShieldSystem.isParrying && isRanged == false)
        {
            if ((currentEntityStamina - finalDmg) >= 0)
            {
                currentEntityStamina -= finalDmg;
                finalDmg = Mathf.RoundToInt(finalDmg * 0.35f);
            }
            else
            {
                int tempFinalDamage = Mathf.Abs(currentEntityStamina - finalDmg);
                int tempNullfiedDamage = finalDmg - Mathf.Abs(currentEntityStamina - finalDmg);
                currentEntityStamina -= finalDmg;
                finalDmg = tempFinalDamage + Mathf.RoundToInt(tempNullfiedDamage * 0.35f);
            }
        }

        //subtract health here
        currentEntityHealth -= finalDmg;
        entityCombatListener.CombatEngaged();
        Debug.Log(finalDmg);
    }

    //handler for taking constant damage; only handles magic resistance. DO NOT ADD ANY RAPID FIRE PHYS DMG WEAPONS
    public void TakeConstantDamage()
    {
        if (accumulatedDamage > 0)
        {
            float dmgMultiplier;
            int finalDmg;

            dmgMultiplier = accumulatedDamage / (currentEntityMagicalArmor * 0.5f);
            finalDmg = CalculateCritical(Mathf.RoundToInt(accumulatedDamage * dmgMultiplier));
            //subtract health here
            currentEntityHealth -= finalDmg;
            //reset value
            accumulatedDamage = 0;
            entityCombatListener.CombatEngaged();
            //updates HUD if player
        }        
    }

    //adds to constant damage
    public void AddToConstantDamage(float rawDmgAdded)
    { 
        accumulatedDamage += rawDmgAdded;
        Debug.Log(accumulatedDamage);
    }

    //critical calculator
    public int CalculateCritical(int baseDamage)
    {
        int appliedDmg;
        float CalcChance = UnityEngine.Random.Range(0.0f, 100.1f);
        if (CalcChance <= currentEntityCritChance)
        {
            //critical hit lands; x2 damage
            appliedDmg = (baseDamage * 2);
        }
        else
        {
            //no critical hit; regular damage
            appliedDmg = baseDamage;
        }
        return appliedDmg;
    }


}
