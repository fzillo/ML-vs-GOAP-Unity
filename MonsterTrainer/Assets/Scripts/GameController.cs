using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    /*
    TODO LIST  
    - refactor academy!!!
    - see if objectivestraining still works
    ~ make mlagents blind of eachother or include them in training
    - activate attackphase in attacktraining curriculum
    - refactor code (eg codestyle C#, Requirement Tags, Hide in Inspector)
    - refactor projectstructure
    ~ calculate goap costs dynamically
    ~ balance goap costs
    ~ include ml-agents in git?
    - documentation in git (pictures/video/gifs/howto)!

    ~ remove sideways sliding on goapAgent
    - bug: monster sometimes resets twice after death (because of ienumerator)
    ~ bug! goap agents slow down together with mlagents - still true?
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
