using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PickUpBombAction : GoapAction
{
    bool completed = false;
    float startTime = 0;

    public Bomb teamBomb;


    public PickUpBombAction()
    {
        addPrecondition("pickUpBombActionEnabled", true);
        addPrecondition("bombActive", true);
        addEffect("doJob", true);
        name = "PickUpBombAction";
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
        return teamBomb.isActiveAndEnabled && !teamBomb.isPickedUp;
    }

    public override bool perform(GameObject agent)
    {
        if (startTime == 0)
        {
            Debug.Log("Starting: " + name);
            startTime = Time.time;
        }

        if (target.GetComponent<Bomb>().isPickedUp)
        {
            Debug.Log("Finished: " + name);
            completed = true;
        }

        return completed;
    }


}
