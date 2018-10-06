using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class MLMonsterAgent : Agent
{
    MonsterTrainerAcademy academy;
    MasterAreaController masterAreaControl;
    GameParameters gParameters;
    //GameController gameControllerInstance;

    Vector3 rotateDir;
    Vector3 dirToGo;

    public Rigidbody monsterRB;
    public Monster thisMonster;

    private RayPerception rayPer;


    public override void InitializeAgent()
    {

        //gameControllerInstance = FindObjectOfType<GameController>();
        masterAreaControl = FindObjectOfType<MasterAreaController>();
        academy = FindObjectOfType<MonsterTrainerAcademy>();
        gParameters = FindObjectOfType<GameParameters>();
        rayPer = GetComponent<RayPerception>();
        monsterRB = GetComponent<Rigidbody>();

    }

    public override void CollectObservations()
    {
        float[] rayAngles = { 20f, 90f, 160f, 45f, 135f, 70f, 110f };
        float[] rayAngles2 = { 45f, 90f, 135f };
        float[] rayAngles3 = { 90f };

        string[] detectableObjects = { "teamMLBomb", "teamGOAPBomb", "mlMonster", "goapMonster", "mlMonsterHead", "goapMonsterHead" };
        string[] detectableObjects2 = { "deadzone", "areanorth", "areasouth", "teamMLStartZone", "teamGOAPStartZone" };
        AddVectorObs(rayPer.Perceive(35f, rayAngles, detectableObjects, 0f, 0f));
        AddVectorObs(rayPer.Perceive(10f, rayAngles2, detectableObjects2, 0f, -1f));
        AddVectorObs(rayPer.Perceive(1f, rayAngles3, detectableObjects2, 0f, -5f));
        AddVectorObs(masterAreaControl.areaNorth.conqueredByTeamML);
        AddVectorObs(masterAreaControl.areaSouth.conqueredByTeamML);
        AddVectorObs(this.GetComponentInParent<Monster>().hasBomb);
        AddVectorObs(transform.InverseTransformDirection(monsterRB.velocity.normalized));
    }

    public override void AgentAction(float[] vectorAction, string textAction)
    {
        //Existential Punishment, implemented to keep the Agent from slacking off
        PunishAgentForExisting();

        Move(vectorAction);

    }

    public override void AgentReset()
    {
        //gameControllerInstance.ResetGame();
    }

    public void Move(float[] act)
    {
        int forwardAxis = Mathf.FloorToInt(act[0]);
        int rotateAxis = Mathf.FloorToInt(act[1]);
        //int rightAxis = Mathf.FloorToInt(act[2]);

        float dirX = 0;
        float rotY = 0;
        float dirZ = 0;

        switch (forwardAxis)
        {
            case 0:
                break;
            case 1:
                dirZ = 1f;
                RewardAgentForMovingForward();
                break;
            case 2:
                dirZ = -1f;
                break;
        }
        switch (rotateAxis)
        {
            case 0:
                break;
            case 1:
                rotY = 1f;
                break;
            case 2:
                rotY = -1f;
                break;
        }
        /*
        switch (rightAxis)
        {
            case 0:
                break;
            case 1:
                dirX = 1f;
                break;
            case 2:
                dirX = -1f;
                break;
        }
        */

        SetMoveParameters(dirX, rotY, dirZ);
    }

    public void SetMoveParameters(float dirX, float rotY, float dirZ)
    {

        rotateDir = transform.up * rotY;

        dirToGo = Vector3.zero;
        Vector3 dirForward = Vector3.zero;
        //Vector3 dirSideways = Vector3.zero;

        if (dirZ > 0)
            dirForward = transform.forward * dirZ * gParameters.monsterAccelerationForward;
        else if (dirZ < 0)
            //when moving backwards move slower (value should be lower)
            dirForward = transform.forward * dirZ * gParameters.monsterAccelerationSidewaysAndBack;

        //when moving sidewards move slower (value should be lower)
        //dirSideways = transform.right * dirX * gParameters.monsterAccelerationSidewaysAndBack;
        dirToGo = dirForward
        //+ dirSideways
        ;
    }

    void FixedUpdate()
    {
        if (this.tag == "mlMonster")
        {
            transform.Rotate(rotateDir, Time.deltaTime * gParameters.monsterRotationSpeed);
            monsterRB.AddForce(dirToGo,
            //ForceMode.Impulse //TODO? isn't bad either!
            ForceMode.VelocityChange
            );
        }
    }

    public void RewardAgentForKnockingEnemy()
    {
        AddReward(academy.rewardForKnockingEnemy);
        Debug.Log("RewardAgentForKnockingEnemy " + academy.rewardForKnockingEnemy + " " + this.gameObject);
    }

    public void RewardAgentForDamagingEnemy()
    {
        AddReward(academy.rewardForDamagingEnemy);
        Debug.Log("RewardAgentForDamagingEnemy " + academy.rewardForDamagingEnemy + " " + this.gameObject);
    }

    public void RewardAgentForConqueringArea()
    {
        AddReward(academy.rewardForConqueringArea);
        Debug.Log("RewardAgentForConqueringArea " + academy.rewardForConqueringArea + " " + this.gameObject);
    }

    public void RewardAgentForPickingUpBomb()
    {
        AddReward(academy.rewardForPickingUpBomb);
        Debug.Log("RewardAgentForPickingUpBomb " + academy.rewardForPickingUpBomb + " " + this.gameObject);
    }

    public void PunishAgentForLosingBomb()
    {
        AddReward(academy.punishmentForLosingBomb);
        Debug.Log("PunishAgentForLosingBomb " + academy.punishmentForLosingBomb + " " + this.gameObject);
    }

    public void RewardAgentForDetonatingBomb()
    {
        AddReward(academy.rewardForDetonatingBomb);
        Debug.Log("RewardAgentForDetonatingBomb" + academy.rewardForDetonatingBomb + " " + this.gameObject);
    }

    public void PunishAgentForExisting()
    {
        AddReward(academy.punishmentForExisting);
        //Debug.Log("PunishAgentForExisting " + academy.punishmentForExisting + " " + this.gameObject);
    }

    public void PunishAgentForDying()
    {
        AddReward(academy.punishmentForDying);
        Debug.Log("PunishAgentForDying " + academy.punishmentForDying + " " + this.gameObject);
    }

    public void RewardAgentForWinning()
    {
        AddReward(academy.rewardForWinning);
        Debug.Log("RewardAgentForWinning " + academy.rewardForWinning + " " + this.gameObject);
    }

    public void PunishAgentForLosing()
    {
        AddReward(academy.punishmentForLosing);
        Debug.Log("PunishAgentForLosing " + academy.punishmentForLosing + " " + this.gameObject);
    }

    public void RewardAgentForMovingForward()
    {
        AddReward(academy.rewardForMovingForward);
        Debug.Log("RewardAgentForMovingForward " + academy.rewardForMovingForward + " " + this.gameObject);
    }
}
