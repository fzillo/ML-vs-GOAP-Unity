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

    //List<Monster> teamMLMonsterList;
    List<Monster> teamGOAPMonsterList;

    public Brain brainToTrain;

    public bool curriculumActive = false;
    public bool debugModeWithPlayerBrain = false;

    // [HideInInspector]
    public bool fulfillObjectivesCurriculum = false;
    public enum TrainingPhasesFullfillObjectives { phaseConquerOneArea, phaseConquerBothAreas, phasePickupBomb, phaseDeliverBomb };

    // [HideInInspector]
    public bool attackEnemiesCurriculum = false;
    public enum TrainingPhasesAttackEnemies { phaseStaticEnemies, phaseMovingEnemiesHalfSpeed, phaseMovingEnemiesFullSpeed, phaseAttackingEnemies };

    // [HideInInspector]
    public int activeCurriculumPhase = 0;

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
        //TODO refactor!
        if (curriculumActive && brainToTrain != null && !debugModeWithPlayerBrain)
        {
            GameObject fulfillObjectiveBrainGameObject = GameObject.Find("FulfillObjectiveBrain");
            Brain brainInstanceFulfillObjective = null;
            if (fulfillObjectiveBrainGameObject != null)
                brainInstanceFulfillObjective = fulfillObjectiveBrainGameObject.GetComponent<Brain>();
            if (brainInstanceFulfillObjective != null)
                brainInstanceFulfillObjective.brainType = BrainType.External;

            GameObject attackEnemiesBrainGameObject = GameObject.Find("AttackEnemiesBrain");
            Brain brainInstanceAttackEnemies = null;
            if (attackEnemiesBrainGameObject != null)
                brainInstanceAttackEnemies = attackEnemiesBrainGameObject.GetComponent<Brain>();
            if (brainInstanceAttackEnemies != null)
                brainInstanceAttackEnemies.brainType = BrainType.External;
        }


        gameControllerInstance = FindObjectOfType<GameController>();
        Debug.Log("gameControllerInstance" + gameControllerInstance);

        gameControllerInstance.initializeGame();

        goapActionControllerInstance = FindObjectOfType<GOAPActionController>();

        /*
        GameObject teamMLControllerGameObject = GameObject.Find("TeamMLController");
        teamMLController = teamMLControllerGameObject.GetComponent<TeamController>();
        teamMLMonsterList = teamMLController.teamMonsterList;
        */

        GameObject teamGOAPControllerGameObject = GameObject.Find("TeamGOAPController");
        teamGOAPController = teamGOAPControllerGameObject.GetComponent<TeamController>();
        teamGOAPMonsterList = teamGOAPController.teamMonsterList;

        if (curriculumActive)
        {

            Debug.Log("BrainToTrain: " + brainToTrain);
            if (brainToTrain != null)
            {
                fulfillObjectivesCurriculum = brainToTrain.name.Equals("FulfillObjectiveBrain");
                attackEnemiesCurriculum = brainToTrain.name.Equals("AttackEnemiesBrain");
            }

            if (fulfillObjectivesCurriculum)
            {
                activeCurriculumPhase = (int)resetParameters["phase_objectives_training"];
            }
            else if (attackEnemiesCurriculum)
            {
                activeCurriculumPhase = (int)resetParameters["phase_attack_training"];
            }

            /*
                        foreach (Monster monsterEntity in teamMLMonsterList)
                        {
                            Debug.Log("!monsterEntity.deactivatedAtStart " + !monsterEntity.deactivatedAtStart + " monsterEntity " + monsterEntity);
                            if (!monsterEntity.deactivatedAtStart)
                            {
                                MLMonsterAgent mlAgent = monsterEntity.GetComponent<MLMonsterAgent>();
                                if (brainToTrain != null)
                                {
                                    Debug.Log("mlAgent " + mlAgent + " gets Brain " + brainToTrain);
                                    mlAgent.GiveBrain(brainToTrain);
                                }
                            }
                        }
             */
            if (attackEnemiesCurriculum)
            {
                ActivateAttackEnemiesCurriculumPhase(activeCurriculumPhase);
                //TODO make sure all goapagents are active!
            }
        }

    }

    public override void AcademyReset()
    {
        gameControllerInstance.ResetGame();

        if (curriculumActive)
        {
            if (fulfillObjectivesCurriculum)
            {
                activeCurriculumPhase = (int)resetParameters["phase_objectives_training"];
            }
            else if (attackEnemiesCurriculum)
            {
                activeCurriculumPhase = (int)resetParameters["phase_attack_training"];
            }

            if (attackEnemiesCurriculum && !activatedTrainingPhasesForGoap.Contains(activeCurriculumPhase))
            {
                ActivateAttackEnemiesCurriculumPhase(activeCurriculumPhase);
                activatedTrainingPhasesForGoap.Add(activeCurriculumPhase);
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
            case (int)MonsterTrainerAcademy.TrainingPhasesAttackEnemies.phaseMovingEnemiesHalfSpeed:
                goapActionControllerInstance.setGoapActionsForCurriculumLearningPhaseMovingEnemiesHalfSpeed(teamGOAPMonsterList);
                break;
            case (int)MonsterTrainerAcademy.TrainingPhasesAttackEnemies.phaseMovingEnemiesFullSpeed:
                goapActionControllerInstance.setGoapActionsForCurriculumLearningPhaseMovingEnemiesFullSpeed(teamGOAPMonsterList);
                break;
            case (int)MonsterTrainerAcademy.TrainingPhasesAttackEnemies.phaseAttackingEnemies:
                goapActionControllerInstance.setGoapActionsForCurriculumLearningPhaseAttackingEnemies(teamGOAPMonsterList);
                break;
            default:
                break;
        }
    }
}
