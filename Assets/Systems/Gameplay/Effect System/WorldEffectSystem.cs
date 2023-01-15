using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class WorldEffectSystem : MonoBehaviour {
    //this is the global effect manager
    //this bestows effects upon all entities that can recieve status effects



    //this bestows the appropriate effect(s) based on weapon info
    public static void BestowStatusEffect(string[] effectList, string[] effectTypeList, float[] effectChanceList,int[] effectDurations , string[] targets, GameObject Caster,GameObject[] EnemyTargets)
    {
        for (var effectNum = 0; effectNum < effectList.Length; effectNum++)
        {
            EntityStatusEffect statusEffect = new EntityStatusEffect();
            switch (effectTypeList[effectNum])
            {
                case "Duration":
                    statusEffect.EffectName = LoadDataBase.durationEffects[effectList[effectNum]]["Effect Name"].ToString();
                    statusEffect.EffectType = LoadDataBase.durationEffects[effectList[effectNum]]["Effect Type"].ToString();
                    statusEffect.EntityHealthChange = (int)LoadDataBase.durationEffects[effectList[effectNum]]["EntityHealthChange"];
                    statusEffect.EntityStaminaChange = (int)LoadDataBase.durationEffects[effectList[effectNum]]["EntityStaminaChange"];
                    statusEffect.EntityManaChange = (int)LoadDataBase.durationEffects[effectList[effectNum]]["EntityManaChange"];
                    statusEffect.EffectDuration = effectDurations[effectNum];
                    break;
                case "Buff":
                    statusEffect.EffectName = LoadDataBase.statModifyingEffects[effectList[effectNum]]["Effect Name"].ToString();
                    statusEffect.EffectType = LoadDataBase.statModifyingEffects[effectList[effectNum]]["Effect Type"].ToString();
                    statusEffect.EntityPhysDmgChange = ((int)LoadDataBase.statModifyingEffects[effectList[effectNum]]["EntityPhysDmgChange"]/100);
                    statusEffect.EntityMagDmgChange = ((int)LoadDataBase.statModifyingEffects[effectList[effectNum]]["EntityMagDmgChange"]/100);
                    statusEffect.EntitySpeedChange = ((int)LoadDataBase.statModifyingEffects[effectList[effectNum]]["EntitySpeedChange"]/100);
                    statusEffect.EntityCriticalChanceChange = ((int)LoadDataBase.statModifyingEffects[effectList[effectNum]]["CriticalChanceChange"]/100);
                    statusEffect.EntityArmorChange = ((int)LoadDataBase.statModifyingEffects[effectList[effectNum]]["ArmorChange"] / 100);
                    statusEffect.EntityMagicalArmorChange = ((int)LoadDataBase.statModifyingEffects[effectList[effectNum]]["MagicalArmorChange"] / 100);
                    statusEffect.IsStackable = (bool)LoadDataBase.statModifyingEffects[effectList[effectNum]]["Stackable"];
                    statusEffect.EffectDuration = effectDurations[effectNum];
                    break;
            }

            double generatedPercentage;
            //checks if effect is supposed to be applied to self or an enemy
            switch (targets[effectNum])
            {
                case "Self":
                    generatedPercentage = Random.Range(0f,100f);
                    if (generatedPercentage <= effectChanceList[effectNum])
                    {
                        foreach (EntityStatusEffect effect in Caster.GetComponent<EntityEffectSystem>().entityStatuses)
                        {
                            if (statusEffect.EffectName == effect.EffectName)
                            {
                                if (statusEffect.IsStackable == false)
                                {
                                    Caster.GetComponent<EntityEffectSystem>().entityStatuses.Remove(effect);
                                }
                            }
                        }
                        Caster.GetComponent<EntityEffectSystem>().entityStatuses.Add(statusEffect);
                    }
                    break;
                case "Enemy":                   
                    foreach (GameObject enemy in EnemyTargets)
                    {
                        generatedPercentage = Random.Range(0f, 100f);
                        if (generatedPercentage <= effectChanceList[effectNum])
                        {
                            foreach (EntityStatusEffect effect in enemy.GetComponent<EntityEffectSystem>().entityStatuses)
                            {
                                if (statusEffect.EffectName == effect.EffectName)
                                {
                                    if (statusEffect.IsStackable == false)
                                    {
                                        enemy.GetComponent<EntityEffectSystem>().entityStatuses.Remove(effect);
                                    }
                                }
                            }
                            enemy.GetComponent<EntityEffectSystem>().entityStatuses.Add(statusEffect);
                        }
                    }
                    break;
            }
        }
        /*
        //calculates probability of effect being bestowed
        int generatedInt = Random.Range(1, 101);

        if (generatedInt <= effectChance)
        {
            //replaces duplicates
            foreach (EntityStatusEffect effect in affectedPlayer.entityStatuses)
            {
                if (effect.EffectName == statusEffect.EffectName)
                {
                    if (effect.IsStackable == false)
                    {
                        affectedPlayer.entityStatuses.Remove(effect);
                    }
                }
            }
            affectedPlayer.entityStatuses.Add(statusEffect);
        }*/
    }

    private void Start()
    {

    }

    private void Update()
    {

    }

}
