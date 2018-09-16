using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class MonsterTrainerAcademy : Academy
{
    StartPositionGenerator startPosGenerator;
    MasterGameAreaController gAreaController;

    public override void InitializeAcademy()
    {
        gAreaController = FindObjectOfType<MasterGameAreaController>();
        startPosGenerator = FindObjectOfType<StartPositionGenerator>();
        startPosGenerator.AssignRandomStartPositionsForAllMonsters();
    }

    public override void AcademyReset()
    {
        startPosGenerator.AssignRandomStartPositionsForAllMonsters();
        gAreaController.ResetAllAreas();
    }

    public override void AcademyStep()
    {

    }
}
