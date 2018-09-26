using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class MonsterTrainerAcademy : Academy
{
    GameController gameControllerInstance;

    public float rewardForKnockingEnemy = 0.01f;
    public float rewardForDamagingEnemy = 0.03f;
    public float rewardForConqueringArea = 0.1f;
    public float rewardForPickingUpBomb = 0.1f;
    public float rewardForDetonatingBomb = 0.2f;
    public float rewardForWinning = 0.25f;

    public float punishmentForExisting = -1f / 30000f;
    public float punishmentForDying = -0.5f;
    public float punishmentForLosingBomb = -0.05f;
    public float punishmentForLosing = -0.25f;

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
