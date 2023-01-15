using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    //this governs entity movement

    //current jump state
    public bool isJumping = false;
    public float jumpTimer = 0;

    //camera pan sensitivity
    public float cameraSensitvity = 1f;

    //reference to local animation controller
    public AnimationControllerShell entityAnim;

    //reference to local Entity Rotation Object
    public Transform entityRotationObject;

    //reference to local Entity ground collider
    public Rigidbody entityGroundCollider;


    // Use this for initialization
    void Start () {
        entityAnim = gameObject.GetComponent<AnimationControllerShell>();
        entityGroundCollider = gameObject.GetComponent<Rigidbody>();
        entityRotationObject = gameObject.transform.Find("Entity Rotation Object");
    }
	
	// Update is called once per frame
	void Update () {
        if (Time.timeScale == 1)
        {
            if (jumpTimer > 0)
            {
                jumpTimer -= Time.deltaTime;
                if (jumpTimer < 0)
                {
                    jumpTimer = 0;
                }
            }
        }
	}

    private void LateUpdate()
    {
    }

    //Forward commands
    public void WalkForward()
    {
        entityGroundCollider.velocity = (entityGroundCollider.transform.forward * gameObject.GetComponent<Entity>().currentEntitySpeed) + (entityGroundCollider.transform.up * entityGroundCollider.velocity.y);
    }
    public void SprintForward()
    {
        entityGroundCollider.velocity = (entityGroundCollider.transform.forward * gameObject.GetComponent<Entity>().currentEntitySpeed * 2f) + (entityGroundCollider.transform.up * entityGroundCollider.velocity.y);
    }

    //Backward Commands
    public void WalkBackward()
    {
        entityGroundCollider.velocity = -(entityGroundCollider.transform.forward * gameObject.GetComponent<Entity>().currentEntitySpeed) + (entityGroundCollider.transform.up * entityGroundCollider.velocity.y);
    }
    public void SprintBackward()
    {
        entityGroundCollider.velocity = -(entityGroundCollider.transform.forward * gameObject.GetComponent<Entity>().currentEntitySpeed * 2f) + (entityGroundCollider.transform.up * entityGroundCollider.velocity.y);
    }

    //Strafe Right Commands
    public void WalkStrafeRight()
    {
        entityGroundCollider.velocity = (entityGroundCollider.transform.right * gameObject.GetComponent<Entity>().currentEntitySpeed) + (entityGroundCollider.transform.up * entityGroundCollider.velocity.y);
    }
    public void SprintStrafeRight()
    {
        entityGroundCollider.velocity = (entityGroundCollider.transform.right * gameObject.GetComponent<Entity>().currentEntitySpeed * 2f) + (entityGroundCollider.transform.up * entityGroundCollider.velocity.y);
    }

    //Strafe Left Commands
    public void WalkStrafeLeft()
    {
        entityGroundCollider.velocity = -(entityGroundCollider.transform.right * gameObject.GetComponent<Entity>().currentEntitySpeed) + (entityGroundCollider.transform.up * entityGroundCollider.velocity.y);
    }
    public void SprintStrafeLeft()
    {
        entityGroundCollider.velocity = -(entityGroundCollider.transform.right * gameObject.GetComponent<Entity>().currentEntitySpeed * 2f) + (entityGroundCollider.transform.up * entityGroundCollider.velocity.y);
    }


    //controls jumping
    public void Jump()
    {
        if (jumpTimer == 0)
        {
            entityGroundCollider.AddForce(Vector3.up * 250);
            jumpTimer = 1;
            isJumping = false;
        }

    }

    //rotates player
    public void Rotate(Vector3 rotateAmount)
    {
        //rotates player gameobject
        gameObject.transform.Rotate(rotateAmount);
    }

}
