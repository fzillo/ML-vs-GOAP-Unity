using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ConquerAreaAction : GoapAction
{
    bool completed = false;
    float startTime = 0;

    AreaController[] areas;
    private AreaController targetArea;

    public ConquerAreaAction()
    {
        addPrecondition("conquerAreaActionEnabled", true);
        addPrecondition("isReady", true);
        addPrecondition("bombActive", false);
        addEffect("doJob", true);
        name = "ConquerAreaAction";
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
        //chose closest Area
        if (areas == null)
        {
            areas = FindObjectsOfType<AreaController>();
        }
        AreaController closest = null;
        float closestDist = 0;

        foreach (AreaController area in areas)
        {
            if (area.conqueredByTeamGOAP)
            {
                continue;
            }

            if (closest == null)
            {
                closest = area;
                closestDist = (area.gameObject.transform.position - agent.transform.position).magnitude;
            }
            else
            {
                // is this one closer than the last?
                float dist = (area.gameObject.transform.position - agent.transform.position).magnitude;
                if (dist < closestDist)
                {
                    // we found a closer one, use it
                    closest = area;
                    closestDist = dist;
                }
            }
        }
        targetArea = closest;
        if (targetArea != null)
        {
            target = targetArea.gameObject;
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

        if (target.GetComponent<AreaController>().conqueredByTeamGOAP)
        {
            Debug.Log("Finished: " + name);
            completed = true;
        }

        return completed;
    }


}
