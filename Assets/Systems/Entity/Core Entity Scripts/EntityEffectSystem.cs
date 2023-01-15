using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityEffectSystem : MonoBehaviour {
    //attach to an entity that recieves status effects 
    //handles both status effects and duration based effects

    //reference to entity that this component is attached to
    public Entity entityMain;

    //list containing all active effects for this entity
    public List<EntityStatusEffect> entityStatuses = new List<EntityStatusEffect>();

    //multipliers for stats(except for health, mana and stamina
    public float entitySpeedMult = 1;
    public float physicalDmgMult = 1;
    public float magicalDmgMult = 1;
    public float criticalChanceMult = 1;

    public float armorMult = 1;
    public float magicalArmorMult = 1;


    //constant changes for health, mana and stamina per second
    public int healthChange = 0;
    public int staminaChange = 0;
    public int manaChange = 0;


	// Use this for initialization
	void Start () {
        entityMain = gameObject.GetComponent<Entity>();
        InvokeRepeating("DurationEffectHandler", 0f, 1.0f);
        InvokeRepeating("StatusEffectHandler",0f,1.0f);
    }
	
	// Update is called once per frame
	void Update () {

        
	}

    //stat change based status effect listener; updates stats in real time
    public void StatusEffectHandler()
    {
        ResetAllValues();
        if (entityStatuses.Count > 0)
        {
            foreach (EntityStatusEffect effect in entityStatuses)
            {
                if (effect.EffectType == "Status")
                {
                    entitySpeedMult += effect.EntitySpeedChange;
                    physicalDmgMult += effect.EntityPhysDmgChange;
                    magicalDmgMult += effect.EntityPhysDmgChange;
                    criticalChanceMult += effect.EntityCriticalChanceChange;
                    armorMult += effect.EntityArmorChange;
                    magicalArmorMult += effect.EntityMagicalArmorChange;

                    effect.EffectDuration -= 1;
                    //removes effect
                    if (effect.EffectDuration <= 0)
                    {
                        entityStatuses.Remove(effect);
                    }
                }
            }
        }
    }

    //duration based effect handler; usually called every second
    public void DurationEffectHandler()
    {
        ResetAllValues();
        if (entityStatuses.Count > 0)
        {
            foreach (EntityStatusEffect effect in entityStatuses)
            {
                if (effect.EffectType == "Duration")
                {
                    healthChange += effect.EntityHealthChange;
                    staminaChange += effect.EntityStaminaChange;
                    manaChange += effect.EntityManaChange;

                    effect.EffectDuration -= 1;
                    //removes effect
                    if (effect.EffectDuration <= 0)
                    {
                        entityStatuses.Remove(effect);
                    }
                }
            }
            entityMain.currentEntityHealth += healthChange;
            entityMain.currentEntityStamina += staminaChange;
            entityMain.currentEntityMana += manaChange;
        }

    }

    //resets every multiplier and duration effect
    public void ResetAllValues()
    {
        entitySpeedMult = 1;
        physicalDmgMult = 1;
        magicalDmgMult = 1;
        criticalChanceMult = 1;

        armorMult = 1;
        magicalArmorMult = 1;

        healthChange = 0;
        staminaChange = 0;
        manaChange = 0;
    }

}
