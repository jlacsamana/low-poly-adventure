using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD_Info : MonoBehaviour {
    //attach this to player HUD object
    //handles updating all the info displayed on the HUD

    //references to classes containing player info
    public Entity playerEntity;
    public EntityInfo playerEntityInfo;
    public Inventory playerInventory;
    public ShieldSystem playerShieldSystem;

    //references to UI elements
    public Slider hpSlider;
    public Slider manaSlider;
    public Slider staminaSlider;
    public Slider shieldSlider;

    public Text hpAmount;
    public Text manaAmount;
    public Text staminaAmount;
    public Text shieldAmount;


	// Use this for initialization
	void Start () {
        playerEntity = GameObject.Find("Player").GetComponent<Entity>();
        playerEntityInfo = GameObject.Find("Player").GetComponent<EntityInfo>();
        playerInventory = GameObject.Find("Player").GetComponent<Inventory>();
        playerShieldSystem = GameObject.Find("Player").GetComponent<ShieldSystem>();
        Invoke("UpdateMaxValues",0.01f);
        Invoke("UpdateCurrentValues", 0.01f);
    }
	
	// Update is called once per frame
	void Update () {
        UpdateHUD();
	}

    //updates the information in the HUD
    public void UpdateHUD()
    {
        UpdateMaxValues();
        UpdateCurrentValues();
    }

    //updates maximum values of the bottom-left stat display
    public void UpdateMaxValues()
    {
        hpSlider.maxValue = playerEntityInfo.maxEntityHealth;
        manaSlider.maxValue = playerEntityInfo.maxEntityMana;
        staminaSlider.maxValue = playerEntityInfo.maxEntityMana;
        if (playerInventory.entityLeftHandSlot.WeaponType == "Shield")
        {
            shieldSlider.maxValue = playerShieldSystem.maxShieldDurability;
        }
        else
        {
            shieldSlider.maxValue = 0;
        }
    }

    //updates current values of the bottom-left stat display
    public void UpdateCurrentValues()
    {
        hpSlider.value = playerEntity.currentEntityHealth;
        hpAmount.text = (playerEntity.currentEntityHealth).ToString();

        manaSlider.value = playerEntity.currentEntityMana;
        manaAmount.text = (playerEntity.currentEntityMana).ToString();


        staminaSlider.value = playerEntity.currentEntityStamina;
        staminaAmount.text = (playerEntity.currentEntityStamina).ToString();

        if (playerInventory.entityLeftHandSlot.WeaponType == "Shield" && playerShieldSystem.isBlocking)
        {

            shieldSlider.value = playerShieldSystem.shieldDurability;
            shieldAmount.text = playerShieldSystem.shieldDurability.ToString();
        }
        else
        {

            shieldSlider.value = 0;
            shieldAmount.text = "--";
        }
    }
}
