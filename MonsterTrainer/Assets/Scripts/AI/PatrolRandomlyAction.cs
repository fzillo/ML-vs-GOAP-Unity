using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolRandomlyAction : GoapAction
{
    GOAPMonsterAgent thisGoapMonsterAgent;
    StartPositionGenerator startPosGenerator;

    bool completed = false;
    float startTime = 0;

    GameObject waypoint;

    public Transform waypointPrefab;

    public PatrolRandomlyAction()
    {
        addPrecondition("patrolRandomlyActionEnabled", true);
        addEffect("doJob", true);
        name = "PatrolRandomlyAction";
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
        waypoint = SpawnRandomWaypoint();

        if (thisGoapMonsterAgent == null)
        {
            thisGoapMonsterAgent = GetComponent<GOAPMonsterAgent>();
        }
        if (thisGoapMonsterAgent != null && waypoint != null)
        {
            thisGoapMonsterAgent.instantiatedWaypoints.Add(waypoint);
        }

        target = waypoint;
        return waypoint != null;
    }

    GameObject SpawnRandomWaypoint()
    {
        if (startPosGenerator == null)
        {
            startPosGenerator = FindObjectOfType<StartPositionGenerator>();
        }

        Transform newWaypointTransform = Instantiate(waypointPrefab, startPosGenerator.GeneratePositionInRandomSpawnZone(), startPosGenerator.GenerateRandomRotation());

        return newWaypointTransform.gameObject;
    }

    public override bool perform(GameObject agent)
    {
        if (startTime == 0)
        {
            Debug.Log("Starting: " + name);
            startTime = Time.time;
        }

        if (waypoint != null)
        {
            thisGoapMonsterAgent.instantiatedWaypoints.Remove(waypoint);
            Destroy(waypoint);
            waypoint = null;
            completed = true;
            Debug.Log("Finished: " + name);
        }

        return completed;
    }
}
