using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    Rigidbody bombRB;
    public GameObject attachedToGO;

    Vector3 startPos;
    MasterAreaController masterAreaControllerInstance;

    MonsterTrainerAcademy academy;

    float posXToGo;
    float posYToGo;
    float posZToGo;

    public float posYOffset = 0.5f;

    public bool isPickedUp;
    public bool isDetonated;
    public bool hasSpawned;

    void Start()
    {
        masterAreaControllerInstance = FindObjectOfType<MasterAreaController>();
        academy = FindObjectOfType<MonsterTrainerAcademy>();

        bombRB = GetComponent<Rigidbody>();
        startPos = bombRB.transform.position;
        Debug.Log(this.gameObject.tag + "" + startPos);
        posYToGo = bombRB.transform.position.y;
        isPickedUp = false;
        isDetonated = false;
        hasSpawned = false;

        this.gameObject.SetActive(false);

    }

    void FixedUpdate()
    {
        if (attachedToGO != null)
        {
            posXToGo = attachedToGO.gameObject.transform.position.x;
            posZToGo = attachedToGO.gameObject.transform.position.z;

            Vector3 posVect = new Vector3(posXToGo, posYToGo + posYOffset, posZToGo);
            bombRB.MovePosition(posVect);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if ((this.gameObject.tag == "teamMLBomb" && col.gameObject.tag == "mlMonster")
        || (this.gameObject.tag == "teamGOAPBomb" && col.gameObject.tag == "goapMonster"))
        {
            if (attachedToGO == null)
            {
                attachedToGO = col.gameObject;
                attachedToGO.GetComponentInChildren<Monster>().SetHasBomb(true);
                isPickedUp = true;

                //Rewarding Agent
                MLMonsterAgent mlAgent = attachedToGO.GetComponentInChildren<MLMonsterAgent>();
                if (mlAgent != null)
                {
                    mlAgent.RewardAgentForPickingUpBomb();
                }

                if (academy.fulfillObjectivesCurriculum && academy.activeCurriculumPhase.Equals((int)MonsterTrainerAcademy.TrainingPhasesFullfillObjectives.phasePickupBomb))
                {
                    academy.Done();
                }
            }
        }
    }

    public void ActivateBomb()
    {
        this.gameObject.SetActive(true);
        hasSpawned = true;
        Debug.Log("Activate Bomb " + this.gameObject + " at " + startPos + " hasSpawned " + hasSpawned);
    }

    public void DetonateBomb()
    {
        Debug.Log("Boom!");
        isDetonated = true;

        //Rewarding Agent
        if (attachedToGO != null)
        {
            MLMonsterAgent mlAgent = attachedToGO.GetComponentInChildren<MLMonsterAgent>();
            if (mlAgent != null)
            {
                mlAgent.RewardAgentForDetonatingBomb();
            }
        }
    }

    public void ResetBomb()
    {
        if (attachedToGO != null)
        {
            attachedToGO.GetComponentInChildren<Monster>().SetHasBomb(false);
            attachedToGO = null;
        }

        bombRB.transform.position = startPos;
        this.gameObject.SetActive(false);
        isPickedUp = false;
        isDetonated = false;
        hasSpawned = false;
    }

    public void ResetBombMidGame()
    {
        if (attachedToGO != null)
        {
            //Punishing Agent
            MLMonsterAgent mlAgent = attachedToGO.GetComponentInChildren<MLMonsterAgent>();
            if (mlAgent != null)
            {
                mlAgent.PunishAgentForLosingBomb();
            }

            attachedToGO.GetComponentInChildren<Monster>().SetHasBomb(false);
            attachedToGO = null;
        }

        if (this.tag == "teamMLBomb")
        {
            if (!(masterAreaControllerInstance.areaNorth.conqueredByTeamML && masterAreaControllerInstance.areaSouth.conqueredByTeamML))
            {

                this.gameObject.SetActive(false);
            }
        }
        else if (this.tag == "teamGOAPBomb")
        {
            if (!(masterAreaControllerInstance.areaNorth.conqueredByTeamGOAP && masterAreaControllerInstance.areaSouth.conqueredByTeamGOAP))
            {

                this.gameObject.SetActive(false);
            }
        }


        bombRB.transform.position = startPos;
        isPickedUp = false;
        isDetonated = false;
        hasSpawned = false;
    }
}
