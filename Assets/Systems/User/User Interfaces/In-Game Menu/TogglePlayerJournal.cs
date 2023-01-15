using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogglePlayerJournal : MonoBehaviour {
    //toggles player journal

    //journal state
    public bool journalState = false;

    //reference to player interact
    public Interact playerInteract;

    //reference to player Journal UI manager
    public CurrentItemViewManager itemViewManager;

    //referemce to Journal UI manager
    public JournalUIManager journalUImanager;

    //journal gameobject
    public GameObject journal;

    //in game menu
    public GameObject inGameMenu;

	// Use this for initialization
	void Start () {

        journalUImanager = GameObject.Find("Systems").transform.Find("Player Journal UI Manager").gameObject.GetComponent<JournalUIManager>();
        journal.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(Keybindings.playerJournal) && inGameMenu.activeInHierarchy == false &&
            (!playerInteract.dialogueInterface.activeInHierarchy && !playerInteract.lootInterface.activeInHierarchy &&
            !playerInteract.tradeInterface.activeInHierarchy))
        {
            switch (journalState)
            {
                case true:
                    journal.SetActive(false);
                    Time.timeScale = 1;
                    journalState = false;
                    itemViewManager.GetComponent<CurrentItemViewManager>().ResetCurrentItemView();
                    itemViewManager.GetComponent<CurrentStatDisplay>().UpdateValues();
                    break;
                case false:
                    journal.SetActive(true);
                    Time.timeScale = 0;
                    journalState = true;
                    journalUImanager.InitJournal();
                    itemViewManager.GetComponent<CurrentItemViewManager>().ResetCurrentItemView();
                    itemViewManager.GetComponent<DisplayCurrentItemList>().UpdateEquipItemListView();
                    itemViewManager.GetComponent<CurrentStatDisplay>().UpdateValues();
                    break;
            }
        }
	}
}
