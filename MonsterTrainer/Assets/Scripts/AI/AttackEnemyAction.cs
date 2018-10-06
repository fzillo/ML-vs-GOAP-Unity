using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEnemyAction : GoapAction
{
    bool completed = false;
    float startTime = 0;

    public Monster[] enemies;
    Monster targetEnemy;

    public AttackEnemyAction()
    {
        addPrecondition("attackEnemyActionEnabled", true);
        addPrecondition("isReady", true);
        addEffect("doJob", true);
        name = "AttackEnemyAction";
    }

    public override void reset()
    {
        completed = false;
        startTime = 0;
    }

    public override bool isDone()
    {
        return completed;
    }

    public override bool requiresInRange()
    {
        return true;
    }

    public override bool checkProceduralPrecondition(GameObject agent)
    {

        Monster closest = null;
        float closestDist = 0;

        foreach (Monster enemy in enemies)
        {
            if (!enemy.isAlive)
            {
                continue;
            }

            if (closest == null)
            {
                closest = enemy;
                closestDist = (enemy.gameObject.transform.position - agent.transform.position).magnitude;
            }
            else
            {
                // is this one closer than the last?
                float dist = (enemy.gameObject.transform.position - agent.transform.position).magnitude;
                if (dist < closestDist)
                {
                    // we found a closer one, use it
                    closest = enemy;
                    closestDist = dist;
                }
            }
        }
        targetEnemy = closest;
        if (targetEnemy != null)
        {
            target = targetEnemy.gameObject;
        }

        return closest != null;
    }

    public override bool perform(GameObject agent)
    {
        if (startTime == 0)
        {
            Debug.Log("Starting: " + name);
            startTime = Time.time;
        }

        Debug.Log(name + " completed " + completed + " !target.GetComponent<Monster>().isAlive " + !target.GetComponent<Monster>().isAlive);

        if (!target.GetComponent<Monster>().isAlive)
        {
            Debug.Log("Finished: " + name);
            completed = true;
        }

        return completed;
    }
}
