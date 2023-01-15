using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JournalUIManager : MonoBehaviour {
    //handles input for journal menu

    //the journal UI transform
    public GameObject journalUI;
    public GameObject journalUIScrollRegion;

    //the character render camera
    public GameObject characterRenderCamera;

    //reference to character sub Ui manager
    public CharacterSubUIManager charSubUIManager;

    //current Ui substate
    public enum UIStates {Character, Inventory, Quest }
    public UIStates currentUIState = UIStates.Character;

    // Use this for initialization
    void Start () {
        journalUI = GameObject.Find("Menu").transform.Find("Player Journal").gameObject;
        charSubUIManager = GameObject.Find("Systems").transform.Find("Player Journal UI Manager").GetComponent<CharacterSubUIManager>();
        journalUIScrollRegion = GameObject.Find("Menu").transform.Find("Player Journal/Journal Scroll Region/Scroll Object").gameObject;
        characterRenderCamera = GameObject.Find("Player").transform.Find("Character Camera").gameObject;

    }
	
	// Update is called once per frame
	void Update () {
        if (journalUI.activeInHierarchy == true)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                ShiftJournalViewL();
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                ShiftJournalViewR();
            }

            if (currentUIState == UIStates.Character)
            {
                if (characterRenderCamera.activeInHierarchy == false)
                {
                    characterRenderCamera.SetActive(true);
                }
             
            }
            else
            {
                if (characterRenderCamera.activeInHierarchy == true)
                {
                    characterRenderCamera.SetActive(false);
                }
                   
            }
        }
        else
        {
            characterRenderCamera.SetActive(false);
        }
	}

    //initializes journal at base position
    public void InitJournal()
    {
        journalUIScrollRegion.transform.localPosition = new Vector3(1920,0,0);
        currentUIState = UIStates.Character;
        charSubUIManager.UpdateOutfitViewport();
        charSubUIManager.UpdateSelectedOutfitView();
        charSubUIManager.UpdateAttrView();
        charSubUIManager.playerEntityInfo.UpdateBaseStats();
    }

    //shifts view right
    public void ShiftJournalViewR()
    {
        if (currentUIState != UIStates.Quest)
        {
            journalUIScrollRegion.transform.localPosition += new Vector3(-1920,0,0);
        }

        if (currentUIState == UIStates.Inventory)
        {
            currentUIState = UIStates.Quest;
        }
        else if(currentUIState == UIStates.Character)
        {
            currentUIState = UIStates.Inventory;
        }
        charSubUIManager.UpdateOutfitViewport();
        charSubUIManager.UpdateSelectedOutfitView();
        charSubUIManager.UpdateAttrView();
        charSubUIManager.playerEntityInfo.UpdateBaseStats();
    }

    //shifts view left
    public void ShiftJournalViewL()
    {
        if (currentUIState != UIStates.Character)
        {
            journalUIScrollRegion.transform.localPosition += new Vector3(1920, 0, 0);
        }

        if (currentUIState == UIStates.Inventory)
        {
            currentUIState = UIStates.Character;
        }
        else if (currentUIState == UIStates.Quest)
        {
            currentUIState = UIStates.Inventory;
        }
        charSubUIManager.UpdateOutfitViewport();
        charSubUIManager.UpdateSelectedOutfitView();
        charSubUIManager.UpdateAttrView();
        charSubUIManager.playerEntityInfo.UpdateBaseStats();
    }


}
