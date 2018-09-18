using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class MonsterTrainerAcademy : Academy
{
    GameController gameControllerInstance;

    public override void InitializeAcademy()
    {
        gameControllerInstance = FindObjectOfType<GameController>();
        Debug.Log("gameControllerInstance" + gameControllerInstance);
        gameControllerInstance.initializeGame();
    }

    public override void AcademyReset()
    {
        gameControllerInstance.ResetGame();
    }

    public override void AcademyStep()
    {

    }
}
