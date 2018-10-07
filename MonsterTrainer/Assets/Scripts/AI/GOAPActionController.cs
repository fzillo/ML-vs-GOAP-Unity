using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GOAPActionController : MonoBehaviour
{
    public bool attackEnemyActionEnabled = true;
    public bool conquerAreaActionEnabled = true;
    public bool convoyBombActionEnabled = true;
    public bool defendAreaActionEnabled = true;
    public bool deliverBombActionEnabled = true;
    public bool patrolRandomlyActionEnabled = false;
    public bool pickUpBombActionEnabled = true;

    float navMeshAgentSpeedInitially = -1f;
    int maximumattackingGoapAgents = 1;

    public void deactivateGoapForMonsters(List<Monster> monsterList)
    {
        foreach (Monster monsterEntity in monsterList)
        {
            GoapAgent goapAgent = monsterEntity.GetComponent<GoapAgent>();
            if (goapAgent != null)
            {
                goapAgent.enabled = false;
            }
        }
    }

    public void activateGoapForMonsters(List<Monster> monsterList)
    {
        foreach (Monster monsterEntity in monsterList)
        {
            GoapAgent goapAgent = monsterEntity.GetComponent<GoapAgent>();
            if (goapAgent != null)
            {
                goapAgent.enabled = true;
            }
        }
    }


    public void setGoapActionsForCurriculumLearningPhaseStaticEnemies(List<Monster> monsterList)
    {
        foreach (Monster monsterEntity in monsterList)
        {
            GoapAgent goapAgent = monsterEntity.GetComponent<GoapAgent>();
            if (goapAgent != null)
            {
                goapAgent.enabled = false;
            }
        }
    }

    public void setGoapActionsForCurriculumLearningPhaseMovingEnemiesHalfSpeed(List<Monster> monsterList)
    {
        foreach (Monster monsterEntity in monsterList)
        {
            NavMeshAgent navAgent = monsterEntity.GetComponent<NavMeshAgent>();
            if (navAgent != null)
            {
                if (navMeshAgentSpeedInitially == -1f)
                {
                    navMeshAgentSpeedInitially = navAgent.speed;
                }
                navAgent.speed = navMeshAgentSpeedInitially / 2f;
            }

            GoapAgent goapAgent = monsterEntity.GetComponent<GoapAgent>();
            if (goapAgent != null)
            {
                goapAgent.enabled = true;
            }
            attackEnemyActionEnabled = false;
            conquerAreaActionEnabled = false;
            convoyBombActionEnabled = false;
            defendAreaActionEnabled = false;
            deliverBombActionEnabled = false;
            patrolRandomlyActionEnabled = true;
            pickUpBombActionEnabled = false;

        }
    }

    public void setGoapActionsForCurriculumLearningPhaseMovingEnemiesFullSpeed(List<Monster> monsterList)
    {
        foreach (Monster monsterEntity in monsterList)
        {
            NavMeshAgent navAgent = monsterEntity.GetComponent<NavMeshAgent>();
            if (navAgent != null)
            {
                if (navMeshAgentSpeedInitially != -1f)
                {
                    navAgent.speed = navMeshAgentSpeedInitially;
                }
            }

            GoapAgent goapAgent = monsterEntity.GetComponent<GoapAgent>();
            if (goapAgent != null)
            {
                goapAgent.enabled = true;
            }
            attackEnemyActionEnabled = false;
            conquerAreaActionEnabled = false;
            convoyBombActionEnabled = false;
            defendAreaActionEnabled = false;
            deliverBombActionEnabled = false;
            patrolRandomlyActionEnabled = true;
            pickUpBombActionEnabled = false;

        }
    }

    public void setGoapActionsForCurriculumLearningPhaseAttackingEnemies(List<Monster> monsterList)
    {
        int activatedAgents = 0;
        foreach (Monster monsterEntity in monsterList)
        {
            if (activatedAgents >= maximumattackingGoapAgents)
            {
                GameObject monsterGO = monsterEntity.gameObject;
                monsterGO.SetActive(false);
            }

            GoapAgent goapAgent = monsterEntity.GetComponent<GoapAgent>();
            if (goapAgent != null)
            {
                goapAgent.enabled = true;
                activatedAgents++;
            }

            attackEnemyActionEnabled = true;
            conquerAreaActionEnabled = false;
            convoyBombActionEnabled = false;
            defendAreaActionEnabled = false;
            deliverBombActionEnabled = false;
            patrolRandomlyActionEnabled = false;
            pickUpBombActionEnabled = false;
        }
    }
}
