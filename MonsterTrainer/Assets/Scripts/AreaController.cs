using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaController : MonoBehaviour
{
    MasterAreaController masterAreaControl;

    public bool conqueredByNone;
    public bool conqueredByTeamM;
    public bool conqueredByTeamA;

    public bool occupiedByTeamM;
    public bool occupiedByTeamA;

    public Material teamMmaterial;
    public Material teamAmaterial;
    public Material defaultMaterial;

    public GameObject westLine;
    public GameObject eastLine;

    void Start()
    {
        masterAreaControl = FindObjectOfType<MasterAreaController>();
    }

    void Update()
    {
        if (occupiedByTeamM && occupiedByTeamA)
        {
            changeAreaToNeutral();
        }
        else if (occupiedByTeamM && !occupiedByTeamA)
        {
            changeAreaToTeamM();
        }
        else if (!occupiedByTeamM && occupiedByTeamA)
        {
            changeAreaToTeamA();
        }
        /*else if (!occupiedByTeamM && !occupiedByTeamA)
        {
            changeAreaToNeutral();
        }*/

        occupiedByTeamM = false;
        occupiedByTeamA = false;
    }

    void FixedUpdate()
    {

    }

    public void ResetArea()
    {
        changeAreaToNeutral();
        occupiedByTeamM = false;
        occupiedByTeamA = false;
    }

    void OnTriggerStay(Collider col)
    //void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "mMonster")
        {
            occupiedByTeamM = true;
        }
        if (col.gameObject.tag == "aMonster")
        {
            occupiedByTeamA = true;
        }



    }

    void OnTriggerExit(Collider col)
    {
        /*
                if (col.gameObject.tag == "mMonster")
                {
                    occupiedByTeamM = false;
                }
                if (col.gameObject.tag == "aMonster")
                {
                    occupiedByTeamA = false;
                }
                 */
    }

    void changeAreaToNeutral()
    {
        if (!conqueredByNone)
        {
            Debug.Log(this.gameObject.tag + " changeAreaToNeutral");
            conqueredByNone = true;
            conqueredByTeamM = false;
            conqueredByTeamA = false;
            changeColorToDefault();
        }
    }


    void changeAreaToTeamM()
    {
        if (!conqueredByTeamM)
        {
            Debug.Log(this.gameObject.tag + " changeAreaToTeamM");
            conqueredByNone = false;
            conqueredByTeamM = true;
            conqueredByTeamA = false;
            changeColorToTeamM();
            masterAreaControl.NotifyAreaChanged();

            //TODO FIX REWARD AGENT
            //MMonsterAgent mAgent = gObj.GetComponentInChildren<MMonsterAgent>();
            //if (mAgent != null)
            //    mAgent.RewardAgentForConqueringArea();
        }
    }

    void changeAreaToTeamA()
    {
        if (!conqueredByTeamA)
        {
            Debug.Log(this.gameObject.tag + " changeAreaToTeamA");
            conqueredByNone = false;
            conqueredByTeamM = false;
            conqueredByTeamA = true;
            changeColorToTeamA();
            masterAreaControl.NotifyAreaChanged();
        }
    }

    public void changeColorToTeamM()
    {
        this.gameObject.GetComponent<Renderer>().material = teamMmaterial;
        westLine.GetComponent<Renderer>().material = teamMmaterial;
        eastLine.GetComponent<Renderer>().material = defaultMaterial;
    }

    public void changeColorToTeamA()
    {
        this.gameObject.GetComponent<Renderer>().material = teamAmaterial;
        eastLine.GetComponent<Renderer>().material = teamAmaterial;
        westLine.GetComponent<Renderer>().material = defaultMaterial;
    }

    public void changeColorToDefault()
    {
        this.gameObject.GetComponent<Renderer>().material = defaultMaterial;
        westLine.GetComponent<Renderer>().material = defaultMaterial;
        eastLine.GetComponent<Renderer>().material = defaultMaterial;
    }

}
