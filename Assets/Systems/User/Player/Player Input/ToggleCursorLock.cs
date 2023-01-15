using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleCursorLock : MonoBehaviour {
    //locks and unlocks cursor

	// Use this for initialization
	void Start () {
   
    }
	
	// Update is called once per frame
	void Update () {
        if (Time.timeScale == 1)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else if (Time.timeScale == 0)
        {
            Cursor.lockState = CursorLockMode.None;
        }
	}
}
