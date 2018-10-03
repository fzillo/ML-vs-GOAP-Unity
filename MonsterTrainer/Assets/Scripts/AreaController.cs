using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaController : MonoBehaviour
{
    MasterAreaController masterAreaControl;

    MonsterTrainerAcademy academy;
    CurriculumController curriculum;

    //TODO remove here?
    TeamController teamMLController;

    public bool conqueredByNone;
    public bool conqueredByTeamML;
    public bool conqueredByTeamGOAP;

    public bool occupiedByTeamML;
    public bool occupiedByTeamGOAP;

    public Material teamMLMaterial;
    public Material teamGOAPMaterial;
    public Material defaultMaterial;

    public GameObject westLine;
    public GameObject eastLine;

    void Start()
    {
        masterAreaControl = FindObjectOfType<MasterAreaController>();

        academy = FindObjectOfType<MonsterTrainerAcademy>();
        curriculum = FindObjectOfType<CurriculumController>();

        //we need teamMController for Rewards
        GameObject teamMControllerGameObject = GameObject.Find("TeamMLController");
        teamMLController = teamMControllerGameObject.GetComponent<TeamController>();
    }

    void Update()
    {
        if (occupiedByTeamML && occupiedByTeamGOAP)
        {
            changeAreaToNeutral();
        }
        else if (occupiedByTeamML && !occupiedByTeamGOAP)
        {
            changeAreaToTeamML();
        }
        else if (!occupiedByTeamML && occupiedByTeamGOAP)
        {
            changeAreaToTeamGOAP();
        }

        occupiedByTeamML = false;
        occupiedByTeamGOAP = false;
    }

    void FixedUpdate()
    {

    }

    public void ResetArea()
    {
        changeAreaToNeutral();
        occupiedByTeamML = false;
        occupiedByTeamGOAP = false;
    }

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "mlMonster")
        {
            occupiedByTeamML = true;
        }
        if (col.gameObject.tag == "goapMonster")
        {
            occupiedByTeamGOAP = true;
        }



    }


    void changeAreaToNeutral()
    {
        if (!conqueredByNone)
        {
            Debug.Log(this.gameObject.tag + " changeAreaToNeutral");
            conqueredByNone = true;
            conqueredByTeamML = false;
            conqueredByTeamGOAP = false;
            changeColorToDefault();
        }
    }


    void changeAreaToTeamML()
    {
        if (!conqueredByTeamML)
        {
            Debug.Log(this.gameObject.tag + " changeAreaToTeamML");
            conqueredByNone = false;
            conqueredByTeamML = true;
            conqueredByTeamGOAP = false;
            changeColorToTeamML();
            masterAreaControl.NotifyAreaChanged();

            //TODO really reward all?
            List<GameObject> teamMMonsters = teamMLController.teamMonsterList;
            foreach (GameObject mMonsterEntity in teamMMonsters)
            {
                if (!mMonsterEntity.activeInHierarchy)
                {
                    continue;
                }

                MLMonsterAgent mlAgent = mMonsterEntity.GetComponentInChildren<MLMonsterAgent>();
                if (mlAgent != null)
                    mlAgent.RewardAgentForConqueringArea();
            }

            if (curriculum.curriculumActive && curriculum.maximumPhase.Equals((int)CurriculumController.Phase.phaseConquerOneArea))
            {
                academy.Done();
            }
        }
    }

    void changeAreaToTeamGOAP()
    {
        if (!conqueredByTeamGOAP)
        {
            Debug.Log(this.gameObject.tag + " changeAreaToTeamGOAP");
            conqueredByNone = false;
            conqueredByTeamML = false;
            conqueredByTeamGOAP = true;
            changeColorToTeamGOAP();
            masterAreaControl.NotifyAreaChanged();
        }
    }

    public void changeColorToTeamML()
    {
        this.gameObject.GetComponent<Renderer>().material = teamMLMaterial;
        westLine.GetComponent<Renderer>().material = teamMLMaterial;
        eastLine.GetComponent<Renderer>().material = defaultMaterial;
    }

    public void changeColorToTeamGOAP()
    {
        this.gameObject.GetComponent<Renderer>().material = teamGOAPMaterial;
        eastLine.GetComponent<Renderer>().material = teamGOAPMaterial;
        westLine.GetComponent<Renderer>().material = defaultMaterial;
    }

    public void changeColorToDefault()
    {
        this.gameObject.GetComponent<Renderer>().material = defaultMaterial;
        westLine.GetComponent<Renderer>().material = defaultMaterial;
        eastLine.GetComponent<Renderer>().material = defaultMaterial;
    }

}
