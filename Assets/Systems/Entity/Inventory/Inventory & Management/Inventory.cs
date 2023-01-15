using System.Collections.Generic;
using UnityEngine;
using LitJson;


public class Inventory : MonoBehaviour {
    //attach this to all entities with an inventory
    
    // this entity's equip slots
    public ItemData entityHeadSlot;
    public ItemData entityTorsoSlot;
    public ItemData entityLegsSlot;
    public ItemData entityFeetSlot;

    public ItemData entityLeftHandSlot;
    public ItemData entityRightHandSlot;

    public ItemData entityNecklaceSlot;
    public ItemData entityLeftRingSlot;
    public ItemData entityRightRingSlot;

    public List<ItemData> AmmoQuiver = new List<ItemData>();

    //this entity's inventory
    public List<ItemData> entityInventory = new List<ItemData>();

    //total stats added
    public int totalPhysicalDamageAdded = 0;
    public int totalMagicalDamageAdded = 0;

    public int totalArmorAdded = 0;
    public int totalMagicalArmorAdded = 0;

    public int totalHealthAdded = 0;
    public int totalStaminaAdded = 0;
    public int totalManaAdded = 0;
    public float totalSpeedAdded = 0;
    public float totalCritChanceAdded = 0;

    //this entity's carried amount of money
    public int goldCoins = 1000;

	// Use this for initialization
	void Start () {
        InitInventory();
        entityInventory.Add(JsonToItemData.ParseItemFromDatabase(LoadDataBase.ringList["Test Ring"]));
        entityInventory.Add(JsonToItemData.ParseItemFromDatabase(LoadDataBase.headGearList["Test Helmet"]));
        entityInventory.Add(JsonToItemData.ParseItemFromDatabase(LoadDataBase.torsoGearList["Test ChestPlate"]));
        entityInventory.Add(JsonToItemData.ParseItemFromDatabase(LoadDataBase.legGearList["Test Greaves"]));
        entityInventory.Add(JsonToItemData.ParseItemFromDatabase(LoadDataBase.footGearList["Test Boots"]));
        entityInventory.Add(JsonToItemData.ParseItemFromDatabase(LoadDataBase.necklaceList["Test Necklace"]));
        entityInventory.Add(JsonToItemData.ParseItemFromDatabase(LoadDataBase.shieldList["Test Shield"]));
        entityInventory.Add(JsonToItemData.ParseItemFromDatabase(LoadDataBase.staffsList["Test Staff"]));
        entityInventory.Add(JsonToItemData.ParseItemFromDatabase(LoadDataBase.rawMaterialList["Debug Material"]));
        entityInventory.Add(JsonToItemData.ParseItemFromDatabase(LoadDataBase.consumableItemList["Debug Consumable"]));
        entityInventory.Add(JsonToItemData.ParseItemFromDatabase(LoadDataBase.oneHandedWeaponsList["Test Sword"]));
        entityInventory.Add(JsonToItemData.ParseItemFromDatabase(LoadDataBase.oneHandedWeaponsList["Test Sword"]));
        entityInventory.Add(JsonToItemData.ParseItemFromDatabase(LoadDataBase.oneHandedWeaponsList["Test Sword"]));
        entityInventory.Add(JsonToItemData.ParseItemFromDatabase(LoadDataBase.oneHandedWeaponsList["Test Sword"]));
        entityInventory.Add(JsonToItemData.ParseItemFromDatabase(LoadDataBase.oneHandedWeaponsList["Test Sword"]));
        entityInventory.Add(JsonToItemData.ParseItemFromDatabase(LoadDataBase.oneHandedWeaponsList["Test Spear"]));
        entityInventory.Add(JsonToItemData.ParseItemFromDatabase(LoadDataBase.twoHandedWeaponsList["Test Greatsword"]));
        entityInventory.Add(JsonToItemData.ParseItemFromDatabase(LoadDataBase.projectileWeaponsList["Test Bow"]));
        entityInventory.Add(JsonToItemData.ParseItemFromDatabase(LoadDataBase.projectileWeaponsList["Test Crossbow"]));
        entityInventory.Add(JsonToItemData.ParseItemFromDatabase(LoadDataBase.ammunitionList["Test Arrow"]));
        entityInventory.Add(JsonToItemData.ParseItemFromDatabase(LoadDataBase.ammunitionList["Test Arrow"]));
        entityInventory.Add(JsonToItemData.ParseItemFromDatabase(LoadDataBase.ammunitionList["Test Arrow"]));
        entityInventory.Add(JsonToItemData.ParseItemFromDatabase(LoadDataBase.ammunitionList["Test Bolt"]));
        entityInventory.Add(JsonToItemData.ParseItemFromDatabase(LoadDataBase.ammunitionList["Test Bolt"]));
        entityInventory.Add(JsonToItemData.ParseItemFromDatabase(LoadDataBase.ammunitionList["Test Bolt"]));
        entityInventory.Add(JsonToItemData.ParseItemFromDatabase(LoadDataBase.tomesList["Test Charge Tome"]));
        entityInventory.Add(JsonToItemData.ParseItemFromDatabase(LoadDataBase.tomesList["Test Constant Tome"]));
    }

    // Update is called once per frame
    void Update () {
        UpdateStatBoosts();

	}

    //initializes inventory
    void InitInventory()
    {
        entityHeadSlot = JsonToItemData.ParseItemFromDatabase(LoadDataBase.headGearList["No Helmet"]);
        entityTorsoSlot = JsonToItemData.ParseItemFromDatabase(LoadDataBase.torsoGearList["No ChestPlate"]);
        entityLegsSlot = JsonToItemData.ParseItemFromDatabase(LoadDataBase.legGearList["No Greaves"]);
        entityFeetSlot = JsonToItemData.ParseItemFromDatabase(LoadDataBase.footGearList["No Boots"]);
        entityLeftHandSlot = JsonToItemData.ParseItemFromDatabase(LoadDataBase.oneHandedWeaponsList["Empty Item Slot"]);
        entityRightHandSlot = JsonToItemData.ParseItemFromDatabase(LoadDataBase.twoHandedWeaponsList["Bare Hands"]);
        entityNecklaceSlot = JsonToItemData.ParseItemFromDatabase(LoadDataBase.necklaceList["No Necklace"]);
        entityLeftRingSlot = JsonToItemData.ParseItemFromDatabase(LoadDataBase.ringList["No Ring"]);
        entityRightRingSlot = JsonToItemData.ParseItemFromDatabase(LoadDataBase.ringList["No Ring"]);
    }

    //updates stat boosts given by equippables and consumables
    void UpdateStatBoosts()
    {
        totalPhysicalDamageAdded = (int)CalculateAddedStats("Physical Damage");
        totalMagicalDamageAdded = (int)CalculateAddedStats( "Magical Damage");
        totalArmorAdded = (int)CalculateAddedStats( "Armor");
        totalMagicalArmorAdded = (int)CalculateAddedStats("Magic Resistance");
        totalHealthAdded = (int)CalculateAddedStats( "Health");
        totalStaminaAdded = (int)CalculateAddedStats( "Stamina");
        totalManaAdded = (int)CalculateAddedStats( "Mana");
        totalSpeedAdded = CalculateAddedStats( "Speed");
        totalCritChanceAdded = CalculateAddedStats("Critical Chance");


    }

    //calculate added stats
    float CalculateAddedStats(string dataType)
    {
        float value = 0;
        switch (dataType)
        {
            case "Physical Damage":
                value = entityHeadSlot.PhysicalDamage + entityTorsoSlot.PhysicalDamage
                + entityLegsSlot.PhysicalDamage + entityFeetSlot.PhysicalDamage + entityLeftHandSlot.PhysicalDamage
                + entityRightHandSlot.PhysicalDamage + entityNecklaceSlot.PhysicalDamage
                + entityLeftRingSlot.PhysicalDamage + entityRightRingSlot.PhysicalDamage;
                break;
            case "Magical Damage":
                value = entityHeadSlot.MagicalDamage + entityTorsoSlot.MagicalDamage
                + entityLegsSlot.MagicalDamage + entityFeetSlot.MagicalDamage + entityLeftHandSlot.MagicalDamage
                + entityRightHandSlot.MagicalDamage + entityNecklaceSlot.MagicalDamage
                + entityLeftRingSlot.MagicalDamage + entityRightRingSlot.MagicalDamage;
                break;
            case "Armor":
                value = entityHeadSlot.Armor + entityTorsoSlot.Armor
                + entityLegsSlot.Armor + entityFeetSlot.Armor + entityLeftHandSlot.Armor
                + entityRightHandSlot.Armor + entityNecklaceSlot.Armor
                + entityLeftRingSlot.Armor + entityRightRingSlot.Armor;
                break;
            case "Magic Resistance":
                value = entityHeadSlot.MagicResistance + entityTorsoSlot.MagicResistance
                + entityLegsSlot.MagicResistance + entityFeetSlot.MagicResistance + entityLeftHandSlot.MagicResistance
                + entityRightHandSlot.MagicResistance + entityNecklaceSlot.MagicResistance
                + entityLeftRingSlot.MagicResistance + entityRightRingSlot.MagicResistance;
                break;
            case "Health":
                value = entityHeadSlot.Health + entityTorsoSlot.Health
                + entityLegsSlot.Health + entityFeetSlot.Health + entityLeftHandSlot.Health
                + entityRightHandSlot.Health + entityNecklaceSlot.Health
                + entityLeftRingSlot.Health + entityRightRingSlot.Health;
                break;
            case "Stamina":
                value = entityHeadSlot.Stamina + entityTorsoSlot.Stamina
                + entityLegsSlot.Stamina + entityFeetSlot.Stamina + entityLeftHandSlot.Stamina
                + entityRightHandSlot.Stamina + entityNecklaceSlot.Stamina
                + entityLeftRingSlot.Stamina + entityRightRingSlot.Stamina;
                break;
            case "Mana":
                value = entityHeadSlot.Mana + entityTorsoSlot.Mana
                + entityLegsSlot.Mana + entityFeetSlot.Mana + entityLeftHandSlot.Mana
                + entityRightHandSlot.Mana + entityNecklaceSlot.Mana
                + entityLeftRingSlot.Mana + entityRightRingSlot.Mana;
                break;
            case "Speed":
                value = entityHeadSlot.Speed + entityTorsoSlot.Speed
                + entityLegsSlot.Speed + entityFeetSlot.Mana + entityLeftHandSlot.Speed
                + entityRightHandSlot.Speed + entityNecklaceSlot.Speed
                + entityLeftRingSlot.Speed + entityRightRingSlot.Speed;
                break;
            case "Critical Chance":
                value = entityHeadSlot.CriticalChance + entityTorsoSlot.CriticalChance
                + entityLegsSlot.CriticalChance + entityFeetSlot.CriticalChance + entityLeftHandSlot.CriticalChance
                + entityRightHandSlot.CriticalChance + entityNecklaceSlot.CriticalChance
                + entityLeftRingSlot.CriticalChance + entityRightRingSlot.CriticalChance;
                break;
        }

        return value;
    }


}
