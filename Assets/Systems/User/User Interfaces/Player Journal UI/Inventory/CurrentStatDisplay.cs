using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentStatDisplay : MonoBehaviour {
    //attach this to journal UI system manager
    //updates current stats in real time

    //reference to player stats
    public Entity EntityCurrentInfo;
    public EntityInfo EntityBaseInfo;
    public Inventory EntityInventoryInfo;

    //stat display texts
    public Text physDamage;
    public Text magiDamage;
    public Text physArmor;
    public Text magiArmor;
    public Text playerSpeed;
    public Text CriticalChance;





	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        UpdateValues();
	}

    //updates all values
    public void UpdateValues()
    {
        UpdateValue(physDamage,EntityCurrentInfo.currentEntityPhysicalDamage);
        UpdateValue(magiDamage, EntityCurrentInfo.currentEntityMagicalDamage);
        UpdateValue(physArmor, EntityCurrentInfo.currentEntityArmor);
        UpdateValue(magiArmor, EntityCurrentInfo.currentEntityMagicalArmor );
        UpdateValue(playerSpeed, EntityCurrentInfo.currentEntitySpeed);
        CriticalChance.text = EntityCurrentInfo.currentEntityCritChance.ToString() + "%";

    }

    //updates a value
    void UpdateValue(Text valueDisplay,float currentValue)
    {
        valueDisplay.text = currentValue.ToString();
    }
}
