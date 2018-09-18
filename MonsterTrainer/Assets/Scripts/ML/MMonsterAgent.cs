using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class MMonsterAgent : Agent
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
        string[] detectableObjects = { "deadzone", "areanorth", "areasouth", "startZoneM", "startZoneA", "mBomb", "aBomb", "mMonster", "aMonster", "mHead", "aHead" };
        AddVectorObs(rayPer.Perceive(2f, rayAngles, detectableObjects, 0f, 2.5f));
        AddVectorObs(rayPer.Perceive(rayDistance, rayAngles, detectableObjects, 0f, 0f));
        AddVectorObs(rayPer.Perceive(10f, rayAngles, detectableObjects, 0f, -1f));
        AddVectorObs(rayPer.Perceive(2f, rayAngles, detectableObjects, 0f, -2.5f));
        AddVectorObs(rayPer.Perceive(1f, rayAngles, detectableObjects, 0f, -5f));
        AddVectorObs(masterAreaControl.areaNorth.conqueredByTeamM);
        AddVectorObs(masterAreaControl.areaSouth.conqueredByTeamM);
        AddVectorObs(masterAreaControl.areaNorth.conqueredByTeamA);
        AddVectorObs(masterAreaControl.areaSouth.conqueredByTeamA);
        AddVectorObs(this.GetComponentInParent<Monster>().hasBomb);
        AddVectorObs(transform.InverseTransformDirection(monsterRB.velocity));
    }

    public override void AgentAction(float[] vectorAction, string textAction)
    {
        //Rewarding Agent (negative)
        RewardAgentForExisting();

        Move(vectorAction);

    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "deadzone")
        {
            //Rewarding Agent (negative)
            RewardAgentForDying();

            Debug.Log("Dead!");
            academy.Done();
        }
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
        //bool shootBool = false;

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
                //case 7:
                //    shootBool = true;
                //    break;
        }
        thisMonster.Move(dirX, rotY, dirZ);
        //thisMonster.Shoot(shootBool);
    }


    public void RewardAgentForKnockingEnemy()
    {
        AddReward(0.1f);
        Debug.Log("RewardAgentForKnockingEnemy 0.1f " + this.gameObject.tag);
    }

    public void RewardAgentForDamagingEnemy()
    {
        AddReward(0.3f);
        Debug.Log("RewardAgentForDamagingEnemy 0.3f " + this.gameObject.tag);
    }

    public void RewardAgentForConqueringArea()
    {
        AddReward(1f);
        Debug.Log("RewardAgentForConqueringArea 1f " + this.gameObject.tag);
    }

    public void RewardAgentForPickingUpBomb()
    {
        AddReward(1f);
        Debug.Log("RewardAgentForPickingUpBomb 1f " + this.gameObject.tag);
    }

    public void RewardAgentForDetonatingBomb()
    {
        AddReward(2f);
        Debug.Log("RewardAgentForDetonatingBomb 2f " + this.gameObject.tag);
    }

    public void RewardAgentForExisting()
    {
        AddReward(-1f / 3000);
        //Debug.Log("RewardAgentForExisting -1f / 3000f " + this.gameObject.tag);
    }

    public void RewardAgentForDying()
    {
        AddReward(-5f);
        Debug.Log("RewardAgentForDying -5f " + this.gameObject.tag);
    }
}
