using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameParameters : MonoBehaviour
{

    //the ml-agents needs way higher values for it to do anything
    //for the ml-agent it needs to be set around 1f
    public float monsterAccelerationForward = 1f;  //this is with AddForce()  //TODO cap speed, so it reacts like with 0.5f OR rewardgoingfast!
    public float monsterAccelerationSidewaysAndBack = 0.7f;  //this is with AddForce()   //TODO cap speed, so it reacts like with 0.3f OR rewardgoingfast!
    public float monsterRotationSpeed = 150f;

    public int respawnTime = 3;

    public float knockBackMultiplier = 10f;
}
