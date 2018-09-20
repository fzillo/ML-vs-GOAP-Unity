using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AMonsterAgent : MonoBehaviour, IGoap
{
    NavMeshAgent navAgent;
    Vector3 previousDestination; //TODO remove?
    //GoapAction previousAction;


    //List<GoapAction> goapActionList = new List<GoapAction>();
    //AreaController[] areas;

    //TODO CANCEL PLAN ON RESET!

    //TODO zentral verwalten
    public Bomb teamBomb;
    //public GameObject teamBombGO;

    // Use this for initialization
    void Start()
    {
        navAgent = this.GetComponent<NavMeshAgent>();
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

        /*
            if (teamBomb == null)
            {
                GameObject bombGo = GameObject.FindGameObjectWithTag("aBomb");
                teamBomb = bombGo.GetComponent<Bomb>();
            }
            */

        worldData.Add(new KeyValuePair<string, object>("bombActive", teamBomb.hasSpawned));
        Debug.Log("worldData bombActive " + teamBomb + " hasSpawned " + teamBomb.hasSpawned);
        worldData.Add(new KeyValuePair<string, object>("bombPickedUp", teamBomb.isPickedUp));
        Debug.Log("worldData bombPickedUp " + teamBomb.isPickedUp);

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
        //goal.Add(new KeyValuePair<string, object>("bombPickedUp", true));
        //goal.Add(new KeyValuePair<string, object>("bombDelivered", true));

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
        //TODO Wieder einbauen, allerdings ohne checkProceduralPrecondition
        /*
        GetWorldState();
        if (!nextAction.checkProceduralPrecondition(this.gameObject))
        {
            Debug.Log("ProceduralPrecondition false for " + nextAction);
            CancelPlan();
            return true;
        }
 */

        /*
        //BUG DEBUGGING
        goapActionList.Add(nextAction);

        GoapAction previousAction = null;
        if (goapActionList.Count > 1)
            previousAction = goapActionList[goapActionList.Count - 2];
        Debug.Log("previousAction " + previousAction + " nextAction " + nextAction);

        //THIS MADE PROBLEMS AFTER RESPAWN WHEN LAST ACTION WAS EQUAL NEXT ACTION
        //if we don't need to move anywhere
        if (previousDestination == nextAction.target.transform.position)
        {
            Debug.Log("previousDestination " + previousDestination + " nextAction " + nextAction + " nextAction.target.transform.position " + nextAction.target.transform.position);
            nextAction.setInRange(true);
            return true;
        }
        */

        //if (agent.isOnNavMesh)
        navAgent.SetDestination(nextAction.target.transform.position);

        if (navAgent.hasPath && navAgent.remainingDistance < 2)
        {
            nextAction.setInRange(true);
            previousDestination = nextAction.target.transform.position;
            return true;
        }
        else
            return false;

        //return false; //TODO remove
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

    //TODO DEBUGGEN!

    public void CancelPlan()
    {
        Debug.Log("CANCEL PLAN AMONSTERAGENT FOR " + this.gameObject);
        if (navAgent != null && navAgent.hasPath)
        {
            navAgent.ResetPath();
        }
        this.GetComponent<GoapAgent>().CancelPlan();
        Debug.Log("RESET PATH AMONSTERAGENT FOR " + this.gameObject);

    }
}
