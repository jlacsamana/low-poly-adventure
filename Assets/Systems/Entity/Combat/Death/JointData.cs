using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointData {
    //joint data
    public Rigidbody ConnectedBody { get; set; }
    public Vector3 AnchorData { get; set; }
    public Vector3 AxisData { get; set; }
    public bool AutoConfigureConnected { get; set; }
    public Vector3 SwingAxis { get; set; }

    //Twist Limit Spring
    public float Spring { get; set; }
    public float Damper { get; set; }

    //Low Twist Limit
    public float LowTwistLimit { get; set; }
    public float LowTwistBounciness { get; set; }
    public float LowTwistContactDistance { get; set; }

    //High Twist Limit
    public float HighTwistLimit { get; set; }
    public float HighTwistBounciness { get; set; }
    public float HighTwistContactDistance { get; set; }

    //Swing Limit Spring
    public float SwingLimitSpring { get; set; }
    public float SwingLimitDamper { get; set; }

    //Swing One Limit
    public float SwingOneLimit { get; set; }
    public float SwingOneBounciness { get; set; }
    public float SwingOneContactDistance { get; set; }

    //Swing Two Limit
    public float SwingTwoLimit { get; set; }
    public float SwingTwoBounciness { get; set; }
    public float SwingTwoContactDistance { get; set; }




}
