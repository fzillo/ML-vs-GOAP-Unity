using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{

    //TODO set isAlive true on reset

    GameParameters gParameters;

    Rigidbody monsterRB;

    public bool isAlive;
    public bool hasBomb;

    void Start()
    {
        gParameters = FindObjectOfType<GameParameters>();
        monsterRB = GetComponent<Rigidbody>();

        isAlive = true;
        hasBomb = false;
    }

    void Update()
    {

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
        //ForceMode.Impulse //auch nicht schlecht!
        ForceMode.VelocityChange
        );
    }

    /*
    public void Shoot(bool shootBool)
    {
        if (shootBool)
        {
            Vector3 position = transform.forward * 20f;
            //transform.TransformDirection(PolarToCartesian(rayDistance, angle));
            Debug.DrawRay(transform.position, position, Color.red, 1f, true);
            Debug.Log("Pew");
        }
    }
    */

    public void GetDamaged()
    {
        Debug.Log(this.gameObject.name + " got damaged!");
        isAlive = false;
        this.gameObject.SetActive(false);
    }

    public void GetKnockedBack(Vector3 vect)
    {
        //GetDamaged();

        monsterRB.AddForce(vect * gParameters.knockBackMultiplier, ForceMode.Impulse);
        Debug.Log(this.gameObject.name + " got knocked back!");
    }

    public bool HasBomb()
    {
        return hasBomb;
    }

    public void SetHasBomb(bool active)
    {
        hasBomb = active;
    }

}
