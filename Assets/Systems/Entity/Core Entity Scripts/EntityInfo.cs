using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityInfo : MonoBehaviour {
    //attach to all entities with stats
    //this contains the base stats of the entity and these values are the ones that should be modified when applying permanent stat changes

    //base 
    //this entity's base stats
    public int baseEntityHealth = 100;
    public int baseEntityMana = 50;
    public int baseEntityStamina = 50;
    public float baseEntitySpeed = 3f;
    
    public int baseEntityArmor = 0;
    public int baseEntityMagicalArmor = 0;

    public int baseEntityPhysicalDamage = 0;
    public int baseEntityMagicalDamage = 0;
    public float baseEntityCriticalChance = 0;


    //attribute points
    public int strengthAttr = 0;
    public int intelligenceAttr = 0;
    public int enduranceAttr = 0;
    public int willpowerAttr = 0;
    public int luckAttr = 0;

    public int availableAttrPoints = 0;

    //maximum
    //this entity's max stats
    public int maxEntityHealth = 100;
    public int maxEntityMana = 100;
    public int maxEntityStamina = 100;

    //updates
    private void Update()
    {
        UpdateMaxStats();
    }

    //update values
    void UpdateMaxStats()
    {
        maxEntityHealth = CalculateStat( baseEntityHealth, gameObject.GetComponent<Inventory>().totalHealthAdded, gameObject.GetComponent<OutfitManager>().outfitAddedHealth);
        maxEntityMana = CalculateStat( baseEntityMana, gameObject.GetComponent<Inventory>().totalManaAdded, gameObject.GetComponent<OutfitManager>().outfitAddedMana);
        maxEntityStamina = CalculateStat( baseEntityStamina, gameObject.GetComponent<Inventory>().totalStaminaAdded, gameObject.GetComponent<OutfitManager>().outfitAddedStamina);


        
    }

    //update a stat
    int CalculateStat(int baseValue, int addedValue, int addedOutfitValue)
    {
        int modifiedValue = baseValue + addedValue + addedOutfitValue;
        return modifiedValue;
    }

    //updates base stats
    public void UpdateBaseStats()
    {
        baseEntityHealth = 100 + (enduranceAttr * 3);
        baseEntityMana = 50 + (intelligenceAttr) + (willpowerAttr * 3);
        baseEntityStamina = 50 + (strengthAttr) + (enduranceAttr) + (willpowerAttr);
        //baseEntitySpeed

        baseEntityArmor = (strengthAttr) + (enduranceAttr);
        baseEntityMagicalArmor = (intelligenceAttr) + (willpowerAttr);

        baseEntityPhysicalDamage = (strengthAttr * 3);
        baseEntityMagicalDamage = (intelligenceAttr * 3);
        baseEntityCriticalChance = (luckAttr * 0.05f);


    }




}
