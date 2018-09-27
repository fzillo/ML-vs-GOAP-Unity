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

    void FixedUpdate()
    {

    }

    public void Move(float dirX, float rotY, float dirZ)
    {

        Vector3 rotateDir = transform.up * rotY;
        Vector3 dirForward = Vector3.zero;
        Vector3 dirSideways = Vector3.zero;
        Vector3 dirToGo = Vector3.zero;

        if (dirZ > 0)
            dirForward = transform.forward * dirZ * gParameters.monsterAccelerationForward;
        else if (dirZ < 0)
            //when moving backwards move slower (value should be lower)
            dirForward = transform.forward * dirZ * gParameters.monsterAccelerationSidewaysAndBack;

        //when moving sidewards move slower (value should be lower)
        dirSideways = transform.right * dirX * gParameters.monsterAccelerationSidewaysAndBack;
        dirToGo = dirForward + dirSideways;

        transform.Rotate(rotateDir, Time.deltaTime * gParameters.monsterRotationSpeed);
        monsterRB.AddForce(dirToGo,
        //ForceMode.Impulse //TODO? isn't bad either!
        ForceMode.VelocityChange
        );
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
