using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    //allows doors to open and close
    //attach to every gameobject with name "Door Group"

    //first door
    public GameObject doorObj1;

    //second door; optional
    public GameObject doorObj2;

    //door open state
    public enum doorState {Closed, Opened, Transition };

    //current door state
    public doorState thisDoorstate = doorState.Closed;

    //door state transition state time in seconds
    public float transitionTime = 1;

    // Start is called before the first frame update
    void Start()
    {
        //if single door is present
        if (gameObject.transform.childCount >= 1)
        {
            doorObj1 = gameObject.transform.GetChild(0).gameObject;
        }

        //if double doors are present
        if (gameObject.transform.childCount >= 2)
        {
            doorObj2 = gameObject.transform.GetChild(1).gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //toggles door state
    public void ToggleDoor()
    {
        //makes it so that door state cannot change while transition is active
        if (thisDoorstate != doorState.Transition)
        {
            //opens door
            if (thisDoorstate == doorState.Closed)
            {
                StartCoroutine(OpenDoor());
            }
            //closes door
            else if (thisDoorstate == doorState.Opened)
            {
                StartCoroutine(CloseDoor());
            }
        }
    }

    //handles door opening
    IEnumerator OpenDoor()
    {
        //if door is dual door
        if (doorObj1 != null && doorObj2 != null)
        {
            while ((Mathf.Round(doorObj1.transform.localEulerAngles.z) != 90f) && (Mathf.Round(doorObj2.transform.localEulerAngles.z) != 270f))
            {              
                thisDoorstate = doorState.Transition;
                doorObj1.transform.Rotate(0, 0, 1);
                doorObj2.transform.Rotate(0, 0, -1);
                yield return null;
            }

            if ((Mathf.Round(doorObj1.transform.localEulerAngles.z) == 90f) && (Mathf.Round(doorObj2.transform.localEulerAngles.z) == 270f))
            {
                thisDoorstate = doorState.Opened;
                yield break;
            }
        }
        //if door is single door
        if (doorObj1 != null)
        {
            while (Mathf.Round(doorObj1.transform.localEulerAngles.z) != 90f)
            {
                thisDoorstate = doorState.Transition;
                doorObj1.transform.Rotate(0, 0, 1);            
                yield return null;
            }

            if (Mathf.Round(doorObj1.transform.localEulerAngles.z) == 90f)
            {
                thisDoorstate = doorState.Opened;
                yield break;
            }
        }
    }

    //handles door closing
    IEnumerator CloseDoor()
    {
        //if door is dual door
        if (doorObj1 != null && doorObj2 != null)
        {
            while ((Mathf.Round(doorObj1.transform.localEulerAngles.z) != 0f) && (Mathf.Round(doorObj2.transform.localEulerAngles.z) != 0f))
            {
                thisDoorstate = doorState.Transition;
                doorObj1.transform.Rotate(0, 0, -1);
                doorObj2.transform.Rotate(0, 0, 1);
                yield return null;
            }

            if ((Mathf.Round(doorObj1.transform.localEulerAngles.z) == 0f) && (Mathf.Round(doorObj2.transform.localEulerAngles.z) == 0f))
            {
                thisDoorstate = doorState.Closed;
                yield break;
            }
        }
        //if door is single door
        if (doorObj1 != null)
        {
            while (Mathf.Round(doorObj1.transform.localEulerAngles.z) != 0f)
            {
                thisDoorstate = doorState.Transition;
                doorObj1.transform.Rotate(0, 0, -1);
                yield return null;
            }
            if (Mathf.Round(doorObj1.transform.localEulerAngles.z) == 0f)
            {
                thisDoorstate = doorState.Closed;
                yield break;
            }
        }
    }


}
