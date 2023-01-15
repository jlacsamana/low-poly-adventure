using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestItemPopUpListener : MonoBehaviour {
    //attach this to quest item item pop up
    //this listens for key/mouse input while pop up is active

    //reference to current item view manager
    public CurrentItemViewManager cItemViewManager;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        ListenForPopUpInput();
    }

    //listener
    public void ListenForPopUpInput()
    {
        //can only take in right click; deselects item
        if (Input.GetMouseButtonDown(1))
        {
            cItemViewManager.DeselectItem();
        }
    }
}
