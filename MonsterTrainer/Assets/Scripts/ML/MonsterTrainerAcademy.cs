using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class MonsterTrainerAcademy : Academy
{
    GameController gameControllerInstance;
    GOAPActionController goapActionControllerInstance;

    TeamController teamMLController;
    TeamController teamGOAPController;

    List<Monster> teamMLMonsterList;
    List<Monster> teamGOAPMonsterList;

    public Brain brainToTrain;

    public bool curriculumActive = false;

    // [HideInInspector]
    public bool fullfillObjectivesCurriculum = false;
    public enum TrainingPhasesFullfillObjectives { phaseConquerOneArea, phaseConquerBothAreas, phasePickupBomb, phaseDeliverBomb };

    // [HideInInspector]
    public bool attackEnemiesCurriculum = false;
    public enum TrainingPhasesAttackEnemies { phaseStaticEnemies, phaseMovingEnemies, phaseAttackingEnemies };

    // [HideInInspector]
    //TODO rename currentPhase?
    public int maximumPhase = 0;
    //Dictionary<int, bool> activatedPhasesForGoap = new Dictionary<int, bool>();
    List<int> activatedTrainingPhasesForGoap = new List<int>();


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

        goapActionControllerInstance = FindObjectOfType<GOAPActionController>();

        GameObject teamMLControllerGameObject = GameObject.Find("TeamMLController");
        teamMLController = teamMLControllerGameObject.GetComponent<TeamController>();
        teamMLMonsterList = teamMLController.teamMonsterList;

        GameObject teamGOAPControllerGameObject = GameObject.Find("TeamGOAPController");
        teamGOAPController = teamGOAPControllerGameObject.GetComponent<TeamController>();
        teamGOAPMonsterList = teamGOAPController.teamMonsterList;

        if (curriculumActive)
        {
            maximumPhase = (int)resetParameters["maximum_phase"];

            Debug.Log("BrainToTrain: " + brainToTrain.name);
            fullfillObjectivesCurriculum = brainToTrain.name.Equals("FullfillObjectiveBrain");
            attackEnemiesCurriculum = brainToTrain.name.Equals("AttackEnemiesBrain");

            foreach (Monster monsterEntity in teamMLMonsterList)
            {
                Debug.Log("!monsterEntity.deactivatedAtStart " + !monsterEntity.deactivatedAtStart + " monsterEntity " + monsterEntity);
                if (!monsterEntity.deactivatedAtStart)
                {
                    MLMonsterAgent mlAgent = monsterEntity.GetComponent<MLMonsterAgent>();
                    mlAgent.brain = brainToTrain;
                }
            }

            if (attackEnemiesCurriculum)
            {
                ActivateAttackEnemiesCurriculumPhase(maximumPhase);
            }
        }

    }

    public override void AcademyReset()
    {
        gameControllerInstance.ResetGame();

        if (curriculumActive)
        {
            maximumPhase = (int)resetParameters["maximum_phase"];

            if (attackEnemiesCurriculum && !activatedTrainingPhasesForGoap.Contains(maximumPhase))
            {
                ActivateAttackEnemiesCurriculumPhase(maximumPhase);
                activatedTrainingPhasesForGoap.Add(maximumPhase);
            }
        }
    }

    public override void AcademyStep()
    {

    }


    public void ActivateAttackEnemiesCurriculumPhase(int phaseIndex)
    {
        if (!attackEnemiesCurriculum)
        {
            return;
        }
        Debug.Log("ActivateAttackEnemiesCurriculumPhase " + phaseIndex);

        switch (phaseIndex)
        {
            case (int)MonsterTrainerAcademy.TrainingPhasesAttackEnemies.phaseStaticEnemies:
                goapActionControllerInstance.setGoapActionsForCurriculumLearningPhaseStaticEnemies(teamGOAPMonsterList);
                break;
            case (int)MonsterTrainerAcademy.TrainingPhasesAttackEnemies.phaseMovingEnemies:
                goapActionControllerInstance.setGoapActionsForCurriculumLearningPhaseMovingEnemies(teamGOAPMonsterList);
                break;
            case (int)MonsterTrainerAcademy.TrainingPhasesAttackEnemies.phaseAttackingEnemies:
                goapActionControllerInstance.setGoapActionsForCurriculumLearningPhaseAttackingEnemies(teamGOAPMonsterList);
                break;
            default:
                break;
        }
    }
}
