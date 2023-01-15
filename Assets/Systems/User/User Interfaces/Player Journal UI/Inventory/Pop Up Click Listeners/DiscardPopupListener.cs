using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class DiscardPopupListener : MonoBehaviour
{//handles I/O for dicard interface and UI

    //reference to current item view manager
    public CurrentItemViewManager cItemViewManager;

    //reference to player gameobject
    public GameObject playerGameObject;

    //input field
    public InputField discardQuantity;

    //discard amount
    public int discardAmount = 0;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //cancels discard
    public void CancelDiscard()
    { 
        gameObject.SetActive(false);
    }

    //confirms discard
    public void ConfirmDiscard()
    {
        playerGameObject.GetComponent<InventoryManager>().DropItems(CurrentItemViewManager.currentSelectedItem, discardAmount);
        cItemViewManager.DeselectItem();
        gameObject.SetActive(false);
    }

    //adds to discard amount
    public void IncreaseDiscardAmount()
    {
        discardAmount++;
        ClampDiscardAmount();
        discardQuantity.text = discardAmount.ToString();
        
    }

    //subtracts from discard amount
    public void DecreaseDiscardAmount()
    {
        discardAmount--;
        ClampDiscardAmount();
        discardQuantity.text = discardAmount.ToString();
        
    }

    //restricts player from discarding more of an item than possible
    public void ClampDiscardAmount()
    {
        discardAmount = Int32.Parse(discardQuantity.text);
        int counter = 0;
        foreach (ItemData m in playerGameObject.GetComponent<Inventory>().entityInventory)
        {

            if (m.CheckIfEqual(CurrentItemViewManager.currentSelectedItem))
            {
                counter++;
            }
        }

        if (discardAmount > counter)
        {
            discardAmount = counter;
        }

        if (discardAmount < 1)
        {
            discardAmount = 1;
        }

        
        discardQuantity.text = discardAmount.ToString();
    }
}
