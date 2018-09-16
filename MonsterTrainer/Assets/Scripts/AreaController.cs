using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaController : MonoBehaviour
{
    MasterGameAreaController gAreaController;

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
        gAreaController = FindObjectOfType<MasterGameAreaController>();
    }

    void Update()
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

        if (occupiedByTeamM && occupiedByTeamA)
        {
            changeAreaToNeutral();
        }
        else if (occupiedByTeamM && !occupiedByTeamA)
        {
            changeAreaToTeamM(col.gameObject);
        }
        else if (!occupiedByTeamM && occupiedByTeamA)
        {
            changeAreaToTeamA();
        }
        else if (!occupiedByTeamM && !occupiedByTeamA)
        {
            changeAreaToNeutral();
        }

    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "mMonster")
        {
            occupiedByTeamM = false;
        }
        if (col.gameObject.tag == "aMonster")
        {
            occupiedByTeamA = false;
        }
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


    void changeAreaToTeamM(GameObject gObj)
    {
        if (!conqueredByTeamM)
        {
            Debug.Log(this.gameObject.tag + " changeAreaToTeamM");
            conqueredByNone = false;
            conqueredByTeamM = true;
            conqueredByTeamA = false;
            changeColorToTeamM();
            gAreaController.NotifyAreaChanged();


            MMonsterAgent mAgent = gObj.GetComponentInChildren<MMonsterAgent>();
            if (mAgent != null)
                mAgent.RewardAgentForConqueringArea();
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
            gAreaController.NotifyAreaChanged();
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
