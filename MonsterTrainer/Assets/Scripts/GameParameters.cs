using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameParameters : MonoBehaviour
{

    //the ml-agents needs way higher values for it to do anything
    //for the ml-agent it needs to be set around 1f
    public float monsterAccelerationForward = 1f;  //this is with AddForce()  //TODO cap speed, so it reacts like with 0.5f OR rewardgoingfast!
    public float monsterAccelerationSidewaysAndBack = 0.75f;  //this is with AddForce()   //TODO cap speed, so it reacts like with 0.3f OR rewardgoingfast!
    public float monsterRotationSpeed = 200f;

    public float knockBackMultiplier = 10f;

    //this is with Rigidbody.Translate()
    //public float monsterSpeed = 10f;
    //public float monsterSpeedModifierSidewaysAndBack = 0.7f;
    //public float monsterRotationSpeed = 5f;

    //public float monsterHealth = 100f;
    //public float monsterBaseDamage = 40f;
    //public int monsterDamageModifierEffective = 2;
    //public int monsterDamageModifierIneffective = 1 / 2;
    //public int secondsToWin = 5;
}
