using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemData : MonoBehaviour {
    //this contains data for each item displayed in the shop UI and updates the text displays

    //text displays
    public Text itemName;
    public Text itemQuantity;
    public Text itemPrice;

    //data
    public string nameString = "";
    public string itemType = "";
    public int quantity = 0;
    public int price = 0;

    //is a sold item
    public bool isSelling = false;

    //item data
    public ItemData itemData;

	// Use this for initialization
	void Start () {
        itemName = gameObject.transform.Find("Item Name").gameObject.GetComponent<Text>();
        itemQuantity = gameObject.transform.Find("Item Quantity").gameObject.GetComponent<Text>();
        itemPrice = gameObject.transform.Find("Item Price").gameObject.GetComponent<Text>();
        itemName.text = nameString;
        itemPrice.text = (price + "G");

    }
	
	// Update is called once per frame
	void Update () {
        if (!isSelling)
        {
            itemQuantity.gameObject.SetActive(false);
            
        }
        else
        {
            itemQuantity.text = quantity.ToString();
        }
        
        itemPrice.text = (price + "G");
    }

}
