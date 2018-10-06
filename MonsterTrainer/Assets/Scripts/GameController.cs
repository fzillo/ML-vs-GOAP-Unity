using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    /*
    TODO LIST    
    - test AttackEnemyCurriculum
    - new phase PatrolRandomlySLOW
    ~ implement shoot again if melee doesnt work (give goap random shoot-delay)
    - implement attackPhase in AttackEnemyCurriculum with 2ML vs 1GOAP or throw it out
    - calculate goap costs dynamically
    - balance goap costs
    - refactor code (eg codestyle C#, Requirement Tags, Hide in Inspector)
    - refactor projectstructure
    ~ include ml-agents in git?
    - documentation in git (pictures/video/gifs)!

    ~ remove sideways sliding on goapAgent
    - bug: sometimes it resets twice after death
    - bug! goap agents slow down together with mlagents 
    ~ bug: when trying to move left+back the monster spins - this seems to be caused by friction with arenabase
    ~ goap attack more persistently
    ~ goap AMonsterAgent MoveAgent() -> should check if preconditions are still true
    ~ Find all Entities in Initialization?
     */




    StartPositionGenerator startPosGenerator;
    MasterAreaController masterAreaControl;

    TeamController teamMLController;
    TeamController teamGOAPController;

    // Use this for initialization
    void Start()
    {
        AssignObjects();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void AssignObjects()
    {
        if (masterAreaControl == null || startPosGenerator == null || teamMLController == null || teamGOAPController == null)
        {
            masterAreaControl = FindObjectOfType<MasterAreaController>();
            startPosGenerator = FindObjectOfType<StartPositionGenerator>();

            GameObject teamMLControllerGameObject = GameObject.Find("TeamMLController");
            GameObject teamGOAPControllerGameObject = GameObject.Find("TeamGOAPController");
            teamMLController = teamMLControllerGameObject.GetComponent<TeamController>();
            teamGOAPController = teamGOAPControllerGameObject.GetComponent<TeamController>();
        }
    }

    public void initializeGame()
    {
        AssignObjects();
        teamMLController.SetStartPostitionsForAllMonster();
        //startPosGenerator.AssignRandomStartPositionsForAllMonstersInStartzone();
    }

    public void ResetGame()
    {
        //startPosGenerator.AssignRandomStartPositionsForAllMonstersInStartzone();
        masterAreaControl.ResetAllAreas();
        teamMLController.SetStartPostitionsForAllMonster();
        teamMLController.ResetAllMonsters();
        teamGOAPController.SetStartPostitionsForAllMonster();
        teamGOAPController.ResetAllMonsters();
    }
}
