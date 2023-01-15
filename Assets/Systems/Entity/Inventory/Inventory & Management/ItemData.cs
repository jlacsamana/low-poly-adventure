using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ItemData {
    //this stores information of items; retrieved from a Jsondata

    //generic item info
    public string ItemType { get; set; }
    public string ItemName { get; set; }
    public string ItemDescription { get; set; }

    //additional info if item is an equipment piece
    public string WeaponType = "";
    public string CombatType = "";
    public int Armor = 0;
    public int MagicResistance = 0;
    public int Health = 0;
    public int Stamina = 0;
    public int Mana = 0;
    public float Speed = 0f;
    public int PhysicalDamage = 0;
    public int MagicalDamage = 0;
    public float CriticalChance = 0f;

    //additional info if item is a weapon
    public float WeaponReach = 0f;
    public float WeaponSpeed = 1.0f;

    //additional info if item is an armor
    public string ArmorType = "";

    //additional info if item is ammunition
    public string AmmunitionType = "";

    //additional info if item is a tome
    public string MagicType = "";
    public float ChargeTime = 0f;

    //additional info if item is a shield
    public int ShieldDurability = 0;


    //effect info
    public string[] EffectTypeList = { };
    public string[] EffectNameList = { };
    public string[] EffectTargetList = { };
    public float[] EffectChanceList = { };
    public int[] EffectDurationList = { };

    //outfit data
    public bool coversHair = false;
    public bool coversFacialHair = false;
    public bool coversHead = false;
    public bool coversArms = false;
    public bool coversHands = false;
    public bool coversTorso = false;
    public bool coversLegs = false;
    public bool coversFeet = false;

    //checks if item is the same
    public bool CheckIfEqual(ItemData otherItem)
    {        
        bool isEqual = false;
        if ((otherItem.EffectTypeList != null && otherItem.EffectNameList != null &&
            otherItem.EffectChanceList != null && otherItem.EffectDurationList != null
            ) && otherItem.ItemType == "Weapon")
        {
            if (ItemType == otherItem.ItemType && ItemName == otherItem.ItemName && ItemDescription == otherItem.ItemDescription &&
            WeaponType == otherItem.WeaponType && CombatType == otherItem.CombatType && Armor == otherItem.Armor &&
            MagicResistance == otherItem.MagicResistance && Health == otherItem.Health && Stamina == otherItem.Stamina &&
            Mana == otherItem.Mana && Speed == otherItem.Speed && PhysicalDamage == otherItem.PhysicalDamage &&
            MagicalDamage == otherItem.MagicalDamage && CriticalChance == otherItem.CriticalChance &&
            WeaponReach == otherItem.WeaponReach && ArmorType == otherItem.ArmorType &&
            AmmunitionType == otherItem.AmmunitionType && MagicType == otherItem.MagicType &&
            ChargeTime == otherItem.ChargeTime && ShieldDurability == otherItem.ShieldDurability
            && EffectTypeList.SequenceEqual(otherItem.EffectTypeList) &&
            EffectNameList.SequenceEqual(otherItem.EffectNameList) &&
            EffectChanceList.SequenceEqual(otherItem.EffectChanceList) &&
            EffectDurationList.SequenceEqual(otherItem.EffectDurationList)

            )
            {
                isEqual = true;
            }
        }
        else
        {
            if (ItemType == otherItem.ItemType && ItemName == otherItem.ItemName && ItemDescription == otherItem.ItemDescription &&
            WeaponType == otherItem.WeaponType && CombatType == otherItem.CombatType && Armor == otherItem.Armor &&
            MagicResistance == otherItem.MagicResistance && Health == otherItem.Health && Stamina == otherItem.Stamina &&
            Mana == otherItem.Mana && Speed == otherItem.Speed && PhysicalDamage == otherItem.PhysicalDamage &&
            MagicalDamage == otherItem.MagicalDamage && CriticalChance == otherItem.CriticalChance &&
            WeaponReach == otherItem.WeaponReach && ArmorType == otherItem.ArmorType &&
            AmmunitionType == otherItem.AmmunitionType && MagicType == otherItem.MagicType &&
            ChargeTime == otherItem.ChargeTime && ShieldDurability == otherItem.ShieldDurability &&
            coversFacialHair == otherItem.coversFacialHair && coversHair == otherItem.coversHair &&
            otherItem.coversHead == coversHead && otherItem.coversArms == coversArms &&
            otherItem.coversHands == coversHands && otherItem.coversTorso == coversTorso &&
            otherItem.coversLegs == coversLegs && otherItem.coversFeet ==coversFeet
)
            {
                isEqual = true;
            }
        }
        return isEqual;
    }
}
