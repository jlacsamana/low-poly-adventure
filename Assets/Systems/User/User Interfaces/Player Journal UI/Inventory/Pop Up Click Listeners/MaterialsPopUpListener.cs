using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialsPopUpListener : MonoBehaviour {
    //attach this to materials item pop up
    //this listens for key/mouse input while pop up is active

    //reference to current item view manager
    public CurrentItemViewManager cItemViewManager;

    //reference to player gameobject
    public GameObject playerGameObject;

    //discard interface
    public GameObject discardInterface;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
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
    }
}
