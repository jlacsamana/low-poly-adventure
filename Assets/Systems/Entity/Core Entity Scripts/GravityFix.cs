using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityFix : MonoBehaviour {
    //attach this to all entities that are effected by gravity

    //this gameobject's rigidbody
    public Rigidbody entityRigidbody;

	// Use this for initialization
	void Start () {
        entityRigidbody = gameObject.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        if (entityRigidbody.velocity.y < 0)
        {
            entityRigidbody.velocity += Vector3.up * Physics.gravity.y * 1.5f * Time.deltaTime;
        }
	}
}
