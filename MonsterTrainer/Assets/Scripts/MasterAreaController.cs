using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterAreaController : MonoBehaviour
{
    public AreaController areaNorth;
    public AreaController areaSouth;

    public Bomb bombTeamML;
    public Bomb bombTeamGOAP;

    MonsterTrainerAcademy academy;

    //TODO remove here?
    TeamController teamMLController;

    void Start()
    {
        academy = FindObjectOfType<MonsterTrainerAcademy>();


        //we need teamMController for Rewards
        GameObject teamMControllerGameObject = GameObject.Find("TeamMLController");
        teamMLController = teamMControllerGameObject.GetComponent<TeamController>();
    }

    void Update()
    {

    }

    public void NotifyAreaChanged()
    {
        activateBombIfBothConqueredByOneTeam();
    }

    public void ResetAllAreas()
    {
        areaNorth.ResetArea();
        bombTeamML.ResetBomb();

        areaSouth.ResetArea();
        bombTeamGOAP.ResetBomb();
    }

    public void StartZoneMDetonates()
    {
        Debug.Log("Team GOAP wins the Game!");
        bombTeamGOAP.DetonateBomb();

        List<GameObject> teamMMonsters = teamMLController.teamMonsterList;
        foreach (GameObject mMonsterEntity in teamMMonsters)
        {
            if (!mMonsterEntity.activeInHierarchy)
            {
                continue;
            }

            MLMonsterAgent mlAgent = mMonsterEntity.GetComponentInChildren<MLMonsterAgent>();
            if (mlAgent != null)
            {
                mlAgent.PunishAgentForLosing();
            }
        }

        academy.Done();
    }

    public void StartZoneADetonates()
    {
        Debug.Log("Team ML wins the Game!");
        bombTeamML.DetonateBomb();

        //TODO reward all?
        List<GameObject> teamMMonsters = teamMLController.teamMonsterList;
        foreach (GameObject mMonsterEntity in teamMMonsters)
        {
            if (!mMonsterEntity.activeInHierarchy)
            {
                continue;
            }

            MLMonsterAgent mlAgent = mMonsterEntity.GetComponentInChildren<MLMonsterAgent>();
            if (mlAgent != null)
            {
                mlAgent.RewardAgentForWinning();
            }
        }

        academy.Done();


    }

    private void activateBombIfBothConqueredByOneTeam()
    {
        if (areaNorth.conqueredByTeamML && areaSouth.conqueredByTeamML)
        {
            if (academy.curriculumActive && academy.maximumPhase.Equals((int)MonsterTrainerAcademy.TrainingPhasesFullfillObjectives.phaseConquerBothAreas))
            {
                academy.Done();
            }

            bombTeamML.ActivateBomb();
        }
        else if (areaNorth.conqueredByTeamGOAP && areaSouth.conqueredByTeamGOAP)
        {
            bombTeamGOAP.ActivateBomb();
        }
    }
}
