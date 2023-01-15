using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interact : MonoBehaviour {
    //handles player interaction with objects/entities in world

    //interaction Object Groups
    public GameObject lootableInventoryLayer;
    public GameObject talkLayer;
    public GameObject doorInteractLayer;

    //interactable layers
    public GameObject dialogueInterface;
    public GameObject lootInterface;
    public GameObject tradeInterface;
        
	// Use this for initialization
	void Start () {
        //starts all interactable layers as disabled
        dialogueInterface.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        InteractionListener();
	}

    //listener for interaction 
    public void InteractionListener()
    {
        //Debug.DrawRay(transform.Find("Main Camera").position, transform.Find("Main Camera").forward * 5f, Color.red);
        RaycastHit interactInfo;
        if (Physics.Raycast(transform.Find("Main Camera").position, transform.Find("Main Camera").forward, out interactInfo, 5f, ~(1 << 13)))
        {
            //makes sure interaction raycast does not hit its own gameobject
            if (interactInfo.transform.root.gameObject.GetInstanceID() != gameObject.GetInstanceID())
            {
                
                //if raycast has hit a lootable inventory
                if (interactInfo.transform.root.gameObject.GetComponent<LootableInventory>() &&
                    interactInfo.transform.root.gameObject.GetComponent<LootableInventory>().isInventoryLootable == true &&
                    interactInfo.transform.root.gameObject.GetComponent<LootableInventory>().entityLootableInventory.Count > 0)
                {
                    if (lootableInventoryLayer.activeInHierarchy != true)
                    {
                        lootableInventoryLayer.SetActive(true);
                    }
                    
                    lootableInventoryLayer.transform.Find("Entity Name").gameObject.GetComponent<Text>().text = interactInfo.transform.root.gameObject.name.Replace("(Clone)", "");
                    if (Input.GetKeyDown(Keybindings.playerInteract) &&
                    (!dialogueInterface.activeInHierarchy && !lootInterface.activeInHierarchy &&
                    !tradeInterface.activeInHierarchy))
                    {
                        foreach (ItemData item in interactInfo.transform.root.gameObject.GetComponent<LootableInventory>().entityLootableInventory)
                        {
                            gameObject.GetComponent<Inventory>().entityInventory.Add(item);
                        }
                        interactInfo.transform.root.gameObject.GetComponent<LootableInventory>().entityLootableInventory.Clear();
                    }

                }
                else
                {
                    if (lootableInventoryLayer.activeInHierarchy != false)
                    {
                        lootableInventoryLayer.SetActive(false);
                    }
                }
                
                //if raycast hit has a dialogue tree
                if (interactInfo.transform.root.gameObject.GetComponent<DialogueController>() &&
                    interactInfo.transform.root.gameObject.GetComponent<Entity>().currentEntityHealth > 0
                    )
                {
                    //sets the hover layer to disabled when dialogue interface is active
                    talkLayer.SetActive(!dialogueInterface.activeInHierarchy);
                    if (tradeInterface.activeInHierarchy)
                    {
                        talkLayer.SetActive(false); 
                    }
                    talkLayer.transform.Find("Entity Name").gameObject.GetComponent<Text>().text = interactInfo.transform.root.gameObject.name.Replace("(Clone)", "");
                    if (Input.GetKeyDown(Keybindings.playerInteract) &&
                    (!dialogueInterface.activeInHierarchy && !lootInterface.activeInHierarchy &&
                    !tradeInterface.activeInHierarchy))
                    {
                        switch (dialogueInterface.activeInHierarchy)
                        {
                            case true:
                                dialogueInterface.SetActive(false);
                                Time.timeScale = 1;
                                //ends dialogue data stream from raycast hit entity & unpauses game
                                
                                break;
                            case false:
                                dialogueInterface.SetActive(true);
                                Time.timeScale = 0;
                                //initialises dialogue data stream from raycast hit entity & pauses game
                                interactInfo.transform.root.gameObject.GetComponent<DialogueController>().DialogueInitializer();
                                break;
                        }
                    }
                }
                else
                {
                    if (talkLayer.activeInHierarchy != false)
                    {
                        talkLayer.SetActive(false);
                    }
                }
                //if raycast hit is an interactable door
                if(interactInfo.transform.parent != null)
                {
                    if (interactInfo.transform.parent.parent.gameObject.GetComponent<Door>())
                    {
                        if (doorInteractLayer.activeInHierarchy != true)
                        {
                            doorInteractLayer.SetActive(true);
                        }

                        if (interactInfo.transform.parent.parent.gameObject.GetComponent<Door>().thisDoorstate == Door.doorState.Closed)
                        {
                            doorInteractLayer.transform.Find("Toggle").gameObject.GetComponent<Text>().text = "Open Door";
                        }
                        else if (interactInfo.transform.parent.parent.gameObject.GetComponent<Door>().thisDoorstate == Door.doorState.Opened)
                        {
                            doorInteractLayer.transform.Find("Toggle").gameObject.GetComponent<Text>().text = "Close Door";
                        }
                        if (Input.GetKeyDown(Keybindings.playerInteract))
                        {
                            interactInfo.transform.parent.parent.gameObject.GetComponent<Door>().ToggleDoor();
                        }
                    }
                }
                else
                {
                    if (doorInteractLayer.activeInHierarchy != false)
                    {
                        doorInteractLayer.SetActive(false);
                    }
                }
            }
        }
        else
        {
            if (lootableInventoryLayer.activeInHierarchy != false)
            {
                lootableInventoryLayer.SetActive(false);
            }

            if (talkLayer.activeInHierarchy != false)
            {
                talkLayer.SetActive(false);
            }

            if (doorInteractLayer.activeInHierarchy != false)
            {
                doorInteractLayer.SetActive(false);
            }


        }
    }


}
