using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueIO {
    //stores data for choices 

    //next dialogue data in array to load
    public int SequentialDialogueData = 0;

    //text for choice button
    public string DialogueOptionString;

    //will trigger exit event
    public bool ExitTrigger = false;

    //will trigger trade UI
    public bool TradeUITrigger = false;

    //will trigger quest 
    public bool QuestTrigger = false;






}
