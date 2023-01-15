using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keybindings : MonoBehaviour {
    //this class contains all the keybindings

    //Movement
    public static KeyCode forWard = KeyCode.W;
    public static KeyCode backWard = KeyCode.S;
    public static KeyCode strafeLeft = KeyCode.A;
    public static KeyCode strafeRight = KeyCode.D;
    public static KeyCode jumpUp = KeyCode.Space;
    public static KeyCode sprint = KeyCode.LeftShift;

    //UI
    public static KeyCode inGameMenu = KeyCode.Escape;
    public static KeyCode playerJournal = KeyCode.Tab;
    public static KeyCode playerInteract = KeyCode.E;

    //combat
    public static KeyCode sheatheAndUnsheathe = KeyCode.F;
    public static KeyCode shieldOrParry = KeyCode.Q;
    


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
