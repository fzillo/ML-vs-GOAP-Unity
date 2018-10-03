using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    public TeamController thisTeamControler;

    public bool isAlive;
    public bool hasBomb;

    public bool deactivatedAtStart = true;

    GameParameters gParameters;

    Rigidbody monsterRB;

    void Start()
    {
        gParameters = FindObjectOfType<GameParameters>();
        monsterRB = GetComponent<Rigidbody>();

        isAlive = true;
        hasBomb = false;
        deactivatedAtStart = false;
    }

    void Update()
    {

    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "deadzone")
        {

            Debug.Log("Deadzone!");
            DieAndRespawn();
        }
    }

    public void GetDamaged()
    {
        Debug.Log(this.gameObject.name + " got damaged!");
        DieAndRespawn();
    }

    public void GetKnockedBack(Vector3 vect, bool takeDamage)
    {

        monsterRB.AddForce(vect * gParameters.knockBackMultiplier, ForceMode.Impulse);
        Debug.Log(this.gameObject.name + " got knocked back!");

        if (takeDamage)
        {
            GetDamaged();
        }
    }

    public bool HasBomb()
    {
        return hasBomb;
    }

    public void SetHasBomb(bool active)
    {
        hasBomb = active;
    }

    public void DieAndRespawn()
    {
        MLMonsterAgent mlAgent = this.gameObject.GetComponentInParent<MLMonsterAgent>();
        if (mlAgent != null)
            mlAgent.PunishAgentForDying();

        if (hasBomb)
        {
            GameObject attachedBomb = thisTeamControler.teamBomb;
            Bomb bombEntity = attachedBomb.GetComponent<Bomb>();
            bombEntity.ResetBombMidGame();
            hasBomb = false;
        }

        isAlive = false;
        this.gameObject.SetActive(false);
        Debug.Log("INACTIVE!");
        Invoke("WaitForRespawnInvoke", gParameters.respawnTime);
    }

    void WaitForRespawnInvoke()
    {
        Debug.Log("ACTIVE!");
        thisTeamControler.RespawnMonster(this);
    }

    public void Reset()
    {
        this.gameObject.SetActive(true);
        hasBomb = false;
        isAlive = true;

        //GOAP
        GOAPMonsterAgent goapAgent = GetComponent<GOAPMonsterAgent>();
        if (goapAgent != null)
        {
            goapAgent.CancelPlan();
        }

    }
}
