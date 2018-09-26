using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    /*
    TODO LIST
    - ML Training!
    - implement curriculum learning plan
    - get multibrain to work
    - standardize movement between mlMonsters(custom) and goapMonsters(navMeshAgent)
    - calculate goap costs dynamically
    ~ attack more persistently
    ~ AMonsterAgent MoveAgent() -> should check if preconditions are still true
    ~ include ml-agents in git?
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
        startPosGenerator.AssignRandomStartPositionsForAllMonsters();
    }

    public void ResetGame()
    {
        startPosGenerator.AssignRandomStartPositionsForAllMonsters();
        masterAreaControl.ResetAllAreas();
        teamMLController.ResetAllMonsters();
        teamGOAPController.ResetAllMonsters();
    }
}
