using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MerchantInventory : MonoBehaviour {
    //attach this to any entity that should be a merchant

    //this class instance assigned to a modifiable variable
    public MerchantInventory entityMerchantInventory;

    //this merchant's list of sellable items
    public List<string> merchantItemList = new List<string>();

    //this merchant's inventory
    public List<ItemData> merchantInventory = new List<ItemData>();

    // Use this for initialization
    void Start () {
        entityMerchantInventory = gameObject.GetComponent<MerchantInventory>();
        InitMerchantInventory();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //fetch item data of all items that are supposed to be sold by this merchant
    public void InitMerchantInventory()
    {
        foreach (string itemName in merchantItemList)
        {
            merchantInventory.Add(LoadDataBase.FindItemData(itemName));
        }
    }
}
