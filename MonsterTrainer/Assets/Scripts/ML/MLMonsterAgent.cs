using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class MLMonsterAgent : Agent
{
    MonsterTrainerAcademy academy;
    MasterAreaController masterAreaControl;

    public Rigidbody monsterRB;
    private RayPerception rayPer;
    public Monster thisMonster;

    public override void InitializeAgent()
    {
        masterAreaControl = FindObjectOfType<MasterAreaController>();
        academy = FindObjectOfType<MonsterTrainerAcademy>();
        rayPer = GetComponent<RayPerception>();
        monsterRB = GetComponent<Rigidbody>();

    }

    public override void CollectObservations()
    {
        const float rayDistance = 35f;
        float[] rayAngles = { 20f, 90f, 160f, 45f, 135f, 70f, 110f };

        //TODO disable DebugRaycasting or use less RayPerceptions for more Performance!
        string[] detectableObjects = { "deadzone", "areanorth", "areasouth", "teamMLStartZone", "teamGOAPStartZone", "teamMLBomb", "teamGOAPBomb", "mlMonster", "goapMonster", "mlMonsterHead", "goapMonsterHead" };
        AddVectorObs(rayPer.Perceive(2f, rayAngles, detectableObjects, 0f, 2.5f));
        AddVectorObs(rayPer.Perceive(rayDistance, rayAngles, detectableObjects, 0f, 0f));
        AddVectorObs(rayPer.Perceive(10f, rayAngles, detectableObjects, 0f, -1f));
        AddVectorObs(rayPer.Perceive(2f, rayAngles, detectableObjects, 0f, -2.5f));
        AddVectorObs(rayPer.Perceive(1f, rayAngles, detectableObjects, 0f, -5f));
        AddVectorObs(masterAreaControl.areaNorth.conqueredByTeamML);
        AddVectorObs(masterAreaControl.areaSouth.conqueredByTeamML);
        AddVectorObs(masterAreaControl.areaNorth.conqueredByTeamGOAP);
        AddVectorObs(masterAreaControl.areaSouth.conqueredByTeamGOAP);
        AddVectorObs(this.GetComponentInParent<Monster>().hasBomb);
        AddVectorObs(transform.InverseTransformDirection(monsterRB.velocity));
    }

    public override void AgentAction(float[] vectorAction, string textAction)
    {
        //Existential Punishment, implemented to keep the Agent from slacking off
        PunishAgentForExisting();

        Move(vectorAction);

    }

    public override void AgentReset()
    {

    }

    public void Move(float[] act)
    {
        int action = Mathf.FloorToInt(act[0]);

        float dirX = 0;
        float rotY = 0;
        float dirZ = 0;

        switch (action)
        {
            case 0:
                break;
            case 1:
                dirZ = 1f;
                break;
            case 2:
                dirZ = -1f;
                break;
            case 3:
                rotY = 1f;
                break;
            case 4:
                rotY = -1f;
                break;
            case 5:
                dirX = 1f;
                break;
            case 6:
                dirX = -1f;
                break;
        }
        thisMonster.Move(dirX, rotY, dirZ);
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
}
