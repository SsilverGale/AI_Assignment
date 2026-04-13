using UnityEngine;
using Unity.Behavior;
using NUnit.Framework;


public class FSM_State
{
    //Variables
    protected BehaviorGraphAgent agent;
    protected FieldOfView FOV;
    protected bool isExiting;
    protected FiniteStateMachine FSM;

    //Constructor
    public FSM_State(BehaviorGraphAgent Agent,FieldOfView fov, FiniteStateMachine fsm)
    {
        agent = Agent;
        FOV = fov;
        FSM = fsm;
    }
    public virtual void Enter()
    {
        agent.enabled = true;
        isExiting = false;
    }
    public virtual void Exit()
    {
        agent.enabled = false;
        isExiting = true;
    }
    public virtual void Update()
    {
        TransitionCheck();
    }
    public virtual void TransitionCheck() {}

}
