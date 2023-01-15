using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserSyncCamera : MonoBehaviour {
    //syncs camera to head joint on armature; attach only to the player model

    //reference to the head joint
    public Transform headJoint;
       
	// Use this for initialization
	void Start () {
        headJoint = gameObject.transform.root.gameObject.transform.Find("Humanoid M Template/Humanoid_Armature/lower_spine/middle_spine/upper_spine/upper_spine_018/upper_spine_019");
        
	}
	
	// Update is called once per frame
	void Update () {
        gameObject.transform.position = headJoint.transform.position
        + new Vector3(headJoint.transform.forward.x * 0.175f, headJoint.transform.forward.y * 0.175f, headJoint.transform.forward.z * 0.175f);
    }
    private void LateUpdate()
    {

    }

}
