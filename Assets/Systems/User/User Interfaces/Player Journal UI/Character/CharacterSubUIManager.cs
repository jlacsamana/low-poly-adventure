using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSubUIManager : MonoBehaviour
{
    //handles the sub UI system for the character screen of the journal

    //sub UIs
    public GameObject statSubUI;
    public GameObject outfitSubUI;

    //reference to player entity's outfit manager
    public OutfitManager playerOutfitManager;

    //reference to player entity's animEquiphandler
    public AnimEquiphandler playerAnimEquipHandler;

    //reference to player's entity classes & inventory
    public Entity playerEntity;
    public EntityInfo playerEntityInfo;
    public Inventory playerInventory;


    //scroll region
    public GameObject outfitScrollRegion;

    //outfit button template
    public GameObject outfitButtonTemplate;

    //text elements of current selected outfit
    public Text outfitNameTxt;
    public Text outfitDescTxt;

    //stat display of current selected outfit
    public Text outfitHealth;
    public Text outfitMana;
    public Text outfitStamina;

    public Text outfitSpeed;
    public Text outfitArmor;
    public Text outfitResistance;

    public Text outfitPhysDamage;
    public Text outfitMagicalDamage;
    public Text outfitCriticalChance;

    //stat display for total stats
    public Text totalHealh;
    public Text totalMana;
    public Text totalStamina;
    public Text totalAttackSpeed;

    public Text totalSpeed;
    public Text totalArmor;
    public Text totalResistance;

    public Text totalPhysDamage;
    public Text totalMagicalDamage;
    public Text totalCriticalChance;

    //displays for attr views
    public Text strDisplay;
    public Text intDisplay;
    public Text endDisplay;
    public Text wilDisplay;
    public Text lucDisplay;

    public Text availablePtDisplay;



    // Start is called before the first frame update
    void Start()
    {
        playerOutfitManager = GameObject.Find("Player").GetComponent<OutfitManager>();
        playerAnimEquipHandler = GameObject.Find("Player").GetComponent<AnimEquiphandler>();
        playerEntity = GameObject.Find("Player").GetComponent<Entity>();
        playerEntityInfo = GameObject.Find("Player").GetComponent<EntityInfo>();
        playerInventory = GameObject.Find("Player").GetComponent<Inventory>();
        SetStatistics();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //sets UI to the statistics
    public void SetStatistics()
    {
        statSubUI.SetActive(true);
        outfitSubUI.SetActive(false);
        playerEntityInfo.UpdateBaseStats();
    }

    //handles outfits management and all associated UIs
    //sets UI to outfit
    public void SetOutfit()
    {
        outfitSubUI.SetActive(true);
        statSubUI.SetActive(false);
        UpdateOutfitViewport();
    }

    //handles the outfit list viewport
    public void UpdateOutfitViewport()
    {
        //clears display port and resets counter
        foreach (Transform m in outfitScrollRegion.transform)
        {
            Destroy(m.gameObject);
        }
        int counter = 0;


        foreach (ItemData outFit in playerOutfitManager.acquiredOutfits)
        {
            GameObject outfitButtonInstance = Instantiate(outfitButtonTemplate,outfitScrollRegion.transform);
            outfitButtonInstance.transform.localPosition = new Vector3(-320,-15 - (30 * counter),0);
            outfitButtonInstance.GetComponent<OutfitButtonData>().outfitData = outFit;
            //Debug.Log(outfitButtonInstance.GetComponent<OutfitButtonData>().outfitData.ItemName);
            outfitButtonInstance.GetComponent<OutfitButtonData>().InitButton();
            outfitButtonInstance.GetComponent<Button>().onClick.AddListener(() => { outfitButtonInstance.GetComponent<OutfitButtonData>().EquipOutfit(); }); 
            counter++;
        }

        outfitScrollRegion.GetComponent<RectTransform>().sizeDelta = new Vector2(640, 100 + (30 * counter));
    }

    //updates current selected outfit view
    public void UpdateSelectedOutfitView()
    {
        outfitNameTxt.text = playerOutfitManager.equippedOutfit.ItemName;
        outfitDescTxt.text = playerOutfitManager.equippedOutfit.ItemDescription;
        UpdateCurrentTotalStatView();
        UpdateCurrentOutfitStats();
    }

    //updates current equipped outfit stat view
    public void UpdateCurrentOutfitStats()
    {
        outfitHealth.text = playerOutfitManager.equippedOutfit.Health.ToString();
        outfitMana.text = playerOutfitManager.equippedOutfit.Mana.ToString();
        outfitStamina.text = playerOutfitManager.equippedOutfit.Stamina.ToString();

        outfitSpeed.text = playerOutfitManager.equippedOutfit.Speed.ToString();
        outfitArmor.text = playerOutfitManager.equippedOutfit.Armor.ToString();
        outfitResistance.text = playerOutfitManager.equippedOutfit.MagicResistance.ToString();

        outfitPhysDamage.text = playerOutfitManager.equippedOutfit.PhysicalDamage.ToString();
        outfitMagicalDamage.text = playerOutfitManager.equippedOutfit.MagicalDamage.ToString();
        outfitCriticalChance.text = $"{playerOutfitManager.equippedOutfit.CriticalChance}%";
    }

    //handles attribute management and all associated UIs
    //adds 1 point to strength
    public void AddStr()
    {
        if (playerEntityInfo.availableAttrPoints > 0)
        {
            playerEntityInfo.availableAttrPoints -= 1;
            playerEntityInfo.strengthAttr += 1;
            playerEntityInfo.UpdateBaseStats();
            UpdateAttrView();
        }
    }

    //adds 1 point to intelligence
    public void AddInt()
    {
        if (playerEntityInfo.availableAttrPoints > 0)
        {
            playerEntityInfo.availableAttrPoints -= 1;
            playerEntityInfo.intelligenceAttr += 1;
            playerEntityInfo.UpdateBaseStats();
            UpdateAttrView();
        }

    }

    //adds 1 point to endurance
    public void AddEnd()
    {
        if (playerEntityInfo.availableAttrPoints > 0)
        {
            playerEntityInfo.availableAttrPoints -= 1;
            playerEntityInfo.enduranceAttr += 1;
            playerEntityInfo.UpdateBaseStats();
            UpdateAttrView();
        }
    }

    //adds 1 point to willpower
    public void AddWil()
    {
        if (playerEntityInfo.availableAttrPoints > 0)
        {
            playerEntityInfo.availableAttrPoints -= 1;
            playerEntityInfo.willpowerAttr += 1;
            playerEntityInfo.UpdateBaseStats();
            UpdateAttrView();
        }
    }

    //adds 1 point to Luck
    public void AddLuc()
    {
        if (playerEntityInfo.availableAttrPoints > 0)
        {
            playerEntityInfo.availableAttrPoints -= 1;
            playerEntityInfo.luckAttr += 1;
            playerEntityInfo.UpdateBaseStats();
            UpdateAttrView();
        }
    }

    //updates attribute display & total stat display accordingly
    public void UpdateAttrView()
    {
        availablePtDisplay.text = playerEntityInfo.availableAttrPoints.ToString();
        strDisplay.text = playerEntityInfo.strengthAttr.ToString();
        intDisplay.text = playerEntityInfo.intelligenceAttr.ToString();
        endDisplay.text = playerEntityInfo.enduranceAttr.ToString();
        wilDisplay.text = playerEntityInfo.willpowerAttr.ToString();
        lucDisplay.text = playerEntityInfo.luckAttr.ToString();
    }


    //updates total stat views 
    public void UpdateCurrentTotalStatView()
    {
        totalHealh.text = playerEntityInfo.baseEntityHealth.ToString();
        totalMana.text = playerEntityInfo.baseEntityMana.ToString();
        totalStamina.text = playerEntityInfo.baseEntityStamina.ToString();

        //totalAttackSpeed.text = $"{playerInventory.entityRightHandSlot.WeaponSpeed}s";
        totalSpeed.text = playerEntityInfo.baseEntitySpeed.ToString();
        totalArmor.text = playerEntityInfo.baseEntityArmor.ToString();
        totalResistance.text = playerEntityInfo.baseEntityMagicalArmor.ToString();

        totalPhysDamage.text = playerEntityInfo.baseEntityPhysicalDamage.ToString();
        totalMagicalDamage.text = playerEntityInfo.baseEntityMagicalDamage.ToString();
        totalCriticalChance.text = $"{playerEntityInfo.baseEntityCriticalChance}%";
        if (playerInventory.entityRightHandSlot.ItemType == "Weapon")
        {

            if (playerInventory.entityRightHandSlot.WeaponType == "One Handed")
            {
                totalAttackSpeed.text = Math.Round(((playerInventory.entityRightHandSlot.WeaponSpeed * 40f) / 60f), 2).ToString() + "s";
            }
            else if (playerInventory.entityRightHandSlot.WeaponType == "Two Handed")
            {
                totalAttackSpeed.text = Math.Round(((playerInventory.entityRightHandSlot.WeaponSpeed * 60f) / 60f), 2).ToString() + "s";
            }
            else if (playerInventory.entityRightHandSlot.WeaponType == "Projectile Weapon")
            {
                if (playerInventory.entityRightHandSlot.CombatType == "Bow")
                {
                    totalAttackSpeed.text = Math.Round(((30f * playerInventory.entityRightHandSlot.WeaponSpeed) / 60f), 2).ToString() + "s";
                }
                else if (playerInventory.entityRightHandSlot.CombatType == "Crossbow")
                {
                    totalAttackSpeed.text = Math.Round(((75f * playerInventory.entityRightHandSlot.WeaponSpeed) / 60f), 2).ToString() + "s";
                }
            }
            else if (playerInventory.entityRightHandSlot.WeaponType == "Staff")
            {
                totalAttackSpeed.text = Math.Round(((playerInventory.entityRightHandSlot.WeaponSpeed * 90f) / 60f), 2).ToString() + "s";
            }
            else if (playerInventory.entityRightHandSlot.WeaponType == "Tome")
            {
                if (playerInventory.entityRightHandSlot.MagicType == "Constant")
                {
                    totalAttackSpeed.text = "--";
                }
                else if (playerInventory.entityRightHandSlot.MagicType == "Charge")
                {
                    totalAttackSpeed.text = playerInventory.entityRightHandSlot.ChargeTime.ToString() + "s";
                }
            }
        }

    }


}
