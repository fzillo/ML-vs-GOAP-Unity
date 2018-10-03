using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


public sealed class GoapAgent : MonoBehaviour
{

    private FSM stateMachine;

    private FSM.FSMState idleState; // finds something to do
    private FSM.FSMState moveToState; // moves to a target
    private FSM.FSMState performActionState; // performs an action

    private HashSet<GoapAction> availableActions;
    private Queue<GoapAction> currentActions;

    private IGoap dataProvider; // this is the implementing class that provides our world data and listens to feedback on planning

    private GoapPlanner planner;


    void Start()
    {
        stateMachine = new FSM();
        availableActions = new HashSet<GoapAction>();
        currentActions = new Queue<GoapAction>();
        planner = new GoapPlanner();
        FindDataProvider();
        CreateIdleState();
        CreateMoveToState();
        CreatePerformActionState();
        stateMachine.pushState(idleState);
        LoadActions();
    }


    void Update()
    {
        stateMachine.Update(this.gameObject);
    }


    public void AddAction(GoapAction a)
    {
        availableActions.Add(a);
    }

    public GoapAction GetAction(Type action)
    {
        foreach (GoapAction g in availableActions)
        {
            if (g.GetType().Equals(action))
                return g;
        }
        return null;
    }

    public void RemoveAction(GoapAction action)
    {
        availableActions.Remove(action);
    }

    private bool HasActionPlan()
    {
        return currentActions.Count > 0;
    }

    private void CreateIdleState()
    {
        idleState = (fsm, gameObj) =>
        {
            // GOAP planning

            // get the world state and the goal we want to plan for
            HashSet<KeyValuePair<string, object>> worldState = dataProvider.GetWorldState();
            HashSet<KeyValuePair<string, object>> goal = dataProvider.CreateGoalState();

            // Plan
            Queue<GoapAction> plan = planner.plan(gameObject, availableActions, worldState, goal);
            if (plan != null)
            {
                // we have a plan, hooray!
                currentActions = plan;
                dataProvider.PlanFound(goal, plan);

                fsm.popState(); // move to PerformAction state
                fsm.pushState(performActionState);

            }
            else
            {
                // ugh, we couldn't get a plan
                Debug.Log("Failed Plan: " + goal);
                dataProvider.PlanFailed(goal);
                fsm.popState(); // move back to IdleAction state
                fsm.pushState(idleState);
            }

        };
    }

    private void CreateMoveToState()
    {
        moveToState = (fsm, gameObj) =>
        {
            // move the game object

            GoapAction action = currentActions.Peek();
            if (action.requiresInRange() && action.target == null)
            {
                Debug.Log("Fatal error: Action requires a target but has none. Planning failed. You did not assign the target in your Action.checkProceduralPrecondition()");
                fsm.popState(); // move
                fsm.popState(); // perform
                fsm.pushState(idleState);
                return;
            }

            // get the agent to move itself
            //Debug.Log("Move to do: " + action.name);
            if (dataProvider.MoveAgent(action))
            {
                fsm.popState();
            }
        };
    }

    private void CreatePerformActionState()
    {

        performActionState = (fsm, gameObj) =>
        {
            // perform the action

            if (!HasActionPlan())
            {
                // no actions to perform
                Debug.Log("<color=red>Done actions</color>");
                fsm.popState();
                fsm.pushState(idleState);
                dataProvider.ActionsFinished();
                return;
            }

            GoapAction action = currentActions.Peek();
            if (action.isDone())
            {
                // the action is done. Remove it so we can perform the next one
                currentActions.Dequeue();
            }

            if (HasActionPlan())
            {
                // perform the next action
                action = currentActions.Peek();
                bool inRange = action.requiresInRange() ? action.isInRange() : true;

                if (inRange)
                {
                    // we are in range, so perform the action
                    bool success = action.perform(gameObj);

                    if (!success)
                    {
                        // action failed, we need to plan again
                        fsm.popState();
                        fsm.pushState(idleState);
                        dataProvider.PlanAborted(action);
                    }
                }
                else
                {
                    // we need to move there first
                    // push moveTo state
                    fsm.pushState(moveToState);
                }

            }
            else
            {
                // no actions left, move to Plan state
                fsm.popState();
                fsm.pushState(idleState);
                dataProvider.ActionsFinished();
            }

        };
    }

    private void FindDataProvider()
    {
        foreach (Component comp in gameObject.GetComponents(typeof(Component)))
        {
            if (typeof(IGoap).IsAssignableFrom(comp.GetType()))
            {
                dataProvider = (IGoap)comp;
                return;
            }
        }
    }

    private void LoadActions()
    {
        GoapAction[] actions = gameObject.GetComponents<GoapAction>();
        foreach (GoapAction a in actions)
        {
            availableActions.Add(a);
        }
        Debug.Log("Found actions: " + actions);
    }


    //ADDED BY FZ
    public void CancelPlan()
    {
        while (!stateMachine.IsStackEmpty())
        {
            stateMachine.popState();
        }
        stateMachine.pushState(idleState);
        Debug.Log("PLAN CANCELLED!");
        dataProvider.ActionsFinished();
    }
}
