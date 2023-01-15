using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEmpty : MonoBehaviour {
    //cleans up empty gameobject memory

	// Use this for initialization
	void Start () {
        InvokeRepeating("Cleaner", 1,5);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //cleans up empty gameobjects
    public void Cleaner()
    {
        foreach (GameObject M in FindObjectsOfType<GameObject>())
        {
            if (M.name == "New Game Object")
            {
                Destroy(M);
            }
        }

    }
}
