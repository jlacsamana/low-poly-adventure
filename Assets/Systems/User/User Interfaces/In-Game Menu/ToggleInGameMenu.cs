using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleInGameMenu : MonoBehaviour {
    //toggles in game menu

    //reference to player interact
    public Interact playerInteract;

    //menu state
    public static bool inGameMenuState = false;

    //in-game menu
    public GameObject inGameMenu;

    //inventory UI
    public GameObject playerJournal;

	// Use this for initialization
	void Start () {
        inGameMenu.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        ToggleMenu();

    }

    //detects if menu hotkey is pressed
    void ToggleMenu()
    {
        if (Input.GetKeyDown(Keybindings.inGameMenu) && playerJournal.activeInHierarchy == false && 
            (!playerInteract.dialogueInterface.activeInHierarchy && !playerInteract.lootInterface.activeInHierarchy &&
            !playerInteract.tradeInterface.activeInHierarchy))
        {
            switch (inGameMenuState)
            {
                case true:
                    inGameMenu.SetActive(false);
                    Time.timeScale = 1;
                    inGameMenuState = false;
                    break;
                case false:
                    inGameMenu.SetActive(true);
                    Time.timeScale = 0;
                    inGameMenuState = true;
                    break;
            }
        }
    }
}
