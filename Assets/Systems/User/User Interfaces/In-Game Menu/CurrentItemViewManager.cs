using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class CurrentItemViewManager : MonoBehaviour {
    //attach to player journal system

    //current selected Item
    public static ItemData currentSelectedItem;

    //reference to pop-ups for handling selected item
    public GameObject popUpContainer;

    public GameObject popUpNonEquippable;
    public GameObject popUpEquippable;
    public GameObject popUpEquipped;
    public GameObject popUpConsumable;
    public GameObject popUpQuestItem;


    //reference to current viewed item panel
    public Text viewedItemName;
    public Text viewedItemDescription;
    public RawImage viewedItemImage;

    public Text physDmgMod;
    public Text magicalDmgMod;
    public Text criticalMod;

    public Text physArmorMod;
    public Text magicalArmorMod;
    public Text speedMod;

    public Text healthMod;
    public Text manaMod;
    public Text staminaMod;
    public Text attackSpeed;

    public Text effect1;
    public Text effect2;
    public Text effect3;
    public Text effect4;


    // Use this for initialization
    void Start () {
        viewedItemName.text = "";
        viewedItemDescription.text = "";
        effect1.text = "--";
        effect2.text = "--";
        effect3.text = "--";
        effect4.text = "--";

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //sets selected Item
    public void SetSelectedItem()
    {
        currentSelectedItem = EventSystem.current.currentSelectedGameObject.GetComponent<ItemListData>().itemData;
        UpdateSelectedItemInfo();
        popUpContainer.SetActive(true);
        if (currentSelectedItem.ItemType == "Armor" || currentSelectedItem.ItemType == "Weapon"
            )
        {
            switch (EventSystem.current.currentSelectedGameObject.GetComponent<ItemListData>().isEquipped)
            {
                case true:
                    popUpEquipped.SetActive(true);
                    break;
                case false:
                    popUpEquippable.SetActive(true);
                    break;
            }

        }
        else if (currentSelectedItem.ItemType == "Consumable")
        {
            popUpConsumable.SetActive(true);
        }
        else if (currentSelectedItem.ItemType == "Raw Material")
        {
            popUpNonEquippable.SetActive(true);
        }
        else
        {
            popUpQuestItem.SetActive(true);
        }
 
    }

    //update information 
    public void UpdateSelectedItemInfo()
    {
        viewedItemName.text = currentSelectedItem.ItemName;
        viewedItemDescription.text = currentSelectedItem.ItemDescription;
        physDmgMod.text = currentSelectedItem.PhysicalDamage.ToString();
        magicalDmgMod.text = currentSelectedItem.MagicalDamage.ToString();
        criticalMod.text = (currentSelectedItem.CriticalChance + "%");
        physArmorMod.text = currentSelectedItem.Armor.ToString();
        magicalArmorMod.text = currentSelectedItem.MagicResistance.ToString();
        speedMod.text = currentSelectedItem.Speed.ToString();
        healthMod.text = currentSelectedItem.Health.ToString();
        manaMod.text = currentSelectedItem.Mana.ToString();
        staminaMod.text = currentSelectedItem.Stamina.ToString();
        attackSpeed.text = "--";
        if (currentSelectedItem.ItemType == "Weapon")
        {
            if (currentSelectedItem.WeaponType == "One Handed")
            {
                attackSpeed.text = Math.Round(((currentSelectedItem.WeaponSpeed * 40f)/60f),2).ToString() + "s";
            }
            else if (currentSelectedItem.WeaponType == "Two Handed")
            {
                attackSpeed.text = Math.Round(((currentSelectedItem.WeaponSpeed * 60f)/60f),2).ToString() + "s";
            }
            else if (currentSelectedItem.WeaponType == "Projectile Weapon")
            {
                if (currentSelectedItem.CombatType == "Bow")
                {
                    attackSpeed.text = Math.Round(((30f * currentSelectedItem.WeaponSpeed)/60f), 2).ToString() + "s";
                }
                else if (currentSelectedItem.CombatType == "Crossbow")
                {
                    attackSpeed.text = Math.Round(((75f * currentSelectedItem.WeaponSpeed) / 60f),2).ToString() + "s";
                }
            }
            else if (currentSelectedItem.WeaponType == "Staff")
            {
                attackSpeed.text = Math.Round(((currentSelectedItem.WeaponSpeed * 90f) / 60f),2).ToString() + "s";
            }
            else if (currentSelectedItem.WeaponType == "Tome")
            {
                if (currentSelectedItem.MagicType == "Constant")
                {
                    attackSpeed.text = "--";
                }
                else if (currentSelectedItem.MagicType == "Charge")
                {
                    attackSpeed.text = currentSelectedItem.ChargeTime.ToString() + "s";
                }
            }
        }
        else
        {
            attackSpeed.text = "--";
        }


        if (currentSelectedItem.EffectNameList.Length >= 1)
        {
            effect1.text = (currentSelectedItem.EffectNameList[0] + "("+ currentSelectedItem.EffectTargetList[0]+ ")");
        }
        else { effect1.text = "--"; }

        if (currentSelectedItem.EffectNameList.Length >= 2)
        {
            effect2.text = (currentSelectedItem.EffectNameList[1] + "(" + currentSelectedItem.EffectTargetList[1] + ")");
        }
        else { effect2.text = "--"; }

        if (currentSelectedItem.EffectNameList.Length >= 3)
        {
            effect3.text = (currentSelectedItem.EffectNameList[2] + "(" + currentSelectedItem.EffectTargetList[2] + ")");
        }
        else { effect3.text = "--"; }
        
        if (currentSelectedItem.EffectNameList.Length >= 4)
        {
            effect4.text = (currentSelectedItem.EffectNameList[3] + "(" + currentSelectedItem.EffectTargetList[3] + ")");
        }
        else { effect4.text = "--"; }
    }

    //resets item view
    public void ResetCurrentItemView()
    {
        viewedItemName.text = "";
        viewedItemDescription.text = "";
        physDmgMod.text = "--";
        magicalDmgMod.text = "--";
        criticalMod.text = "--";
        physArmorMod.text = "--";
        magicalArmorMod.text = "--";
        speedMod.text = "--";
        healthMod.text = "--";
        manaMod.text = "--";
        staminaMod.text = "--";
        effect1.text = "--";
        effect2.text = "--";
        effect3.text = "--";
        effect4.text = "--";
        attackSpeed.text = "--";
        currentSelectedItem = null;
        popUpContainer.SetActive(false);
        foreach (Transform child in popUpContainer.transform)
        {
            child.gameObject.SetActive(false);
        }
        popUpContainer.transform.Find("Cover Panel").gameObject.SetActive(true);
    }

    //deselects selected item
    public void DeselectItem()
    {
        viewedItemName.text = "";
        viewedItemDescription.text = "";
        physDmgMod.text = "--";
        magicalDmgMod.text = "--";
        criticalMod.text = "--";
        physArmorMod.text = "--";
        magicalArmorMod.text = "--";
        speedMod.text = "--";
        healthMod.text = "--";
        manaMod.text = "--";
        staminaMod.text = "--";
        effect1.text = "--";
        effect2.text = "--";
        effect3.text = "--";
        effect4.text = "--";
        attackSpeed.text = "--";
        currentSelectedItem = null;
        popUpContainer.SetActive(false);
        foreach (Transform child in popUpContainer.transform)
        {
            child.gameObject.SetActive(false);
        }
        popUpContainer.transform.Find("Cover Panel").gameObject.SetActive(true);
        viewedItemName.text = "";
        viewedItemDescription.text = "";
    }

}
