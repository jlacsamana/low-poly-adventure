using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DisplayCurrentItemList : MonoBehaviour {
    //attach this to systems
    //this manages the inventory UI for the player inventory

    //reference to inventory attached to player
    public Inventory playerInventory;

    //reference to item list scroll region
    public Transform itemScrollRegion;

    //reference to equipped item list scroll region
    public Transform equippedItemScrollRegion;

    //reference to item list button prefab
    public GameObject listButtonPrefab;

    //initial position for buttons
    public Vector3 defaultButtonPos = new Vector3(0,0,0);

    //the current list to display
    public List<ItemData> currentItemList = new List<ItemData>();

    //current list to display
    public string currentItemListID = "Weapon";

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(Keybindings.playerJournal))
        {
            UpdateItemListView();
            UpdateEquipItemListView();
        }
    }

    //universal item list button
    public void UniversalItemListButton()
    {
        switch (EventSystem.current.currentSelectedGameObject.name)
        {
            case "Weapons":
                currentItemListID = "Weapon";
                UpdateItemListView();
                break;
            case "Armor":
                currentItemListID = "Armor";
                UpdateItemListView();
                break;
            case "Consumables":
                currentItemListID = "Consumable";
                UpdateItemListView();
                break;
            case "Raw Materials":
                currentItemListID = "Raw Material";
                UpdateItemListView();
                break;
            case "Quest Items":
                currentItemListID = "Quest Item";
                UpdateItemListView();
                break;
        }
        
    }

    //updates current displayed list
    public void UpdateItemListView()
    {
        //resets display region
        currentItemList.Clear();
        itemScrollRegion.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(itemScrollRegion.gameObject.GetComponent<RectTransform>().sizeDelta.x, 0);
        foreach (Transform child in itemScrollRegion)
        {
            Destroy(child.gameObject);
        }

        if (playerInventory.entityInventory.Count > 0)
        {
            //returns all inventory items with specified type
            foreach (ItemData item in playerInventory.entityInventory)
            {
                if (item.ItemType == currentItemListID)
                {
                    currentItemList.Add(item);
                }
            }
            List<ItemData> instantiatedItemButtons = new List<ItemData>();
            //instantiates item list buttons
            foreach (ItemData item in currentItemList)
            {
                bool alreadyExists = false;
                if (instantiatedItemButtons.Count > 0)
                {               
                    foreach (ItemData I in instantiatedItemButtons)
                    {
                        if (item.CheckIfEqual(I))
                        {
                            alreadyExists = true;
                            foreach (Transform instantiatedItem in itemScrollRegion)
                            {
                                if (instantiatedItem.gameObject.GetComponent<ItemListData>().itemData
                                    .CheckIfEqual(I))
                                {
                                    instantiatedItem.GetComponent<ItemListData>().itemDataQuantity++;
                                    instantiatedItem.GetComponent<ItemListData>().UpdateData();
                                }
                            }
                        }
                    }
                    if (alreadyExists ==  false)
                    {
                        instantiatedItemButtons.Add(item);
                        GameObject currentButton = Instantiate(listButtonPrefab, new Vector3(0, 0, 0), Quaternion.identity, itemScrollRegion);
                        itemScrollRegion.gameObject.GetComponent<RectTransform>().sizeDelta += new Vector2(0, 30);
                        currentButton.transform.localPosition = new Vector3(0, -15 - (30 * (instantiatedItemButtons.Count - 1)), 0);
                        currentButton.GetComponent<ItemListData>().itemData = item;
                        currentButton.GetComponent<ItemListData>().itemDataQuantity++;
                        currentButton.GetComponent<ItemListData>().UpdateData();
                    }
                }
                else
                {
                    instantiatedItemButtons.Add(item);
                    GameObject currentButton = Instantiate(listButtonPrefab, new Vector3(0, 0, 0), Quaternion.identity, itemScrollRegion);
                    itemScrollRegion.gameObject.GetComponent<RectTransform>().sizeDelta += new Vector2(0, 30);
                    currentButton.transform.localPosition = new Vector3(0, -15 - (30 * (instantiatedItemButtons.Count - 1)), 0);
                    currentButton.GetComponent<ItemListData>().itemData = item;
                    currentButton.GetComponent<ItemListData>().itemDataQuantity++;
                    currentButton.GetComponent<ItemListData>().UpdateData();
                }
            }        
        }
    }

    //updates equipped list
    public void UpdateEquipItemListView()
    {
        foreach (Transform child in equippedItemScrollRegion)
        {
            Destroy(child.gameObject);
        }
        equippedItemScrollRegion.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(equippedItemScrollRegion.gameObject.GetComponent<RectTransform>().sizeDelta.x, 0);
        List<ItemData> equippedList = new List<ItemData>(new ItemData[] {playerInventory.entityHeadSlot,playerInventory.entityTorsoSlot,
            playerInventory.entityLegsSlot,playerInventory.entityFeetSlot,playerInventory.entityLeftHandSlot, playerInventory.entityRightHandSlot,
        playerInventory.entityNecklaceSlot,playerInventory.entityRightRingSlot, playerInventory.entityLeftRingSlot});
        int counter = 0;
        foreach (ItemData equippedItem in equippedList)
        {
            if (!(equippedItem.CheckIfEqual(JsonToItemData.ParseItemFromDatabase(LoadDataBase.footGearList["No Boots"]))  ||
                equippedItem.CheckIfEqual(JsonToItemData.ParseItemFromDatabase(LoadDataBase.headGearList["No Helmet"])) ||
                equippedItem.CheckIfEqual(JsonToItemData.ParseItemFromDatabase(LoadDataBase.legGearList["No Greaves"])) ||
                equippedItem.CheckIfEqual(JsonToItemData.ParseItemFromDatabase(LoadDataBase.necklaceList["No Necklace"]))||
                equippedItem.CheckIfEqual(JsonToItemData.ParseItemFromDatabase(LoadDataBase.ringList["No Ring"]))||
                equippedItem.CheckIfEqual(JsonToItemData.ParseItemFromDatabase(LoadDataBase.torsoGearList["No ChestPlate"]))||
                equippedItem.CheckIfEqual(JsonToItemData.ParseItemFromDatabase(LoadDataBase.twoHandedWeaponsList["Bare Hands"]))||
                equippedItem.CheckIfEqual(JsonToItemData.ParseItemFromDatabase(LoadDataBase.oneHandedWeaponsList["Empty Item Slot"]))
                ))
            {
                counter++;
                GameObject instantiatedButton = Instantiate(listButtonPrefab, new Vector3(0, 0, 0), Quaternion.identity, equippedItemScrollRegion);
                equippedItemScrollRegion.gameObject.GetComponent<RectTransform>().sizeDelta += new Vector2(0, 30);
                instantiatedButton.transform.localPosition = new Vector3(0, -17.5f - (30 * (counter - 1)), 0);
                instantiatedButton.GetComponent<ItemListData>().itemData = equippedItem;
                instantiatedButton.GetComponent<ItemListData>().isEquipped = true;
                instantiatedButton.GetComponent<ItemListData>().UpdateEquippedData();
            }
        }
        if (playerInventory.AmmoQuiver.Count > 0)
        {
            counter++;
            GameObject instantiatedButton = Instantiate(listButtonPrefab, new Vector3(0, 0, 0), Quaternion.identity, equippedItemScrollRegion);
            equippedItemScrollRegion.gameObject.GetComponent<RectTransform>().sizeDelta += new Vector2(0, 30);
            instantiatedButton.transform.localPosition = new Vector3(0, -17.5f - (30 * (counter - 1)), 0);
            instantiatedButton.GetComponent<ItemListData>().itemData = playerInventory.AmmoQuiver[0];
            instantiatedButton.GetComponent<ItemListData>().itemDataQuantity = playerInventory.AmmoQuiver.Count;
            instantiatedButton.GetComponent<ItemListData>().isEquipped = true;
            instantiatedButton.GetComponent<ItemListData>().UpdateEquippedData();
        }
    }
}
