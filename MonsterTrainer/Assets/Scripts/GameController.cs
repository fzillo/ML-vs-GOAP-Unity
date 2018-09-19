using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    /*
    TODO LIST
    - Find all Entities in Initialization
    - port to ml v0.5
    - get multibrain to work
    - calculate goap costs dynamically
    - let agent die on deathzone
    - AMonsterAgent MoveAgent() -> should check if preconditions are still true
     */




    StartPositionGenerator startPosGenerator;
    MasterAreaController masterAreaControl;

    TeamController teamMController;
    TeamController teamAController;

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
        if (masterAreaControl == null || startPosGenerator == null || teamMController == null || teamAController == null)
        {
            masterAreaControl = FindObjectOfType<MasterAreaController>();
            startPosGenerator = FindObjectOfType<StartPositionGenerator>();

            GameObject teamMControllerGameObject = GameObject.Find("TeamMController");
            GameObject teamAControllerGameObject = GameObject.Find("TeamAController");
            teamMController = teamMControllerGameObject.GetComponent<TeamController>();
            teamAController = teamAControllerGameObject.GetComponent<TeamController>();
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
        // teamMController.ResetAllMonsters();
        //teamAController.ResetAllMonsters();
    }
}
