using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserSyncLookDir : MonoBehaviour {
    //attach this only to the player's instance of "Look Direction Object"
    //syncs that object's rotation to the camera; in an AI controlled entity, this is handled by the Ai instead

    //rotation value for head


	// Use this for initialization
	void Start () {
        transform.rotation = transform.root.Find("Main Camera").transform.rotation;
    }
	
	// Update is called once per frame
	void Update () {
        if (Time.timeScale ==1)
        {
            transform.rotation = transform.root.Find("Main Camera").transform.rotation;
        }

        if (transform.localEulerAngles.x > 45 && transform.localEulerAngles.x < 315)
        {
            if (Mathf.Abs(transform.localEulerAngles.x - 45) < Mathf.Abs(transform.localEulerAngles.x - 315))
            {
                transform.localRotation = Quaternion.Euler(45, transform.localRotation.y, 0);
            }
            else if (Mathf.Abs(transform.localEulerAngles.x - 45) > Mathf.Abs(transform.localEulerAngles.x - 315))
            {
                transform.localRotation = Quaternion.Euler(315, transform.localRotation.y, 0);
            }

        }
    }
}
