using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncGroundCollider : MonoBehaviour {
    //syncs main gameobject with the ground collider

    //reference to main gameobject
    public GameObject enityMainObj;

    //reference to this entity's ground collider
    public GameObject entityGroundCollider;


	// Use this for initialization
	void Start () {
        enityMainObj = gameObject.transform.root.gameObject;
        entityGroundCollider = Instantiate(
            Resources.Load<GameObject>("Game System Objects/Entity/Ground Collider"),
            new Vector3(enityMainObj.transform.position.x,-2.5f, enityMainObj.transform.position.z), Quaternion.identity);

        entityGroundCollider = Resources.Load<GameObject>("Game System Objects/Entity/Ground Collider");

    }
	
	// Update is called once per frame
	void Update () {
        if (Time.timeScale != 0)
        {
            SyncMovement();
        }
	}

    //sync main gameobject to this gameobject
    public void SyncMovement()
    {
        enityMainObj.transform.position = gameObject.transform.position;
    }
}
