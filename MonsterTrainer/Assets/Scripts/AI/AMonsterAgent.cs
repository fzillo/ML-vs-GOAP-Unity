using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AMonsterAgent : MonoBehaviour, IGoap
{
    NavMeshAgent agent;
    Vector3 previousDestination;
    //AreaController[] areas;

    //TODO CANCEL PLAN ON RESET!

    //TODO zentral verwalten
    public Bomb teamBomb;

    // Use this for initialization
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
    }

    /**
	 * The starting state of the Agent and the world.
	 * Supply what states are needed for actions to run.
	 */
    public HashSet<KeyValuePair<string, object>> GetWorldState()
    {
        HashSet<KeyValuePair<string, object>> worldData = new HashSet<KeyValuePair<string, object>>();

        worldData.Add(new KeyValuePair<string, object>("isReady", true));
        worldData.Add(new KeyValuePair<string, object>("bombActive", teamBomb.isActiveAndEnabled));
        worldData.Add(new KeyValuePair<string, object>("bombPickedUp", teamBomb.isPickedUp));

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
        if (!nextAction.checkProceduralPrecondition(this.gameObject))
        {
            CancelPlan();
        }


        //if we don't need to move anywhere
        if (previousDestination == nextAction.target.transform.position)
        {
            nextAction.setInRange(true);
            return true;
        }

        agent.SetDestination(nextAction.target.transform.position);

        if (agent.hasPath && agent.remainingDistance < 2)
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
        if (agent.hasPath)
        {
            Vector3 toTarget = agent.steeringTarget - this.transform.position;
            float turnAngle = Vector3.Angle(this.transform.forward, toTarget);
            agent.acceleration = turnAngle * agent.speed;
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
        this.GetComponent<GoapAgent>().CancelPlan();
    }
}
