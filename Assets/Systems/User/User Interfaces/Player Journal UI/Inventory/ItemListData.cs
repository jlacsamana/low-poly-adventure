using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using UnityEngine.UI;

public class ItemListData : MonoBehaviour {
    //this attaches to item list templates and stores data such as item name and quantity

    //this button's item data
    public ItemData itemData;

    //this button's quantity
    public int itemDataQuantity = 0;

    //is an equipped Item?
    public bool isEquipped = false;

	// Use this for initialization
	void Start () {
        gameObject.GetComponent<Button>().onClick.AddListener(GameObject.Find("Player Journal UI Manager").GetComponent<CurrentItemViewManager>().SetSelectedItem);

    }
	
	// Update is called once per frame
	void Update () {
 
	}

    //updates item data and quantity on display
    public void UpdateData()
    {
        gameObject.transform.Find("Item Name").GetComponent<Text>().text = itemData.ItemName.ToString();
        gameObject.transform.Find("Item Quantity").GetComponent<Text>().text = itemDataQuantity.ToString();
    }

    //updates item data for equipped items
    public void UpdateEquippedData()
    {
        gameObject.transform.Find("Item Name").GetComponent<Text>().text = itemData.ItemName.ToString();
        if (itemData.WeaponType == "Ammunition")
        {
            gameObject.transform.Find("Item Quantity").GetComponent<Text>().text = itemDataQuantity.ToString();
        }
        else
        {
            gameObject.transform.Find("Item Quantity").GetComponent<Text>().text = "";
        }

    }
}
