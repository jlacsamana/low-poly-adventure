using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GlobalPriceTable : MonoBehaviour {
    //this houses all the base prices for items in trade

    //weapon prices
    public ItemPriceInfo[] weaponPrices =
    {
        new ItemPriceInfo{ itemName = "Test Sword", itemBasePrice = 100 }

    };

    //armor prices
    public ItemPriceInfo[] armorPrices =
    {


    };

    public ItemPriceInfo[] consumablePrices =
    {


    };

    public ItemPriceInfo[] materialPrices =
    {


    };

    //returns a value of an item searched by name(string)
    public static int FetchBaseItemPrice(string itemType, string itemNameStr)
    {
        int returnValue = 0;
        ItemPriceInfo[] listToRead = { };
        switch (itemType)
        {
            case "Weapon":
                listToRead =  GameObject.Find("Systems/Gameplay").GetComponent<GlobalPriceTable>().weaponPrices;
                break;
            case "Armor":
                listToRead = GameObject.Find("Systems/Gameplay").GetComponent<GlobalPriceTable>().armorPrices;
                break;
            case "Consumable":
                listToRead = GameObject.Find("Systems/Gameplay").GetComponent<GlobalPriceTable>().consumablePrices;
                break;
            case "Raw Material":
                listToRead = GameObject.Find("Systems/Gameplay").GetComponent<GlobalPriceTable>().materialPrices;
                break;
        }


        foreach (ItemPriceInfo itemPrice in listToRead)
        {
            if (itemPrice.itemName == itemNameStr)
            {
               
                returnValue = itemPrice.itemBasePrice;
            }
        }
        return returnValue;
    }
}
