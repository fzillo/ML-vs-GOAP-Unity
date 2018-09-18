using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    Rigidbody bombRB;
    public GameObject attachedToGO;

    Vector3 startPos;
    MasterAreaController masterAreaControllerInstance;

    float posXToGo;
    float posYToGo;
    float posZToGo;

    public float posYOffset = 0.5f;

    public bool isPickedUp;
    public bool isDetonated;

    void Start()
    {
        masterAreaControllerInstance = FindObjectOfType<MasterAreaController>();
        bombRB = GetComponent<Rigidbody>();
        startPos = bombRB.transform.position;
        Debug.Log(this.gameObject.tag + "" + startPos);
        posYToGo = bombRB.transform.position.y;
        isPickedUp = false;
        isDetonated = false;

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
        if ((this.gameObject.tag == "mBomb" && col.gameObject.tag == "mMonster")
        || (this.gameObject.tag == "aBomb" && col.gameObject.tag == "aMonster"))
        {
            attachedToGO = col.gameObject;
            attachedToGO.GetComponentInChildren<Monster>().SetHasBomb(true);
            isPickedUp = true;

            //Rewarding Agent
            MMonsterAgent mAgent = attachedToGO.GetComponentInChildren<MMonsterAgent>();
            if (mAgent != null)
                mAgent.RewardAgentForPickingUpBomb();
        }
    }

    public void ActivateBomb()
    {
        this.gameObject.SetActive(true);
        Debug.Log("Activate Bomb " + this.gameObject.tag + " at " + startPos);
    }

    public void DetonateBomb()
    {
        Debug.Log("Boom!");
        isDetonated = true;

        //Rewarding Agent
        MMonsterAgent mAgent = attachedToGO.GetComponentInChildren<MMonsterAgent>();
        if (mAgent != null)
            mAgent.RewardAgentForDetonatingBomb();
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
    }

    //TODO Refactor
    public void ResetBombMidGame()
    {
        //TODO ResetTimer?
        if (attachedToGO != null)
        {
            attachedToGO.GetComponentInChildren<Monster>().SetHasBomb(false);
            attachedToGO = null;
        }

        if (this.tag == "mBomb")
        {
            if (!(masterAreaControllerInstance.areaNorth.conqueredByTeamM && masterAreaControllerInstance.areaSouth.conqueredByTeamM))
            {

                this.gameObject.SetActive(false);
            }
        }
        else if (this.tag == "aBomb")
        {
            if (!(masterAreaControllerInstance.areaNorth.conqueredByTeamA && masterAreaControllerInstance.areaSouth.conqueredByTeamA))
            {

                this.gameObject.SetActive(false);
            }
        }


        bombRB.transform.position = startPos;
        isPickedUp = false;
        isDetonated = false;
    }
}
