using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class TradeDisplayManager : MonoBehaviour {
    //this handles any trade transaction; updates trade interfaces, and processes buying and selling

    //reference to the trade UI
    public GameObject tradeUI;

    //reference to the dialogue UI
    public GameObject dialogueUI;

    //reference to player inventory
    public Inventory playerInventory;

    //reference to current merchant inventory & dialogue controller
    public MerchantInventory currentMerchant;
    public DialogueController currentMerchantDialogueController;

    //reference to the item display region
    public GameObject DisplayRegion;

    //reference to the item shop displauy button
    public GameObject itemVendorButton;

    //reference to trade confirmation screens
    public GameObject tradePurchasingScreen;
    public GameObject tradeSellingScreen;

    //trade mode(buy or sell)
    private enum TradeMode { Buy, Sell };
    private TradeMode currentTradeMode = TradeMode.Buy;

    //item display mode
    private enum ItemMode {Weapon, Armor, Consumables, Materials, Outfit };
    private ItemMode currentItemMode = ItemMode.Weapon;

    //buying and selling information and UI references
    public int sellBuyQuantity = 1;
    public ShopItemData currentSelectedItem;

    public int total = 0;

    public GameObject buyItemName;
    public GameObject sellItemName;

    public GameObject buyItemPrice;
    public GameObject sellItemPrice;

    public GameObject buyTotal;
    public GameObject sellTotal;

    public GameObject buyQuantity;
    public GameObject sellQuantity;

    //current selected item info
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
        tradeUI.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(Keybindings.inGameMenu) && tradeUI.activeInHierarchy)
        {
            EndTrade();
        }
    }

    //initialises a trade transaction
    public void InitTrade(ref MerchantInventory merchant)
    {
        currentMerchant = merchant;
        currentMerchantDialogueController = currentMerchant.gameObject.GetComponent<DialogueController>();
        //toggles trade UI on and current dialogue UI off
        tradeUI.SetActive(true);
        dialogueUI.SetActive(false);
        //updates trade UI
        tradeUI.transform.Find("Text Objects/Vendor Name").gameObject.GetComponent<Text>().text = currentMerchant.gameObject.name.Replace("(Clone)", "");
        UpdateTradeDisplay();
        ResetCurrentItemInfo();
    }

    //ends current active transaction
    public void EndTrade()
    {
        //toggles trade UI off and current dialogue UI on
        tradeUI.SetActive(false);
        dialogueUI.SetActive(true);
        tradePurchasingScreen.SetActive(false);
        tradeSellingScreen.SetActive(false);
        currentMerchantDialogueController.
        DialogueHandler(currentMerchantDialogueController.dialogueLibrary[currentMerchantDialogueController.tradeDialogueIndex]);
        ResetCurrentItemInfo();
    }

    //sets button modes
    public void SetSellMode()
    {
        currentTradeMode = TradeMode.Sell;
        UpdateTradeDisplay();
    }

    public void SetBuyMode()
    {
        currentTradeMode = TradeMode.Buy;
        UpdateTradeDisplay();
    }
    
    //sets item mode
    public void SetWpnMode()
    {
        currentItemMode = ItemMode.Weapon;
        UpdateTradeDisplay();
    }

    public void SetArmorMode()
    {
        currentItemMode = ItemMode.Armor;
        UpdateTradeDisplay();
    }

    public void SetConsumableMode()
    {
        currentItemMode = ItemMode.Consumables;
        UpdateTradeDisplay();
    }

    public void SetMaterialMode()
    {
        currentItemMode = ItemMode.Materials;
        UpdateTradeDisplay();
    }

    public void SetOutfitMode()
    {
        currentItemMode = ItemMode.Outfit;
        UpdateTradeDisplay();
    }

    //Updates display
    public void UpdateTradeDisplay()
    {
        List<ItemData> itemDisplayList = new List<ItemData>();
        List<ItemData> currentlyDisplayedItems = new List<ItemData>();
        //if trade mode is set to buy
        if (currentTradeMode == TradeMode.Buy)
        {
            switch (currentItemMode)
            {
                case ItemMode.Weapon:                   
                    foreach (ItemData item in currentMerchant.merchantInventory)
                    {
                        if (item.ItemType == "Weapon")
                        {
                            itemDisplayList.Add(item);
                        }
                    }
                    break;
                case ItemMode.Armor:
                    foreach (ItemData item in currentMerchant.merchantInventory)
                    {
                        if (item.ItemType == "Armor")
                        {
                            itemDisplayList.Add(item);
                        }
                    }
                    break;
                case ItemMode.Consumables:
                    foreach (ItemData item in currentMerchant.merchantInventory)
                    {
                        if (item.ItemType == "Consumable")
                        {
                            itemDisplayList.Add(item);
                        }
                    }
                    break;
                case ItemMode.Materials:
                    foreach (ItemData item in currentMerchant.merchantInventory)
                    {
                        if (item.ItemType == "Raw Material")
                        {
                            itemDisplayList.Add(item);
                        }
                    }
                    break;
                case ItemMode.Outfit:
                    foreach (ItemData item in currentMerchant.merchantInventory)
                    {
                        if (item.ItemType == "Outfit")
                        {
                            itemDisplayList.Add(item);
                        }
                    }
                    break;
            }
        }
        //if trade mode is set to sell
        else if (currentTradeMode == TradeMode.Sell)
        {
            switch (currentItemMode)
            {
                case ItemMode.Weapon:
                    foreach (ItemData item in playerInventory.entityInventory)
                    {
                        if (item.ItemType == "Weapon")
                        {
                            itemDisplayList.Add(item);
                        }
                    }
                    break;
                case ItemMode.Armor:
                    foreach (ItemData item in playerInventory.entityInventory)
                    {
                        if (item.ItemType == "Armor")
                        {
                            itemDisplayList.Add(item);
                        }
                    }
                    break;
                case ItemMode.Consumables:
                    foreach (ItemData item in playerInventory.entityInventory)
                    {
                        if (item.ItemType == "Consumable")
                        {
                            itemDisplayList.Add(item);
                        }
                    }
                    break;
                case ItemMode.Materials:
                    foreach (ItemData item in playerInventory.entityInventory)
                    {
                        if (item.ItemType == "Raw Material")
                        {
                            itemDisplayList.Add(item);
                        }
                    }
                    break;
            }
        }


        //clears display
        foreach (Transform item in DisplayRegion.transform)
        {
            Destroy(item.gameObject);
        }
        //instantiates buttons for display
        if (itemDisplayList.Count > 0)
        {
            int itemCounter = 0;
            currentlyDisplayedItems.Clear();
            foreach (ItemData item in itemDisplayList)
            {
                //only displays items that have value; making an item worthless is a way to blacklist it
                if (GlobalPriceTable.FetchBaseItemPrice(item.ItemType, item.ItemName) != 0)
                {
                    bool alreadyExists = false;
                    foreach (ItemData instantiatedItem in currentlyDisplayedItems)
                    {
                        //this item already has a button
                        if (instantiatedItem.CheckIfEqual(item))
                        {
                            alreadyExists = true;
                            foreach (Transform shopButton in DisplayRegion.transform)
                            {
                                if (shopButton.gameObject.GetComponent<ShopItemData>().itemData
                                    .CheckIfEqual(item))
                                {
                                    shopButton.GetComponent<ShopItemData>().quantity++;
                                }
                            }
                        }
                    }
                    //if this item doesnt have a button yet
                    if (!alreadyExists)
                    {
                        currentlyDisplayedItems.Add(item);
                        GameObject instantiateButton = Instantiate(itemVendorButton, DisplayRegion.transform);
                        instantiateButton.GetComponent<RectTransform>().localPosition = new Vector3(0, -15 - (30 * itemCounter));
                        instantiateButton.GetComponent<ShopItemData>().nameString = item.ItemName;
                        instantiateButton.GetComponent<ShopItemData>().itemType = item.ItemType;
                        instantiateButton.GetComponent<ShopItemData>().quantity = 1;
                        //this is temporary price
                        if (currentTradeMode == TradeMode.Buy)
                        {
                            instantiateButton.GetComponent<ShopItemData>().price =
                            GlobalPriceTable.FetchBaseItemPrice(instantiateButton.GetComponent<ShopItemData>().itemType,
                            instantiateButton.GetComponent<ShopItemData>().nameString);
                            instantiateButton.GetComponent<Button>().onClick.AddListener(BuyButton);
                        }
                        else if (currentTradeMode == TradeMode.Sell)
                        {
                            instantiateButton.GetComponent<ShopItemData>().price = Mathf.RoundToInt
                            (GlobalPriceTable.FetchBaseItemPrice(instantiateButton.GetComponent<ShopItemData>().itemType,
                            instantiateButton.GetComponent<ShopItemData>().nameString) * 0.35f);
                            instantiateButton.GetComponent<Button>().onClick.AddListener(SellButton);
                            instantiateButton.GetComponent<ShopItemData>().isSelling = true;
                        }
                        instantiateButton.GetComponent<ShopItemData>().itemData = item;
                        itemCounter++;
                    }
                }
            }
            //sets length of display region
            DisplayRegion.GetComponent<RectTransform>().sizeDelta = new Vector2(790, 60 + (30 * itemCounter));
        
        }
    }

    //buy buttons
    public void BuyButton()
    {
        tradePurchasingScreen.SetActive(true);
        //updates buy information
        buyItemName.GetComponent<Text>().text = EventSystem.current.currentSelectedGameObject.GetComponent<ShopItemData>().nameString;
        buyItemPrice.GetComponent<Text>().text = (EventSystem.current.currentSelectedGameObject.GetComponent<ShopItemData>().price + "G");
        currentSelectedItem = EventSystem.current.currentSelectedGameObject.GetComponent<ShopItemData>();
        total = currentSelectedItem.price * sellBuyQuantity;
        sellBuyQuantity = 1;
        ConstrainQuantity();
        switch (currentTradeMode)
        {
            case TradeMode.Sell:
                sellTotal.GetComponent<Text>().text = (total + "G");
                break;
            case TradeMode.Buy:
                buyTotal.GetComponent<Text>().text = (total + "G");
                break;
        }
        UpdateCurrentItemInfo();
    }

    public void ConfirmBuyButton()
    {
        tradePurchasingScreen.SetActive(false);
        playerInventory.goldCoins -= total;
        for (int i = 0; i < sellBuyQuantity; i++)
        {
            playerInventory.entityInventory.Add(currentSelectedItem.itemData);
        }
        UpdateTradeDisplay();
        ResetCurrentItemInfo();
    }

    //sell buttons
    public void SellButton()
    {
        tradeSellingScreen.SetActive(true);
        //updates sell information
        sellItemName.GetComponent<Text>().text = EventSystem.current.currentSelectedGameObject.GetComponent<ShopItemData>().nameString;
        sellItemPrice.GetComponent<Text>().text = (EventSystem.current.currentSelectedGameObject.GetComponent<ShopItemData>().price + "G");
        currentSelectedItem = EventSystem.current.currentSelectedGameObject.GetComponent<ShopItemData>();
        total = currentSelectedItem.price * sellBuyQuantity;
        sellBuyQuantity = 1;
        ConstrainQuantity();
        switch (currentTradeMode)
        {
            case TradeMode.Sell:
                sellTotal.GetComponent<Text>().text = (total + "G");
                break;
            case TradeMode.Buy:
                buyTotal.GetComponent<Text>().text = (total + "G");
                break;
        }
        UpdateCurrentItemInfo();
    }

    public void ConfirmSellButton()
    {
        List<ItemData> removeList = new List<ItemData>();
        tradeSellingScreen.SetActive(false);
        playerInventory.goldCoins += total;
        foreach (ItemData item in playerInventory.entityInventory)
        {
            if (item.CheckIfEqual(currentSelectedItem.itemData))
            {
                removeList.Add(item);
            }
            
        }
        for (int i = 0;i < sellBuyQuantity; i++)
        {        
            playerInventory.entityInventory.Remove(removeList[i]);
        }
        UpdateTradeDisplay();
        ResetCurrentItemInfo();
    }

    //cancel transaction
    public void CancelTransaction()
    {
        tradePurchasingScreen.SetActive(false);
        tradeSellingScreen.SetActive(false);
        ResetCurrentItemInfo();
    }

    //toggles selected quantity
    public void IncreaseQuantity()
    {
        sellBuyQuantity++;
        ConstrainQuantity();
        total = currentSelectedItem.price * sellBuyQuantity;
        switch (currentTradeMode)
        {
            case TradeMode.Sell:
                sellTotal.GetComponent<Text>().text = (total + "G");
                sellQuantity.gameObject.GetComponent<InputField>().text = sellBuyQuantity.ToString();
                break;
            case TradeMode.Buy:
                buyTotal.GetComponent<Text>().text = (total + "G");
                buyQuantity.gameObject.GetComponent<InputField>().text = sellBuyQuantity.ToString();
                break;
        }

    }

    public void DecreaseQuantity()
    {
        sellBuyQuantity--;
        ConstrainQuantity();
        total = currentSelectedItem.price * sellBuyQuantity;
        switch (currentTradeMode)
        {
            case TradeMode.Sell:
                sellTotal.GetComponent<Text>().text = (total + "G");
                sellQuantity.gameObject.GetComponent<InputField>().text = sellBuyQuantity.ToString();
                break;
            case TradeMode.Buy:
                buyTotal.GetComponent<Text>().text = (total + "G");
                buyQuantity.gameObject.GetComponent<InputField>().text = sellBuyQuantity.ToString();
                break;
        }
    }

    //change value of sellbuyquantity through the input field
    public void InputQuantity()
    {
        bool isInt;
        switch (currentTradeMode)
        {
            case TradeMode.Sell:
                isInt = int.TryParse(sellQuantity.GetComponent<InputField>().text, out sellBuyQuantity);
                if (sellQuantity.transform.Find("Text").gameObject.GetComponent<Text>().text == "")
                {
                    sellBuyQuantity = 1;
                }
                break;
            case TradeMode.Buy:
                isInt = int.TryParse(buyQuantity.GetComponent<InputField>().text, out sellBuyQuantity);
                if (buyQuantity.transform.Find("Text").gameObject.GetComponent<Text>().text == "")
                {
                    sellBuyQuantity = 1;
                }
                break;
        }
        total = currentSelectedItem.price * sellBuyQuantity;
        ConstrainQuantity();
    }

    public void ConstrainQuantity()
    {
        //ensures that the value does not fall below zero
        if (sellBuyQuantity < 1)
        {
            sellBuyQuantity = 1;
        }

        //ensures that player can't sell more than they have
        if (currentTradeMode == TradeMode.Sell)
        {
            int counter = 0;
            foreach (ItemData item in playerInventory.entityInventory)
            {
                if (item.CheckIfEqual(currentSelectedItem.itemData))
                {
                    counter++;
                }
            }
            if (sellBuyQuantity > counter)
            {
                sellBuyQuantity = counter;
            }
        }

        //ensures player can't buy more than they can afford
        if (currentTradeMode == TradeMode.Buy)
        {
            int counter = 0;
            foreach (ItemData item in playerInventory.entityInventory)
            {
                if (item.CheckIfEqual(currentSelectedItem.itemData))
                {
                    counter++;
                }
            }
            if ((sellBuyQuantity * currentSelectedItem.GetComponent<ShopItemData>().price) > playerInventory.goldCoins)
            {
                sellBuyQuantity = (int)(playerInventory.goldCoins / currentSelectedItem.GetComponent<ShopItemData>().price);
            }

            //constricts buy quantity to 1 if buying an outfit
            if (currentItemMode == ItemMode.Outfit)
            {
                sellBuyQuantity = 1;
            }
        }

        switch (currentTradeMode)
        {
            case TradeMode.Sell:
                sellTotal.GetComponent<Text>().text = (total + "G");
                sellQuantity.gameObject.GetComponent<InputField>().text = sellBuyQuantity.ToString();
                break;
            case TradeMode.Buy:
                buyTotal.GetComponent<Text>().text = (total + "G");
                buyQuantity.gameObject.GetComponent<InputField>().text = sellBuyQuantity.ToString();
                break;
        }
    }

    //update the current selected item view
    public void UpdateCurrentItemInfo()
    {
        viewedItemName.text = currentSelectedItem.itemData.ItemName;
        viewedItemDescription.text = currentSelectedItem.itemData.ItemDescription;
        physDmgMod.text = currentSelectedItem.itemData.PhysicalDamage.ToString();
        magicalDmgMod.text = currentSelectedItem.itemData.MagicalDamage.ToString();
        criticalMod.text = (currentSelectedItem.itemData.CriticalChance + "%");
        physArmorMod.text = currentSelectedItem.itemData.Armor.ToString();
        magicalArmorMod.text = currentSelectedItem.itemData.MagicResistance.ToString();
        speedMod.text = currentSelectedItem.itemData.Speed.ToString();
        healthMod.text = currentSelectedItem.itemData.Health.ToString();
        manaMod.text = currentSelectedItem.itemData.Mana.ToString();
        staminaMod.text = currentSelectedItem.itemData.Stamina.ToString();
        attackSpeed.text = "--";

        if (currentSelectedItem.itemData.ItemType == "Weapon")
        {
            if (currentSelectedItem.itemData.WeaponType == "One Handed")
            {
                attackSpeed.text = Math.Round(((currentSelectedItem.itemData.WeaponSpeed * 40f) / 60f), 2).ToString() + "s";
            }
            else if (currentSelectedItem.itemData.WeaponType == "Two Handed")
            {
                attackSpeed.text = Math.Round(((currentSelectedItem.itemData.WeaponSpeed * 60f) / 60f), 2).ToString() + "s";
            }
            else if (currentSelectedItem.itemData.WeaponType == "Projectile Weapon")
            {
                if (currentSelectedItem.itemData.CombatType == "Bow")
                {
                    attackSpeed.text = Math.Round(((30f * currentSelectedItem.itemData.WeaponSpeed) / 60f), 2).ToString() + "s";
                }
                else if (currentSelectedItem.itemData.CombatType == "Crossbow")
                {
                    attackSpeed.text = Math.Round(((75f * currentSelectedItem.itemData.WeaponSpeed) / 60f), 2).ToString() + "s";
                }
            }
            else if (currentSelectedItem.itemData.WeaponType == "Staff")
            {
                attackSpeed.text = Math.Round(((currentSelectedItem.itemData.WeaponSpeed * 90f) / 60f), 2).ToString() + "s";
            }
            else if (currentSelectedItem.itemData.WeaponType == "Tome")
            {
                if (currentSelectedItem.itemData.MagicType == "Constant")
                {
                    attackSpeed.text = "--";
                }
                else if (currentSelectedItem.itemData.MagicType == "Charge")
                {
                    attackSpeed.text = currentSelectedItem.itemData.ChargeTime.ToString() + "s";
                }
            }
        }
        else
        {
            attackSpeed.text = "--";
        }

        if (currentSelectedItem.itemData.EffectNameList.Length >= 1)
        {
            effect1.text = (currentSelectedItem.itemData.EffectNameList[0] + "(" + currentSelectedItem.itemData.EffectTargetList[0] + ")");
        }
        else { effect1.text = "--"; }

        if (currentSelectedItem.itemData.EffectNameList.Length >= 2)
        {
            effect2.text = (currentSelectedItem.itemData.EffectNameList[1] + "(" + currentSelectedItem.itemData.EffectTargetList[1] + ")");
        }
        else { effect2.text = "--"; }

        if (currentSelectedItem.itemData.EffectNameList.Length >= 3)
        {
            effect3.text = (currentSelectedItem.itemData.EffectNameList[2] + "(" + currentSelectedItem.itemData.EffectTargetList[2] + ")");
        }
        else { effect3.text = "--"; }

        if (currentSelectedItem.itemData.EffectNameList.Length >= 4)
        {
            effect4.text = (currentSelectedItem.itemData.EffectNameList[3] + "(" + currentSelectedItem.itemData.EffectTargetList[3] + ")");
        }
        else { effect4.text = "--"; }
    }

    //resets the current selected item view
    public void ResetCurrentItemInfo()
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
    }

}
