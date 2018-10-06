using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DeliverBombAction : GoapAction
{


    bool completed = false;
    float startTime = 0;

    public Bomb teamBomb;

    public DeliverBombAction()
    {
        addPrecondition("deliverBombActionEnabled", true);
        addPrecondition("bombPickedUp", true);
        addEffect("doJob", true);
        name = "DeliverBombAction";
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
        return this.gameObject.Equals(teamBomb.attachedToGO) && teamBomb.isPickedUp;
    }

    public override bool perform(GameObject agent)
    {
        if (startTime == 0)
        {
            Debug.Log("Starting: " + name);
            startTime = Time.time;
        }


        if (teamBomb.isDetonated)
        {
            Debug.Log("Finished: " + name);
            completed = true;
        }

        return completed;
    }


}
