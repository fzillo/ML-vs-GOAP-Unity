using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class MonsterTrainerAcademy : Academy
{
    GameController gameControllerInstance;

    TeamController teamMLController;

    public Brain brainToTrain;

    public bool curriculumActive = false;


    [HideInInspector]
    public bool fullfillObjectivesCurriculum = false;
    public enum TrainingPhasesFullfillObjectives { phaseConquerOneArea, phaseConquerBothAreas, phasePickupBomb, phaseDeliverBomb };

    [HideInInspector]
    public bool attackEnemyCurriculum = false;
    public enum TrainingPhasesAttackEnemies { phaseStaticEnemies, phaseMovingEnemies, phaseAttackingEnemies };

    [HideInInspector]
    public int maximumPhase = 0;


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

        gameControllerInstance = FindObjectOfType<GameController>();
        Debug.Log("gameControllerInstance" + gameControllerInstance);

        gameControllerInstance.initializeGame();

        GameObject teamMLControllerGameObject = GameObject.Find("TeamMLController");
        teamMLController = teamMLControllerGameObject.GetComponent<TeamController>();

        if (curriculumActive)
        {
            maximumPhase = (int)resetParameters["maximum_phase"];

            fullfillObjectivesCurriculum = brainToTrain.tag.Equals("FullfillObjectiveBrain");
            attackEnemyCurriculum = brainToTrain.tag.Equals("AttackEnemiesBrain");

            List<GameObject> mlMonsters = teamMLController.teamMonsterList;

            foreach (GameObject monsterGameObject in mlMonsters)
            {
                Monster monsterEntity = monsterGameObject.GetComponent<Monster>();
                Debug.Log("!monsterEntity.deactivatedAtStart " + !monsterEntity.deactivatedAtStart + " monsterEntity " + monsterEntity);
                if (!monsterEntity.deactivatedAtStart)
                {
                    MLMonsterAgent mlAgent = monsterGameObject.GetComponent<MLMonsterAgent>();
                    mlAgent.brain = brainToTrain;
                }
            }
        }
    }

    public override void AcademyReset()
    {
        gameControllerInstance.ResetGame();

        if (curriculumActive)
        {
            maximumPhase = (int)resetParameters["maximum_phase"];
        }
    }

    public override void AcademyStep()
    {

    }


    public void SpawnTrainingDummiesInRandomIntervals(bool moving, bool attacking)
    {
        int nextSpawnInSteps = Random.Range(0, 1000);


    }
}
