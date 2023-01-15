using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    //this is the player's movement controller shell
    //attack to the player

    //player camera
    public GameObject playerCam;

    //reference to local animatorControllerShell
    public AnimationControllerShell localAnim;

    //reference to local movement
    public Movement localMovement;

    //camera pan sensitivity
    public float cameraSensitvity = 1f;

    // Use this for initialization
    void Start()
    {
        playerCam = GameObject.Find("Main Camera");
        localAnim = gameObject.GetComponent<AnimationControllerShell>();
        localMovement = gameObject.GetComponent<Movement>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Time.timeScale ==1)
        {
            PlayerMovementInput();
            PlayerRotationInput();
            PlayerConstrainCam();
        }

	}

    private void LateUpdate()
    {
     
    }

    //checks for movement input
    public void PlayerMovementInput()
    {
        if (!Input.GetKey(Keybindings.strafeRight) || !Input.GetKey(Keybindings.backWard) ||
        !Input.GetKey(Keybindings.strafeLeft) || !Input.GetKey(Keybindings.strafeRight))
        {
            gameObject.GetComponent<Rigidbody>().velocity = (gameObject.transform.up * gameObject.GetComponent<Rigidbody>().velocity.y);
        }
        if (Input.GetKey(Keybindings.forWard))
        {
            if (Input.GetKey(Keybindings.sprint))
            {
                localMovement.SprintForward();
            }
            else
            {
                localMovement.WalkForward();
            }
        }
        if (Input.GetKey(Keybindings.backWard))
        {
            if (Input.GetKey(Keybindings.sprint))
            {
                localMovement.SprintBackward();
            }
            else
            {
                localMovement.WalkBackward();
            }
        }
        if (Input.GetKey(Keybindings.strafeLeft))
        {
            if (Input.GetKey(Keybindings.sprint))
            {
                localMovement.SprintStrafeLeft();
            }
            else
            {
                localMovement.WalkStrafeLeft();
            }
        }
        if (Input.GetKey(Keybindings.strafeRight))
        {
            if (Input.GetKey(Keybindings.sprint))
            {
                localMovement.SprintStrafeRight();
            }
            else
            {
                localMovement.WalkStrafeRight();
            }
        }
        if (Input.GetKey(Keybindings.jumpUp))
        {
            localMovement.Jump();
        }

        //checks when state is changed
        if (Input.GetKeyDown(Keybindings.forWard) || Input.GetKeyDown(Keybindings.backWard) || Input.GetKeyDown(Keybindings.strafeLeft) || Input.GetKeyDown(Keybindings.strafeRight))
        {
            localAnim.ChangeStateTrigger();
        }
        if (Input.GetKeyUp(Keybindings.forWard) || Input.GetKeyUp(Keybindings.backWard) || Input.GetKeyUp(Keybindings.strafeLeft) || Input.GetKeyUp(Keybindings.strafeRight))
        {
            localAnim.ChangeStateTrigger();
        }
        if (Input.GetKeyDown(Keybindings.sprint))
        {
            localAnim.ChangeStateTrigger();
        }
        if (Input.GetKeyUp(Keybindings.sprint))
        {
            localAnim.ChangeStateTrigger();
        }

        //allows movement animations while turning
        if (Input.GetAxis("Mouse X") != 0)
        {
            localAnim.ChangeStateTrigger();
        }

    }

    //rotation input checker
    void PlayerRotationInput()
    {
        localMovement.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * 0.75f, 0));
        playerCam.transform.localEulerAngles = new Vector3(playerCam.transform.localEulerAngles.x - Input.GetAxis("Mouse Y"), playerCam.transform.localEulerAngles.y, playerCam.transform.localEulerAngles.z);
    }

    //syncs main camera to player gameobject
    void PlayerConstrainCam()
    {
        if (playerCam.transform.localEulerAngles.x > 60 && playerCam.transform.localEulerAngles.x < 300)
        {
            if (Mathf.Abs(playerCam.transform.localEulerAngles.x - 60) < Mathf.Abs(playerCam.transform.localEulerAngles.x - 300))
            {
                playerCam.transform.localRotation = Quaternion.Euler(60, playerCam.transform.localRotation.y, 0);
            }
            else if (Mathf.Abs(playerCam.transform.localEulerAngles.x - 60) > Mathf.Abs(playerCam.transform.localEulerAngles.x - 300))
            {
                playerCam.transform.localRotation = Quaternion.Euler(300, playerCam.transform.localRotation.y, 0);
            }

        }
    }


}
