using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHandler : MonoBehaviour {
    //handles deaths

    //reference to this entity's animation controller
    public AnimationControllerShell entityAnimController;

    //transform joints
    public Transform headJoint;
    public Transform middleSpineJoint;
    public Transform lowerSpineJoint;

    public Transform rArmJoint;
    public Transform lArmJoint;

    public Transform rElbowJoint;
    public Transform lElbowJoint;

    public Transform rHipJoint;
    public Transform lHipJoint;

    public Transform rKneeJoint;
    public Transform lKneeJoint;

    public Transform rFootJoint;
    public Transform lFootJoint;

    //rigidbody mass values for each joint
    public float headMass;
    public float middleSpineMass;
    public float lowerSpineMass;

    public float rArmMass;
    public float lArmMass;

    public float rElbowMass;
    public float lElbowMass;

    public float rHipMass;
    public float lHipMass;

    public float rKneeMass;
    public float lKneeMass;

    public float rFootMass;
    public float lFootMass;

    //is death sequence initiated?
    public bool entityDeathSequenceInit = false;

    // Use this for initialization
    void Start() {
        entityAnimController = gameObject.GetComponent<AnimationControllerShell>();

    }

    // Update is called once per frame
    void Update() {
        if (entityDeathSequenceInit == true)
        {
            SyncAnimationGroupBones();
        }
    }

    //activates death sequence(ragdoll)
    public void ActivateDeathSequence()
    {
        //disables all animators
        foreach (Animator m in gameObject.GetComponentsInChildren<Animator>())
        {
            m.enabled = false;
            if (m.transform.gameObject.GetComponent<Rigidbody>())
            {
                foreach (Rigidbody r in m.transform.gameObject.GetComponents<Rigidbody>())
                {
                    r.isKinematic = false;
                }
            }
        }
        //re-enables the ragdoll by adding a CharacterJoint to all necessary transforms & applies saved data to them
        EnableRagDoll();
        entityDeathSequenceInit = true;
    }

    //transcribes info from Joint to a JointData Class
    public JointData TranscribeJointData(CharacterJoint characterJoint)
    {
        JointData newJointData = new JointData();
        newJointData.ConnectedBody = characterJoint.connectedBody;
        newJointData.AnchorData = characterJoint.anchor;
        newJointData.AxisData = characterJoint.axis;
        newJointData.AutoConfigureConnected = true;
        newJointData.SwingAxis = characterJoint.swingAxis;

        newJointData.LowTwistLimit = characterJoint.lowTwistLimit.limit;
        newJointData.LowTwistBounciness = characterJoint.lowTwistLimit.bounciness;
        newJointData.LowTwistContactDistance = characterJoint.lowTwistLimit.contactDistance;

        newJointData.HighTwistLimit = characterJoint.highTwistLimit.limit;
        newJointData.HighTwistBounciness = characterJoint.highTwistLimit.bounciness;
        newJointData.HighTwistContactDistance = characterJoint.highTwistLimit.contactDistance;

        newJointData.SwingLimitSpring = characterJoint.swingLimitSpring.spring;
        newJointData.SwingLimitDamper = characterJoint.swingLimitSpring.damper;

        newJointData.SwingOneLimit = characterJoint.swing1Limit.limit;
        newJointData.SwingOneBounciness = characterJoint.swing1Limit.bounciness;
        newJointData.SwingOneContactDistance = characterJoint.swing1Limit.contactDistance;

        newJointData.SwingTwoLimit = characterJoint.swing2Limit.limit;
        newJointData.SwingTwoBounciness = characterJoint.swing2Limit.bounciness;
        newJointData.SwingTwoContactDistance = characterJoint.swing2Limit.contactDistance;

        return newJointData;
    }

    //transcribes data from JointData to an actual CharacterJoint
    public void TranscribeToCharJoint(ref CharacterJoint selectedJoint, JointData savedJointData) {
        selectedJoint.anchor = savedJointData.AnchorData;
        selectedJoint.axis = savedJointData.AxisData;
        selectedJoint.autoConfigureConnectedAnchor = savedJointData.AutoConfigureConnected;
        selectedJoint.swingAxis = savedJointData.SwingAxis;
        selectedJoint.connectedBody = savedJointData.ConnectedBody;

        selectedJoint.lowTwistLimit = new SoftJointLimit
        {
            limit = savedJointData.LowTwistLimit,
            bounciness = savedJointData.LowTwistBounciness,
            contactDistance = savedJointData.LowTwistContactDistance

        };

        selectedJoint.highTwistLimit = new SoftJointLimit
        {
            limit = savedJointData.HighTwistLimit,
            bounciness = savedJointData.HighTwistBounciness,
            contactDistance = savedJointData.HighTwistContactDistance
        };

        selectedJoint.swingLimitSpring = new SoftJointLimitSpring
        {
            spring = savedJointData.SwingLimitSpring,
            damper = savedJointData.SwingLimitDamper
        };

        selectedJoint.swing1Limit = new SoftJointLimit
        {
            limit = savedJointData.SwingOneLimit,
            bounciness = savedJointData.SwingOneBounciness,
            contactDistance = savedJointData.SwingOneContactDistance
        };

        selectedJoint.swing2Limit = new SoftJointLimit
        {
            limit = savedJointData.SwingTwoLimit,
            bounciness = savedJointData.SwingTwoBounciness,
            contactDistance = savedJointData.SwingTwoContactDistance
        };
    }

    //saves data on startup, destroys Character Joint, and disables rigidbody
    public void DisableRagdoll()
    {
        //assigns transforms
        Transform main = gameObject.transform.Find("Humanoid M Template/Humanoid_Armature");
        headJoint = main.Find("lower_spine/middle_spine/upper_spine/upper_spine_018/upper_spine_019");
        middleSpineJoint = main.Find("lower_spine/middle_spine");
        lowerSpineJoint = main.Find("lower_spine");

        rArmJoint = main.Find("lower_spine/middle_spine/upper_spine/upper_arm_L_001");
        lArmJoint = main.Find("lower_spine/middle_spine/upper_spine/upper_arm_R");

        rElbowJoint = main.Find("lower_spine/middle_spine/upper_spine/upper_arm_L_001/lower_arm_L_001");
        lElbowJoint = main.Find("lower_spine/middle_spine/upper_spine/upper_arm_R/lower_arm_R");

        rHipJoint = main.Find("lower_spine/upper_leg_L_001");
        lHipJoint = main.Find("lower_spine/upper_leg_R");

        rKneeJoint = main.Find("lower_spine/upper_leg_L_001/lower_leg_L_001");
        lKneeJoint = main.Find("lower_spine/upper_leg_R/lower_leg_R");

        rFootJoint = main.Find("lower_leg_L_002/upper_foot_L_001");
        lFootJoint = main.Find("lower_leg_R_001/upper_foot_R");






        //disables all rigidbodies
        headJoint.GetComponent<Rigidbody>().isKinematic = true;
        middleSpineJoint.GetComponent<Rigidbody>().isKinematic = true;
        lowerSpineJoint.GetComponent<Rigidbody>().isKinematic = true;

        rArmJoint.GetComponent<Rigidbody>().isKinematic = true;
        lArmJoint.GetComponent<Rigidbody>().isKinematic = true;

        rElbowJoint.GetComponent<Rigidbody>().isKinematic = true;
        lElbowJoint.GetComponent<Rigidbody>().isKinematic = true;

        rHipJoint.GetComponent<Rigidbody>().isKinematic = true;
        lHipJoint.GetComponent<Rigidbody>().isKinematic = true;

        rKneeJoint.GetComponent<Rigidbody>().isKinematic = true;
        lKneeJoint.GetComponent<Rigidbody>().isKinematic = true;

        rFootJoint.GetComponent<Rigidbody>().isKinematic = true;
        lFootJoint.GetComponent<Rigidbody>().isKinematic = true;

        //disables ragdoll colliders
        headJoint.GetComponent<Collider>().enabled = false;
        middleSpineJoint.GetComponent<Collider>().enabled = false;
        lowerSpineJoint.GetComponent<Collider>().enabled = false;

        rArmJoint.GetComponent<Collider>().enabled = false;
        lArmJoint.GetComponent<Collider>().enabled = false;

        rElbowJoint.GetComponent<Collider>().enabled = false;
        lElbowJoint.GetComponent<Collider>().enabled = false;

        rHipJoint.GetComponent<Collider>().enabled = false;
        lHipJoint.GetComponent<Collider>().enabled = false;

        rKneeJoint.GetComponent<Collider>().enabled = false;
        lKneeJoint.GetComponent<Collider>().enabled = false;

        rFootJoint.GetComponent<Collider>().enabled = false;
        lFootJoint.GetComponent<Collider>().enabled = false;

    }

    //applies character joints, reattaches Character Joints, applies Saved Info and re-enables rigidbodies
    public void EnableRagDoll()
    {
        //enables all rigidbodies
        headJoint.GetComponent<Rigidbody>().isKinematic = false;
        middleSpineJoint.GetComponent<Rigidbody>().isKinematic = false;
        lowerSpineJoint.GetComponent<Rigidbody>().isKinematic = false;

        rArmJoint.GetComponent<Rigidbody>().isKinematic = false;
        lArmJoint.GetComponent<Rigidbody>().isKinematic = false;

        rElbowJoint.GetComponent<Rigidbody>().isKinematic = false;
        lElbowJoint.GetComponent<Rigidbody>().isKinematic = false;

        rHipJoint.GetComponent<Rigidbody>().isKinematic = false;
        lHipJoint.GetComponent<Rigidbody>().isKinematic = false;

        rKneeJoint.GetComponent<Rigidbody>().isKinematic = false;
        lKneeJoint.GetComponent<Rigidbody>().isKinematic = false;

        rFootJoint.GetComponent<Rigidbody>().isKinematic = false;
        lFootJoint.GetComponent<Rigidbody>().isKinematic = false;

        //disables collision; sets collision layer to 12, which is "dead ground colliders"
        //gameObject.GetComponent<Rigidbody>().isKinematic = true;
        gameObject.layer = 13;

        headJoint.gameObject.layer = 12;
        middleSpineJoint.gameObject.layer = 12;
        lowerSpineJoint.gameObject.layer = 12;

        rArmJoint.gameObject.layer = 12;
        lArmJoint.gameObject.layer = 12;

        rElbowJoint.gameObject.layer = 12;
        lElbowJoint.gameObject.layer = 12;

        rHipJoint.gameObject.layer = 12;
        lHipJoint.gameObject.layer = 12;

        rKneeJoint.gameObject.layer = 12;
        lKneeJoint.gameObject.layer = 12;

        rFootJoint.gameObject.layer = 12;
        lFootJoint.gameObject.layer = 12;

        //enable ragdoll colliders
        headJoint.GetComponent<Collider>().enabled = true;
        middleSpineJoint.GetComponent<Collider>().enabled = true;
        lowerSpineJoint.GetComponent<Collider>().enabled = true;

        rArmJoint.GetComponent<Collider>().enabled = true;
        lArmJoint.GetComponent<Collider>().enabled = true;

        rElbowJoint.GetComponent<Collider>().enabled = true;
        lElbowJoint.GetComponent<Collider>().enabled = true;

        rHipJoint.GetComponent<Collider>().enabled = true;
        lHipJoint.GetComponent<Collider>().enabled = true;

        rKneeJoint.GetComponent<Collider>().enabled = true;
        lKneeJoint.GetComponent<Collider>().enabled = true;

        rFootJoint.GetComponent<Collider>().enabled = true;
        lFootJoint.GetComponent<Collider>().enabled = true;


    }

    //sync position and rotation of all joints in armatures in the animation group
    public void SyncAnimationGroupBones()
    {
        foreach (Animator m in entityAnimController.animatorGroup)
        {
            Transform armatureToMove = m.transform.Find("Humanoid_Armature");

            //syncs head
            armatureToMove.Find("lower_spine/middle_spine/upper_spine/upper_spine_018/upper_spine_019").position = headJoint.position;
            armatureToMove.Find("lower_spine/middle_spine/upper_spine/upper_spine_018/upper_spine_019").rotation = headJoint.rotation;

            //syncs middle spine
            armatureToMove.Find("lower_spine/middle_spine").position = middleSpineJoint.position;
            armatureToMove.Find("lower_spine/middle_spine").rotation = middleSpineJoint.rotation;

            //syncs lower spine
            armatureToMove.Find("lower_spine").position = lowerSpineJoint.position;
            armatureToMove.Find("lower_spine").rotation = lowerSpineJoint.rotation;

            //syncs left arm
            armatureToMove.Find("lower_spine/middle_spine/upper_spine/upper_arm_R").position = lArmJoint.position;
            armatureToMove.Find("lower_spine/middle_spine/upper_spine/upper_arm_R").rotation = lArmJoint.rotation;

            //syncs left elbow
            armatureToMove.Find("lower_spine/middle_spine/upper_spine/upper_arm_R/lower_arm_R").position = lElbowJoint.position;
            armatureToMove.Find("lower_spine/middle_spine/upper_spine/upper_arm_R/lower_arm_R").rotation = lElbowJoint.rotation;


            //syncs right arm
            armatureToMove.Find("lower_spine/middle_spine/upper_spine/upper_arm_L_001").position = rArmJoint.position;
            armatureToMove.Find("lower_spine/middle_spine/upper_spine/upper_arm_L_001").rotation = rArmJoint.rotation;

            //syncs right elbow
            armatureToMove.Find("lower_spine/middle_spine/upper_spine/upper_arm_L_001/lower_arm_L_001").position = rElbowJoint.position;
            armatureToMove.Find("lower_spine/middle_spine/upper_spine/upper_arm_L_001/lower_arm_L_001").rotation = rElbowJoint.rotation;

            //syncs left hip
            armatureToMove.Find("lower_spine/upper_leg_R").position = lHipJoint.position;
            armatureToMove.Find("lower_spine/upper_leg_R").rotation = lHipJoint.rotation;

            //syncs left knee
            armatureToMove.Find("lower_spine/upper_leg_R/lower_leg_R").position = lKneeJoint.position;
            armatureToMove.Find("lower_spine/upper_leg_R/lower_leg_R").rotation = lKneeJoint.rotation;

            //syncs left foot
            armatureToMove.Find("lower_leg_R_001/upper_foot_R").position = lFootJoint.position;
            armatureToMove.Find("lower_leg_R_001/upper_foot_R").rotation = lFootJoint.rotation;

            //syncs right hip
            armatureToMove.Find("lower_spine/upper_leg_L_001").position = rHipJoint.position;
            armatureToMove.Find("lower_spine/upper_leg_L_001").rotation = rHipJoint.rotation;

            //syncs right knee
            armatureToMove.Find("lower_spine/upper_leg_L_001/lower_leg_L_001").position = rKneeJoint.position;
            armatureToMove.Find("lower_spine/upper_leg_L_001/lower_leg_L_001").rotation = rKneeJoint.rotation;

            //syncs right foot
            armatureToMove.Find("lower_leg_L_002/upper_foot_L_001").position = rFootJoint.position;
            armatureToMove.Find("lower_leg_L_002/upper_foot_L_001").rotation = rFootJoint.rotation;
        }
    }
    

}
