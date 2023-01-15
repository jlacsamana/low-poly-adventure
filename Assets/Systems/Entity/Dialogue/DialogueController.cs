using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[System.Serializable]
public class DialogueController : MonoBehaviour {
    //attach to any entity with dialogue enabled
    //Handles I/O for dialogue choices and paths

    //reference to the dialogue interaction layer
    public GameObject dialogueLayer;
    public Transform npcDialogueContainer;
    public Transform playerDialogueContainer;
    public Transform playerDialogueOptionsContainer;

    //reference to the dialogue option prefab 
    public GameObject dialogueOptionPrefab;

    //reference to the trade display manager
    public TradeDisplayManager tradeDisplayManager;

    //the index of the DialogueData in dialogueLibrary that contains the trade dialogue
    public int tradeDialogueIndex;

    //list of dialogue 
    public DialogueData[] dialogueLibrary;

    //current DialogueDataIndex
    public int currentDialogueInstance = 0;


    //current dialogue choice
    public int currentDialogueChoice = 0;



	// Use this for initialization
	void Start () {
        dialogueLayer = GameObject.Find("Interaction UI layer/Interactable Interfaces/Dialogue");
        npcDialogueContainer = dialogueLayer.transform.Find("Talk Container NPC");
        playerDialogueContainer = dialogueLayer.transform.Find("Talk Container Player");
        playerDialogueOptionsContainer = playerDialogueContainer.transform.Find("Dialogue Mask/Dialogue Options Container");
        tradeDisplayManager = GameObject.Find("Systems/Gameplay").GetComponent<TradeDisplayManager>();
        dialogueOptionPrefab = Resources.Load("UI Assets/Player HUD/Dialogue/Dialogue Option Button") as GameObject;


    }

    // Update is called once per frame
    void Update () {
		
	}

    //dialogue initialiser; always make the first in the dialogueLibrary array is the data for the intialiser
    public void DialogueInitializer()
    {
        currentDialogueInstance = 0;
        //assigns name of npc
        npcDialogueContainer.Find("NPC Name").GetComponent<Text>().text = gameObject.name.Replace("(Clone)", "");
        //initialises first DialogueData in array
        npcDialogueContainer.Find("NPC Dialogue").GetComponent<Text>().text = dialogueLibrary[0].DialogueLine;
        //resets the dialogue options controller
        foreach (Transform child in playerDialogueOptionsContainer)
        {
            Destroy(child.gameObject);
        }
        playerDialogueOptionsContainer.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(390, 0);
        //creates button for each option
        int buttonIndex = 0;
        playerDialogueOptionsContainer.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(390, 82.5f * dialogueLibrary[0].DialogueResponses.Length);
        foreach (DialogueIO dialogueOption in dialogueLibrary[0].DialogueResponses)
        {
            GameObject instantiatedOption = Instantiate(dialogueOptionPrefab, playerDialogueOptionsContainer);

            instantiatedOption.GetComponent<RectTransform>().localPosition = new Vector2(0,(40 + buttonIndex*82.5f));
            instantiatedOption.GetComponent<Button>().onClick.AddListener(SetDialogueChoice);
            instantiatedOption.GetComponentInChildren<Text>().text = dialogueOption.DialogueOptionString;
            buttonIndex++;
            
        }
        


    }

    //dialogue choice Handler
    public void DialogueHandler(DialogueData dialogueData)
    {
        npcDialogueContainer.Find("NPC Dialogue").GetComponent<Text>().text = dialogueLibrary[currentDialogueInstance].DialogueLine;
        //resets the dialogue options controller
        foreach (Transform child in playerDialogueOptionsContainer)
        {
            Destroy(child.gameObject);
        }
        playerDialogueOptionsContainer.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(390, 0);
        //creates button for each option
        int buttonIndex = 0;
        playerDialogueOptionsContainer.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(390, 82.5f * dialogueLibrary[0].DialogueResponses.Length);
        foreach (DialogueIO dialogueOption in dialogueLibrary[currentDialogueInstance].DialogueResponses)
        {
            GameObject instantiatedOption = Instantiate(dialogueOptionPrefab, playerDialogueOptionsContainer);

            instantiatedOption.GetComponent<RectTransform>().localPosition = new Vector2(0, (40 + buttonIndex * 82.5f));
            instantiatedOption.GetComponent<Button>().onClick.AddListener(SetDialogueChoice);
            instantiatedOption.GetComponentInChildren<Text>().text = dialogueOption.DialogueOptionString;
            buttonIndex++;

        }
    }

    //ends dialogue
    public void EndDialogue()
    {
        dialogueLayer.SetActive(false);
        Time.timeScale = 1;
    }


    //sets current dialogue choice
    public void SetDialogueChoice()
    {
        int index = EventSystem.current.currentSelectedGameObject.transform.GetSiblingIndex();
        //if dialogue sequence choice has end trigger
        if (dialogueLibrary[currentDialogueInstance].DialogueResponses[index].ExitTrigger)
        {
            dialogueLayer.SetActive(false);
            Time.timeScale = 1;
        }
        //if dialogue choice triggers a quest
        else if (dialogueLibrary[currentDialogueInstance].DialogueResponses[index].QuestTrigger)
        {

        }
        //if dialogue choice triggers a trade UI
        else if (dialogueLibrary[currentDialogueInstance].DialogueResponses[index].TradeUITrigger)
        {
            tradeDisplayManager.InitTrade(ref gameObject.GetComponent<MerchantInventory>().entityMerchantInventory);
        }
        else
        {
            currentDialogueInstance = dialogueLibrary[currentDialogueInstance].DialogueResponses[index].SequentialDialogueData;
            DialogueHandler(dialogueLibrary[dialogueLibrary[currentDialogueInstance].DialogueResponses[index].SequentialDialogueData]);

        }

    }

}
