using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEntityValues : MonoBehaviour {

    //the test entity class attached to 
    EntityInfo EntityMain;

	// Use this for initialization
	void Start () {
        EntityMain = gameObject.GetComponent<EntityInfo>();
        EntityMain.baseEntityArmor = 5;
        EntityMain.baseEntityMagicalArmor = 5;

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
