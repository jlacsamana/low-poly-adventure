using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControllerShell : MonoBehaviour {
    //attach this to all entities that are animated
    //allows easy access to setting animation states

    //movement states are as follows; 0 = Idle, 1 = Walk, 2 = Run

    /*weapon states are as follows; 0 = No Weapon, 1 = 1H Stab, 2 = 1H NoStab,
    3 = 1H Polearm, 4 = Staff, 5 = Bow, 6 = Crossbow, 7 = 2H Stab, 8 = 2H Nostab, 9 = 2H Polearm */

    //reference to animators in children of this component's gameobject
    public List<Animator> animatorGroup;

    public Animator coreAnim;
    public Animator coreAnimReference;
    public Animator projectileWpnAnim;

    //reference to gameobject's inventory
    public Inventory entityInventory;

    //reference to this entity's look dir object
    public Transform entityRotationObj;

    //reference to this entity's ground collider
    public Rigidbody entityGroundCollider;

    //is weapon sheathed or not
    public bool weaponIsSheathed = true;

    //current rotation of entity
    public Vector3 currentEntityRotation = new Vector3();

    //armature transform path
    readonly string upperTorsoPath = "Humanoid_Armature/lower_spine/middle_spine/upper_spine";
    readonly string headTransformPath = "Humanoid_Armature/lower_spine/middle_spine/upper_spine/upper_spine_018";
    //I messed up the rigging of the armature so left is right and right is left
    readonly string armRTransformPath = "Humanoid_Armature/lower_spine/middle_spine/upper_spine/upper_arm_L_001";
    readonly string armLTransformPath = "Humanoid_Armature/lower_spine/middle_spine/upper_spine/upper_arm_R";     

    // Use this for initialization
    void Start() {
        UpdateAnimatorList();     
        entityGroundCollider = gameObject.GetComponent<Rigidbody>();
        entityInventory = gameObject.GetComponent<Inventory>();
        entityRotationObj = gameObject.transform.Find("Entity Rotation Object");
        Invoke("AttackStateChecker", 0.01f);
        gameObject.GetComponent<DeathHandler>().DisableRagdoll();
    }

    // Update is called once per frame
    void Update() {
        if (Time.timeScale == 1)
        {
            MovementStateListener();
        }
        currentEntityRotation = gameObject.transform.eulerAngles;
    }

    private void LateUpdate()
    {
        if (Time.timeScale == 1)
        {
            RotateTorso(entityRotationObj.localEulerAngles.x);
        }
    }

    //staggers calling of animator list update
    public void DelayedUpdateAnimatorList()
    {
        Invoke("UpdateAnimatorList", Time.deltaTime);
    }

    //updates list of animators in children of this gameobject
    public void UpdateAnimatorList()
    {
        List<Animator> tempAnimatorGroup = new List<Animator>();
        animatorGroup = new List<Animator>();
        tempAnimatorGroup = new List<Animator>(gameObject.GetComponentsInChildren<Animator>());
        foreach (Animator m in tempAnimatorGroup)
        {
            if (m.transform.gameObject.name == "Humanoid M Template")
            {
                animatorGroup.Add(m);
            }
        }
        foreach (Animator m in animatorGroup)
        {
            if (m.transform.parent.name != "Equipment" && m.transform.parent.name != "Accessories")
            {
                coreAnim = m;
            }
        }

        
    }

    //call when state is supposed to change
    public void ChangeStateTrigger()
    {
        foreach (Animator anim in animatorGroup)
        {
            anim.SetTrigger("State Changed");
        }
    }

    //call when a bool changes
    public void ChangeBoolVal(string boolName, bool value)
    {
        foreach (Animator anim in animatorGroup)
        {
            anim.SetBool(boolName, value);
        }
    }

    //call when a trigger is fired
    public void ActivateTrigger(string triggerName)
    {
        foreach (Animator anim in animatorGroup)
        {
            anim.SetTrigger(triggerName);
        }
    }

    //call to reset a trigger
    public void ResetTriggerState(string triggerName)
    {
        foreach (Animator anim in animatorGroup)
        {
            anim.ResetTrigger(triggerName);
        }
    }

    //set value for a float
    public void SetFloat(string valueName, float value)
    {
        foreach (Animator anim in animatorGroup)
        {
            anim.SetFloat(valueName, value);
        }
    }

    //call when movement state changes; pass appropriate int for state 
    public void ChangeMovementState(int movementState)
    {
        foreach (Animator anim in animatorGroup)
        {
            anim.SetInteger("Movement State", movementState);
        }
    }

    //call when weapon type changes
    public void ChangeWeaponType(int weaponType)
    {
        foreach (Animator anim in animatorGroup)
        {
            anim.SetInteger("Weapon Type", weaponType);
        }
    }

    //applies random number for attack randomizer to all animators attached to entity
    public void ApplyRandomAttack(int randomNum)
    {
        foreach (Animator anim in animatorGroup)
        {
            anim.SetInteger("AttackRandomizer", randomNum);
        }
    }

    //movement state listener
    public void MovementStateListener()
    {
        //if not moving
        if (entityGroundCollider.velocity == new Vector3(0,entityGroundCollider.velocity.y, 0))
        {
            ChangeMovementState(0);
            if (Vector3.Distance(currentEntityRotation, gameObject.transform.eulerAngles) != 0)
            {
                ChangeMovementState(1);
            }
        }
        //if moving at walking speed
        else if (entityGroundCollider.velocity == (entityGroundCollider.transform.forward * gameObject.GetComponent<Entity>().currentEntitySpeed) + (entityGroundCollider.transform.up * entityGroundCollider.velocity.y) ||
            entityGroundCollider.velocity == -(entityGroundCollider.transform.forward * gameObject.GetComponent<Entity>().currentEntitySpeed) + (entityGroundCollider.transform.up * entityGroundCollider.velocity.y) ||
            entityGroundCollider.velocity == -(entityGroundCollider.transform.right * gameObject.GetComponent<Entity>().currentEntitySpeed) + (entityGroundCollider.transform.up * entityGroundCollider.velocity.y) ||
            entityGroundCollider.velocity == (entityGroundCollider.transform.right * gameObject.GetComponent<Entity>().currentEntitySpeed) + (entityGroundCollider.transform.up * entityGroundCollider.velocity.y)
            )
        {
            ChangeMovementState(1);
        }
        //if moving at running speed
        else if (entityGroundCollider.velocity == (entityGroundCollider.transform.forward * gameObject.GetComponent<Entity>().currentEntitySpeed * 2f) + (entityGroundCollider.transform.up * entityGroundCollider.velocity.y) ||
            entityGroundCollider.velocity == -(entityGroundCollider.transform.forward * gameObject.GetComponent<Entity>().currentEntitySpeed * 2f) + (entityGroundCollider.transform.up * entityGroundCollider.velocity.y) ||
            entityGroundCollider.velocity == -(entityGroundCollider.transform.right * gameObject.GetComponent<Entity>().currentEntitySpeed * 2f) + (entityGroundCollider.transform.up * entityGroundCollider.velocity.y) ||
            entityGroundCollider.velocity == (entityGroundCollider.transform.right * gameObject.GetComponent<Entity>().currentEntitySpeed * 2f) + (entityGroundCollider.transform.up * entityGroundCollider.velocity.y)
            )
        {
            ChangeMovementState(2);
        }
    }

    //attack state checker
    public void AttackStateChecker()
    {
        switch (entityInventory.entityRightHandSlot.CombatType)
        {
            case "No Weapon":
                ChangeWeaponType(0);
                break;
            case "1H Stab":
                ChangeWeaponType(1);
                break;
            case "1H NoStab":
                ChangeWeaponType(2);
                break;
            case "1H Polearm":
                ChangeWeaponType(3);
                break;
            case "Staff":
                ChangeWeaponType(4);
                break;
            case "Bow":
                ChangeWeaponType(5);
                break;
            case "Crossbow":
                ChangeWeaponType(6);
                break;
            case "2H Stab":
                ChangeWeaponType(7);
                break;
            case "2H Nostab":
                ChangeWeaponType(8);
                break;
            case "2H Polearm":
                ChangeWeaponType(9);
                break;
        }
        ChangeStateTrigger();
    }

    //rotates torso vertically
    public void RotateTorso(float rotateAmount)
    {
        float rotateAngle = 0;
        if (rotateAmount >= 320)
        {
            rotateAngle = -(Mathf.Abs(360 - rotateAmount));
        }
        else if (rotateAmount <= 40)
        {
            rotateAngle = rotateAmount ;
        }
        else if (rotateAmount > 40 && rotateAmount < 180)
        {
            rotateAngle = 40;
        }
        else if (rotateAmount < 320 && rotateAmount > 180)
        {
            rotateAngle = -(Mathf.Abs(360 - 320));
        }

        if (weaponIsSheathed == true)
        {
            //if weapon is sheathed
            foreach (Animator m in animatorGroup)
            {
                Transform upperTorso = m.transform.Find(upperTorsoPath);
                Transform head = m.transform.Find(headTransformPath);
                Transform lArm = m.transform.Find(armLTransformPath);
                Transform rArm = m.transform.Find(armRTransformPath);

                //shift rotation of upper torso
                upperTorso.localEulerAngles = new Vector3
                (upperTorso.localEulerAngles.x, upperTorso.localEulerAngles.y + (rotateAngle / 2), upperTorso.localEulerAngles.z);
                //shift rotation of head
                head.localEulerAngles = new Vector3
                (head.localEulerAngles.x, head.localEulerAngles.y + (rotateAngle / 2), head.localEulerAngles.z);
                //shifts rotation of arms
                lArm.localEulerAngles = new Vector3
                (lArm.localEulerAngles.x, lArm.localEulerAngles.y - (rotateAngle / 2), lArm.localEulerAngles.z);
                rArm.localEulerAngles = new Vector3
                (rArm.localEulerAngles.x, rArm.localEulerAngles.y - (rotateAngle / 2), rArm.localEulerAngles.z);
            }
        }
        else if (weaponIsSheathed == false)
        {
            //if weapon is not sheathed
            if (entityInventory.entityRightHandSlot.WeaponType == "One Handed")
            {
                foreach (Animator m in animatorGroup)
                {
                    Transform upperTorso = m.transform.Find(upperTorsoPath);
                    Transform head = m.transform.Find(headTransformPath);
                    Transform lArm = m.transform.Find(armLTransformPath);
                    Transform rArm = m.transform.Find(armRTransformPath);

                    //shift rotation of upper torso
                    upperTorso.localEulerAngles = new Vector3
                    (upperTorso.localEulerAngles.x, upperTorso.localEulerAngles.y + (rotateAngle / 2), upperTorso.localEulerAngles.z);
                    //shift rotation of head
                    head.localEulerAngles = new Vector3
                    (head.localEulerAngles.x, head.localEulerAngles.y + (rotateAngle / 2), head.localEulerAngles.z);
                    //shifts rotation of arms
                    rArm.localEulerAngles = new Vector3
                    (rArm.localEulerAngles.x, rArm.localEulerAngles.y + (rotateAngle / 2), rArm.localEulerAngles.z);
                    if (entityInventory.entityLeftHandSlot.WeaponType == "Shield")
                    {
                        lArm.localEulerAngles = new Vector3
                        (lArm.localEulerAngles.x, lArm.localEulerAngles.y + (rotateAngle / 2.25f), lArm.localEulerAngles.z);

                    }
                    else
                    {
                        lArm.localEulerAngles = new Vector3
                        (lArm.localEulerAngles.x, lArm.localEulerAngles.y - (rotateAngle / 2), lArm.localEulerAngles.z);
                    }
                }
            }
            else if (entityInventory.entityRightHandSlot.WeaponType == "Two Handed" ||
            entityInventory.entityRightHandSlot.WeaponType == "Projectile Weapon")
            {

                foreach (Animator m in animatorGroup)
                {
                    Transform upperTorso = m.transform.Find(upperTorsoPath);
                    Transform head = m.transform.Find(headTransformPath);
                    Transform lArm = m.transform.Find(armLTransformPath);
                    Transform rArm = m.transform.Find(armRTransformPath);

                    //shift rotation of upper torso
                    upperTorso.localEulerAngles = new Vector3
                    (upperTorso.localEulerAngles.x, upperTorso.localEulerAngles.y + (rotateAngle / 2), upperTorso.localEulerAngles.z);
                    //shift rotation of head
                    head.localEulerAngles = new Vector3
                    (head.localEulerAngles.x, head.localEulerAngles.y + (rotateAngle / 2), head.localEulerAngles.z);
                    //shifts rotation of arms
                    lArm.localEulerAngles = new Vector3
                    (lArm.localEulerAngles.x, lArm.localEulerAngles.y + (rotateAngle / 2), lArm.localEulerAngles.z);
                    rArm.localEulerAngles = new Vector3
                    (rArm.localEulerAngles.x, rArm.localEulerAngles.y + (rotateAngle / 2), rArm.localEulerAngles.z);
                }
            }
            else if (entityInventory.entityRightHandSlot.WeaponType == "Staff")
            {
                foreach (Animator m in animatorGroup)
                {
                    Transform upperTorso = m.transform.Find(upperTorsoPath);
                    Transform head = m.transform.Find(headTransformPath);
                    Transform lArm = m.transform.Find(armLTransformPath);
                    Transform rArm = m.transform.Find(armRTransformPath);

                    //shift rotation of upper torso
                    upperTorso.localEulerAngles = new Vector3
                    (upperTorso.localEulerAngles.x, upperTorso.localEulerAngles.y + (rotateAngle / 2), upperTorso.localEulerAngles.z);
                    //shift rotation of head
                    head.localEulerAngles = new Vector3
                    (head.localEulerAngles.x, head.localEulerAngles.y + (rotateAngle / 2), head.localEulerAngles.z);
                    //shifts rotation of arms
                    lArm.localEulerAngles = new Vector3
                    (lArm.localEulerAngles.x, lArm.localEulerAngles.y - (rotateAngle / 2), lArm.localEulerAngles.z);
                    rArm.localEulerAngles = new Vector3
                    (rArm.localEulerAngles.x, rArm.localEulerAngles.y + (rotateAngle / 2), rArm.localEulerAngles.z);
                }
            }
        }



    }
}
