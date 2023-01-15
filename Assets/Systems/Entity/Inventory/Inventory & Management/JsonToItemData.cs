using System;
using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class JsonToItemData : MonoBehaviour {
    //static class that converts litjson jsondata to itemdata class

    //parses jsondata from databases and returns an ItemData class instance with the info
    public static ItemData ParseItemFromDatabase(JsonData itemRaw)
    {
        ItemData exportedItemData = new ItemData();

        //assigns item description, type and name
        exportedItemData.ItemType = itemRaw["Type"].ToString();
        exportedItemData.ItemName = itemRaw["Item Name"].ToString();
        exportedItemData.ItemDescription = itemRaw["Item Description"].ToString();
      
        switch (itemRaw["Type"].ToString())
        {
            case "Armor":
                //if item is an armor
                exportedItemData.ArmorType = itemRaw["Armor Type"].ToString();
                exportedItemData.Armor = (int)itemRaw["Armor"];
                exportedItemData.MagicResistance = (int)itemRaw["Magic Resistance"];
                exportedItemData.Health = (int)itemRaw["Health"];
                exportedItemData.Stamina = (int)itemRaw["Stamina"];
                exportedItemData.Mana = (int)itemRaw["Mana"];
                exportedItemData.Speed = ((int)itemRaw["Speed"])/10f;
                exportedItemData.PhysicalDamage = (int)itemRaw["Physical Damage"];
                exportedItemData.MagicalDamage = (int)itemRaw["Magical Damage"];
                exportedItemData.CriticalChance = ((int)itemRaw["Critical Chance"]) / 10f;
                break;
            case "Weapon":
                exportedItemData.WeaponType = itemRaw["Weapon Type"].ToString();
                if (exportedItemData.WeaponType == "Tome")
                {
                    exportedItemData.EffectNameList = ParseFromJsonArrayStr(itemRaw["Effect Name"]);
                    exportedItemData.EffectTypeList = ParseFromJsonArrayStr(itemRaw["Effect Type"]);
                    exportedItemData.EffectTargetList = ParseFromJsonArrayStr(itemRaw["Effect Target"]);
                    exportedItemData.EffectChanceList = ParseFromJsonArrayFloat(itemRaw["Effect Chance"]);
                    exportedItemData.EffectDurationList = ParseFromJsonArrayInt(itemRaw["Effect Duration"]);

                    exportedItemData.CombatType = itemRaw["Combat Type"].ToString();
                    exportedItemData.Armor = (int)itemRaw["Armor"];
                    exportedItemData.MagicResistance = (int)itemRaw["Magic Resistance"];
                    exportedItemData.Health = (int)itemRaw["Health"];
                    exportedItemData.Stamina = (int)itemRaw["Stamina"];
                    exportedItemData.Mana = (int)itemRaw["Mana"];
                    exportedItemData.Speed = ((int)itemRaw["Speed"]) / 10f;
                    exportedItemData.PhysicalDamage = (int)itemRaw["Physical Damage"];
                    exportedItemData.MagicalDamage = (int)itemRaw["Magical Damage"];
                    exportedItemData.CriticalChance = ((int)itemRaw["Critical Chance"]) / 10f;
                    exportedItemData.MagicType = itemRaw["Magic Type"].ToString();
                    exportedItemData.ChargeTime = ((int)itemRaw["Charge Time"]) / 10f;

                }
                else if (exportedItemData.WeaponType == "Ammunition")
                {
                    exportedItemData.AmmunitionType = itemRaw["Ammunition Type"].ToString();
                    exportedItemData.PhysicalDamage = (int)itemRaw["Physical Damage"];
                }              
                else if (exportedItemData.WeaponType == "Shield")
                {
                    exportedItemData.CombatType = itemRaw["Combat Type"].ToString();
                    exportedItemData.Armor = (int)itemRaw["Armor"];
                    exportedItemData.MagicResistance = (int)itemRaw["Magic Resistance"];
                    exportedItemData.Health = (int)itemRaw["Health"];
                    exportedItemData.Stamina = (int)itemRaw["Stamina"];
                    exportedItemData.Mana = (int)itemRaw["Mana"];
                    exportedItemData.Speed = ((int)itemRaw["Speed"]) / 10f;
                    exportedItemData.PhysicalDamage = (int)itemRaw["Physical Damage"];
                    exportedItemData.MagicalDamage = (int)itemRaw["Magical Damage"];
                    exportedItemData.CriticalChance = ((int)itemRaw["Critical Chance"]) / 10f;
                    exportedItemData.ShieldDurability = (int)itemRaw["Shield Durability"];
                }
                else if (exportedItemData.WeaponType == "Staff"|| exportedItemData.WeaponType == "Projectile Weapon")
                {
                    exportedItemData.EffectNameList = ParseFromJsonArrayStr(itemRaw["Effect Name"]);
                    exportedItemData.EffectTypeList = ParseFromJsonArrayStr(itemRaw["Effect Type"]);
                    exportedItemData.EffectTargetList = ParseFromJsonArrayStr(itemRaw["Effect Target"]);
                    exportedItemData.EffectChanceList = ParseFromJsonArrayFloat(itemRaw["Effect Chance"]);
                    exportedItemData.EffectDurationList = ParseFromJsonArrayInt(itemRaw["Effect Duration"]);

                    exportedItemData.CombatType = itemRaw["Combat Type"].ToString();
                    exportedItemData.Armor = (int)itemRaw["Armor"];
                    exportedItemData.MagicResistance = (int)itemRaw["Magic Resistance"];
                    exportedItemData.Health = (int)itemRaw["Health"];
                    exportedItemData.Stamina = (int)itemRaw["Stamina"];
                    exportedItemData.Mana = (int)itemRaw["Mana"];
                    exportedItemData.Speed = ((int)itemRaw["Speed"]) / 10f;
                    exportedItemData.PhysicalDamage = (int)itemRaw["Physical Damage"];
                    exportedItemData.MagicalDamage = (int)itemRaw["Magical Damage"];
                    exportedItemData.CriticalChance = ((int)itemRaw["Critical Chance"]) / 10f;
                    exportedItemData.WeaponSpeed = ((int)itemRaw["Weapon Speed"]) / 100f;

                }
                else
                {
                    exportedItemData.EffectNameList = ParseFromJsonArrayStr(itemRaw["Effect Name"]);
                    exportedItemData.EffectTypeList = ParseFromJsonArrayStr(itemRaw["Effect Type"]);
                    exportedItemData.EffectTargetList = ParseFromJsonArrayStr(itemRaw["Effect Target"]);
                    exportedItemData.EffectChanceList = ParseFromJsonArrayFloat(itemRaw["Effect Chance"]);
                    exportedItemData.EffectDurationList = ParseFromJsonArrayInt(itemRaw["Effect Duration"]);

                    exportedItemData.CombatType = itemRaw["Combat Type"].ToString();
                    exportedItemData.Armor = (int)itemRaw["Armor"];
                    exportedItemData.MagicResistance = (int)itemRaw["Magic Resistance"];
                    exportedItemData.Health = (int)itemRaw["Health"];
                    exportedItemData.Stamina = (int)itemRaw["Stamina"];
                    exportedItemData.Mana = (int)itemRaw["Mana"];
                    exportedItemData.Speed = ((int)itemRaw["Speed"]) / 10f;
                    exportedItemData.PhysicalDamage = (int)itemRaw["Physical Damage"];
                    exportedItemData.MagicalDamage = (int)itemRaw["Magical Damage"];
                    exportedItemData.CriticalChance = ((int)itemRaw["Critical Chance"]) / 10f;
                    exportedItemData.WeaponReach = ((int)itemRaw["Weapon Reach"]) / 100f;
                    exportedItemData.WeaponSpeed = ((int)itemRaw["Weapon Speed"]) / 100f;
                }
                break;
            case "Consumable":
                //if item is a consumable
                exportedItemData.EffectNameList = ParseFromJsonArrayStr(itemRaw["Effect Name"]);
                exportedItemData.EffectTypeList = ParseFromJsonArrayStr(itemRaw["Effect Type"]);
                exportedItemData.EffectTargetList = ParseFromJsonArrayStr(itemRaw["Effect Target"]);
                exportedItemData.EffectChanceList = ParseFromJsonArrayFloat(itemRaw["Effect Chance"]);
                exportedItemData.EffectDurationList = ParseFromJsonArrayInt(itemRaw["Effect Duration"]);

                exportedItemData.Health = (int)itemRaw["Health"];
                exportedItemData.Stamina = (int)itemRaw["Stamina"];
                exportedItemData.Mana = (int)itemRaw["Mana"];



                break;
            case "Raw Material":
                //if item is a raw material

                break;
            case "Quest Item":
                //if item is a quest item

                break;

        }

        //parses item data
        if (itemRaw["Type"].ToString() == "Outfit")
        {
            exportedItemData.coversHair = (bool)itemRaw["Covers Hair"];
            exportedItemData.coversFacialHair = (bool)itemRaw["Covers Facial Hair"];
            exportedItemData.coversHead = (bool)itemRaw["Covers Head"];
            exportedItemData.coversArms = (bool)itemRaw["Covers Arms"];
            exportedItemData.coversHands = (bool)itemRaw["Covers Hands"];
            exportedItemData.coversTorso = (bool)itemRaw["Covers Torso"];
            exportedItemData.coversLegs = (bool)itemRaw["Covers Legs"];
            exportedItemData.coversFeet = (bool)itemRaw["Covers Feet"];
        }

        return exportedItemData;
    }

    static string[] ParseFromJsonArrayStr(JsonData rawJsonArray)
    {
        List<string> parsedArrayStr = new List<string>();
        for (int item = 0; item < rawJsonArray.Count; item++)
        {
            parsedArrayStr.Add(rawJsonArray[item].ToString());
        }

        return parsedArrayStr.ToArray();
    }
	
    static int[] ParseFromJsonArrayInt(JsonData rawJsonArray)
    {
        List<int> parsedArrayInt = new List<int>();
        for (int item = 0; item < rawJsonArray.Count; item++)
        {
            parsedArrayInt.Add((int)rawJsonArray[item]);
        }

        return parsedArrayInt.ToArray();
    }

    static float[] ParseFromJsonArrayFloat(JsonData rawJsonArray)
    {
        List<float> parsedArrayFloat = new List<float>();
        for (int item = 0; item < rawJsonArray.Count; item++)
        {
            parsedArrayFloat.Add(((int)rawJsonArray[item])/10);
        }

        return parsedArrayFloat.ToArray();
    }
}
