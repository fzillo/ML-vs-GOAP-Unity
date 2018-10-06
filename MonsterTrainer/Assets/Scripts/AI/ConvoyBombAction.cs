using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvoyBombAction : GoapAction
{
    bool completed = false;
    float startTime = 0;

    public ConvoyBombAction()
    {
        addPrecondition("convoyBombActionEnabled", true);
        addPrecondition("bombPickedUp", true);
        addEffect("doJob", true);
        name = "ConvoyBombAction";
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
        return true;
    }

    public override bool perform(GameObject agent)
    {
        if (startTime == 0)
        {
            Debug.Log("Starting: " + name);
            startTime = Time.time;
        }

        if (target.GetComponent<Bomb>().isDetonated || !target.GetComponent<Bomb>().isPickedUp)
        {
            Debug.Log("Finished: " + name);
            completed = true;
        }

        return completed;
    }
}
