using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeAndDate : MonoBehaviour {
    //this handles in game time
    //day cycle length; default 1440 minutes, exactly 24 hours
    public float dayLength = 1440;

    //day length scale
    public float dayLengthScale = 1;

    //conversion from real life time to in game time; default 1 second is to 1 minute in game; day is 24 minutes by defualt
    public float deltaTimeToInGameTime = 1;

    //current time in day cycle
    public float currentTime = 0;

    // Use this for initialization
    void Start () {
        //InvokeRepeating("UpdateTime", 0, 1);
	}
	
	// Update is called once per frame
	void Update ()
    {
        UpdateTime();

    }

    //updates time every second
    void UpdateTime()
    {
        if (Time.timeScale == 1f)
        {
            currentTime += deltaTimeToInGameTime * Time.deltaTime;
            //makes sure time resets after completing day cycle
            if (currentTime >= (dayLength * dayLengthScale))
            {
                currentTime = 0.0f;
            }
        }
    }
}
