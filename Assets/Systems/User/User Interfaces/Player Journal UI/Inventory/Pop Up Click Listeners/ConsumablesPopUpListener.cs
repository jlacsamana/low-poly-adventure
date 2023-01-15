using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumablesPopUpListener : MonoBehaviour
{
    //reference to current item view manager
    public CurrentItemViewManager cItemViewManager;

    //reference to player inventory manager
    public InventoryManager playerInventoryManager;

    //discard interface
    public GameObject discardInterface;

    // Start is called before the first frame update
    void Start()
    {
        playerInventoryManager = GameObject.Find("Player").GetComponent<InventoryManager>();
    }

    // Update is called once per frame
    void Update()
    {
        ListenForPopUpInput();
    }

    //listener
    public void ListenForPopUpInput()
    {
        //deselects item
        if (Input.GetMouseButtonDown(1) && discardInterface.activeInHierarchy == false)
        {
            cItemViewManager.DeselectItem();
        }
        //drops item(s)
        else if (Input.GetKeyDown(KeyCode.D))
        {
            //activates discard interface
            discardInterface.SetActive(true);
            discardInterface.GetComponent<DiscardPopupListener>().discardAmount = 1;
            //cItemViewManager.DeselectItem();
        }
        //consumes item
        else if (Input.GetMouseButtonDown(0) && discardInterface.activeInHierarchy == false)
        {
            //add a function for consuming
            playerInventoryManager.ConsumeItem(CurrentItemViewManager.currentSelectedItem);
            cItemViewManager.DeselectItem();
        }
    }
}
