using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueData {
    //attach this to an entity which should have dialogue enabled
    //this class stores dialogue data

    //the dialogue to be displayed on the left dialogue panel 
    public string DialogueLine;

    //the response choices associated with this dialogue 
    public DialogueIO[] DialogueResponses;

    

}
