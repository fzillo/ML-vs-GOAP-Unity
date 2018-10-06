using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GOAPMonsterAgent : MonoBehaviour, IGoap
{
    NavMeshAgent navAgent;

    GOAPActionController goapActionControllerInstance;
    //Vector3 previousDestination;

    [HideInInspector]
    public List<GameObject> instantiatedWaypoints = new List<GameObject>();

    public Bomb teamBomb;

    // Use this for initialization
    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();

        goapActionControllerInstance = FindObjectOfType<GOAPActionController>();
    }

    /**
	 * The starting state of the Agent and the world.
	 * Supply what states are needed for actions to run.
	 */
    public HashSet<KeyValuePair<string, object>> GetWorldState()
    {
        HashSet<KeyValuePair<string, object>> worldData = new HashSet<KeyValuePair<string, object>>();

        worldData.Add(new KeyValuePair<string, object>("isReady", true));
        Debug.Log("worldData isReady " + true);
        worldData.Add(new KeyValuePair<string, object>("bombActive", teamBomb.hasSpawned));
        Debug.Log("worldData bombActive " + teamBomb + " hasSpawned " + teamBomb.hasSpawned);
        worldData.Add(new KeyValuePair<string, object>("bombPickedUp", teamBomb.isPickedUp));
        Debug.Log("worldData bombPickedUp " + teamBomb.isPickedUp);

        //Check settings from GOAPActionController
        worldData.Add(new KeyValuePair<string, object>("attackEnemyActionEnabled", goapActionControllerInstance.attackEnemyActionEnabled));
        Debug.Log("worldData attackEnemyActionEnabled" + goapActionControllerInstance.attackEnemyActionEnabled);
        worldData.Add(new KeyValuePair<string, object>("conquerAreaActionEnabled", goapActionControllerInstance.conquerAreaActionEnabled));
        Debug.Log("worldData conquerAreaActionEnabled" + goapActionControllerInstance.conquerAreaActionEnabled);
        worldData.Add(new KeyValuePair<string, object>("convoyBombActionEnabled", goapActionControllerInstance.convoyBombActionEnabled));
        Debug.Log("worldData convoyBombActionEnabled" + goapActionControllerInstance.convoyBombActionEnabled);
        worldData.Add(new KeyValuePair<string, object>("defendAreaActionEnabled", goapActionControllerInstance.defendAreaActionEnabled));
        Debug.Log("worldData defendAreaActionEnabled" + goapActionControllerInstance.defendAreaActionEnabled);
        worldData.Add(new KeyValuePair<string, object>("deliverBombActionEnabled", goapActionControllerInstance.deliverBombActionEnabled));
        Debug.Log("worldData deliverBombActionEnabled" + goapActionControllerInstance.deliverBombActionEnabled);
        worldData.Add(new KeyValuePair<string, object>("patrolRandomlyActionEnabled", goapActionControllerInstance.patrolRandomlyActionEnabled));
        Debug.Log("worldData patrolRandomlyActionEnabled" + goapActionControllerInstance.patrolRandomlyActionEnabled);
        worldData.Add(new KeyValuePair<string, object>("pickUpBombActionEnabled", goapActionControllerInstance.pickUpBombActionEnabled));
        Debug.Log("worldData pickUpBombActionEnabled" + goapActionControllerInstance.pickUpBombActionEnabled);

        return worldData;
    }

    /**
	 * Give the planner a new goal so it can figure out 
	 * the actions needed to fulfill it.
	 */
    public HashSet<KeyValuePair<string, object>> CreateGoalState()
    {
        HashSet<KeyValuePair<string, object>> goal = new HashSet<KeyValuePair<string, object>>();

        goal.Add(new KeyValuePair<string, object>("doJob", true));

        return goal;
    }

    /**
      * Called during Update. Move the agent towards the target in order
      * for the next action to be able to perform.
      * Return true if the Agent is at the target and the next action can perform.
      * False if it is not there yet.
      */
    public bool MoveAgent(GoapAction nextAction)
    {

        navAgent.SetDestination(nextAction.target.transform.position);

        if (navAgent.hasPath && navAgent.remainingDistance < 2)
        {
            nextAction.setInRange(true);
            //previousDestination = nextAction.target.transform.position;
            return true;
        }
        else
            return false;
    }


    // Update is called once per frame
    void Update()
    {
        // cosmetic
        if (navAgent.hasPath)
        {
            Vector3 toTarget = navAgent.steeringTarget - this.transform.position;
            float turnAngle = Vector3.Angle(this.transform.forward, toTarget);
            navAgent.acceleration = turnAngle * navAgent.speed;
        }

    }

    /**
	 * No sequence of actions could be found for the supplied goal.
	 * You will need to try another goal
	 */
    public void PlanFailed(HashSet<KeyValuePair<string, object>> failedGoal)
    {

    }

    /**
	 * A plan was found for the supplied goal.
	 * These are the actions the Agent will perform, in order.
	 */
    public void PlanFound(HashSet<KeyValuePair<string, object>> goal, Queue<GoapAction> actions)
    {

    }

    /**
	 * All actions are complete and the goal was reached. Hooray!
	 */
    public void ActionsFinished()
    {

    }

    /**
	 * One of the actions caused the plan to abort.
	 * That action is returned.
	 */
    public void PlanAborted(GoapAction aborter)
    {

    }

    public void CancelPlan()
    {
        Debug.Log("CANCEL PLAN FOR " + this.gameObject);
        if (navAgent != null && navAgent.hasPath)
        {
            navAgent.ResetPath();
        }

        foreach (GameObject waypoint in instantiatedWaypoints)
        {
            Destroy(waypoint);
        }
        instantiatedWaypoints = new List<GameObject>();

        this.GetComponent<GoapAgent>().CancelPlan();
        Debug.Log("RESET PATH FOR " + this.gameObject);
    }
}
