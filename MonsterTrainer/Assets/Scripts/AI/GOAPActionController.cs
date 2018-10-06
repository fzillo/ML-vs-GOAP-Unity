using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOAPActionController : MonoBehaviour
{
    public bool attackEnemyActionEnabled = true;
    public bool conquerAreaActionEnabled = true;
    public bool convoyBombActionEnabled = true;
    public bool defendAreaActionEnabled = true;
    public bool deliverBombActionEnabled = true;
    public bool patrolRandomlyActionEnabled = false;
    public bool pickUpBombActionEnabled = true;

    public void deactivateGoapForMonsters(List<Monster> monsterList)
    {
        foreach (Monster monsterEntity in monsterList)
        {
            GoapAgent agent = monsterEntity.GetComponent<GoapAgent>();
            if (agent != null)
            {
                agent.enabled = false;
            }
        }
    }

    public void activateGoapForMonsters(List<Monster> monsterList)
    {
        foreach (Monster monsterEntity in monsterList)
        {
            GoapAgent agent = monsterEntity.GetComponent<GoapAgent>();
            if (agent != null)
            {
                agent.enabled = true;
            }
        }
    }


    public void setGoapActionsForCurriculumLearningPhaseStaticEnemies(List<Monster> monsterList)
    {
        foreach (Monster monsterEntity in monsterList)
        {
            GoapAgent agent = monsterEntity.GetComponent<GoapAgent>();
            if (agent != null)
            {
                agent.enabled = false;
            }
        }
    }

    public void setGoapActionsForCurriculumLearningPhaseMovingEnemies(List<Monster> monsterList)
    {
        foreach (Monster monsterEntity in monsterList)
        {
            GoapAgent agent = monsterEntity.GetComponent<GoapAgent>();
            if (agent != null)
            {
                agent.enabled = true;
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
        foreach (Monster monsterEntity in monsterList)
        {

            GoapAgent agent = monsterEntity.GetComponent<GoapAgent>();
            if (agent != null)
            {
                agent.enabled = true;
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
