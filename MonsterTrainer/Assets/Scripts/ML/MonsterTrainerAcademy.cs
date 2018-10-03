using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class MonsterTrainerAcademy : Academy
{
    GameController gameControllerInstance;
    CurriculumController curriculum;

    public float rewardForKnockingEnemy = 0.1f;
    public float rewardForDamagingEnemy = 0.1f;
    public float rewardForConqueringArea = 0.25f;
    public float rewardForPickingUpBomb = 0.25f;
    public float rewardForDetonatingBomb = 0.2f;
    public float rewardForWinning = 1f;
    public float rewardForMovingForward = 2f / 50000f;

    public float punishmentForExisting = -1f / 50000f;
    public float punishmentForDying = -0.5f;
    public float punishmentForLosingBomb = -0.2f;
    public float punishmentForLosing = -0.2f;

    public override void InitializeAcademy()
    {
        curriculum = FindObjectOfType<CurriculumController>();
        if (curriculum.curriculumActive)
        {
            curriculum.maximumPhase = (int)resetParameters["maximum_phase"];
        }

        gameControllerInstance = FindObjectOfType<GameController>();
        Debug.Log("gameControllerInstance" + gameControllerInstance);
        gameControllerInstance.initializeGame();
    }

    public override void AcademyReset()
    {
        gameControllerInstance.ResetGame();

        if (curriculum.curriculumActive)
        {
            curriculum.maximumPhase = (int)resetParameters["maximum_phase"];
        }
    }

    public override void AcademyStep()
    {

    }
}
