using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventoryHUD : MonoBehaviour {
    //this updates the stat HUD in the inventory screen

    //Text displays
    public Text hpDisplay;
    public Text manaDisplay;
    public Text staminaDisplay;
    public Text shieldDisplay;
    public Text ShieldTextDisplay;

    //reference to player stats
    public Entity playerEntity;
    public EntityInfo playerEntityInfo;
    public Inventory playerInventory;
    public ShieldSystem playerShieldSys;
    
	// Use this for initialization
	void Start () {
        playerEntity = GameObject.Find("Player").GetComponent<Entity>();
        playerEntityInfo = GameObject.Find("Player").GetComponent<EntityInfo>();
        playerInventory = GameObject.Find("Player").GetComponent<Inventory>();
        playerShieldSys = GameObject.Find("Player").GetComponent<ShieldSystem>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Time.timeScale == 0)
        {
            hpDisplay.text = (playerEntity.currentEntityHealth + "/"+ playerEntityInfo.maxEntityHealth);
            manaDisplay.text = (playerEntity.currentEntityMana + "/" + playerEntityInfo.maxEntityMana);
            staminaDisplay.text = (playerEntity.currentEntityStamina + "/" + playerEntityInfo.maxEntityStamina);
            if (playerInventory.entityLeftHandSlot.WeaponType == "Shield")
            {
                ShieldTextDisplay.color = new Color(ShieldTextDisplay.color.r, ShieldTextDisplay.color.g, ShieldTextDisplay.color.g, 1);
                shieldDisplay.color = new Color(ShieldTextDisplay.color.r, ShieldTextDisplay.color.g, ShieldTextDisplay.color.g, 1);
                shieldDisplay.text = (playerShieldSys.shieldDurability + "/" + playerShieldSys.maxShieldDurability);
            }
            else
            {
                ShieldTextDisplay.color = new Color(ShieldTextDisplay.color.r, ShieldTextDisplay.color.g, ShieldTextDisplay.color.g, 0.5f);
                shieldDisplay.color = new Color(ShieldTextDisplay.color.r, ShieldTextDisplay.color.g, ShieldTextDisplay.color.g, 0.5f);
                shieldDisplay.text = ("N/A");
            }
        }
	}
}
