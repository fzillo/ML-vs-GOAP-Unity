using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    /*
    TODO LIST
    - Get CancelPlan to work for GoapAgents without them standing around!
    - Find all Entities in Initialization
    - port to ml v0.5
    - get multibrain to work
    - calculate goap costs dynamically
    - let agent die on deathzone
    - bug: amonsteragent is stuck if it dies on way to objective area
    - <fixed?> bug: navmeshagent sometimes breaks "Failed to create agent because it is not close enough to the NavMesh"
     */



    StartPositionGenerator startPosGenerator;
    MasterAreaController masterAreaControl;
    // Use this for initialization
    void Start()
    {
        if (masterAreaControl == null || startPosGenerator == null)
        {
            masterAreaControl = FindObjectOfType<MasterAreaController>();
            startPosGenerator = FindObjectOfType<StartPositionGenerator>();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void initializeGame()
    {
        if (masterAreaControl == null || startPosGenerator == null)
        {
            masterAreaControl = FindObjectOfType<MasterAreaController>();
            startPosGenerator = FindObjectOfType<StartPositionGenerator>();
        }
        startPosGenerator.AssignRandomStartPositionsForAllMonsters();
    }

    public void ResetGame()
    {
        startPosGenerator.AssignRandomStartPositionsForAllMonsters();
        masterAreaControl.ResetAllAreas();
    }
}
