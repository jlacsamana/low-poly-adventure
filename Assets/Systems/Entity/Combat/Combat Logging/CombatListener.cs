using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatListener : MonoBehaviour {
    //this class attaches to any entity that can equip
    //disables equipping and unequipping if in combat

    //is in combat
    public bool entityIsInCombat = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //entity takes damage or uses weapon; equip is disabled for 5 seconds
    public void CombatEngaged()
    {
        CancelInvoke();
        entityIsInCombat = true;
        Invoke("CombatDisengaged", 3);
    }

    //entity is no longer in combat
    public void CombatDisengaged()
    {
        entityIsInCombat = false;
    }
}
