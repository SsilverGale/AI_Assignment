using UnityEngine;
using Unity.Behavior;

public class ChasingState : FSM_State
{
    private BehaviorGraphAgent agent;
    private FieldOfView FOV;
    private FiniteStateMachine FSM;

    public ChasingState(BehaviorGraphAgent Agent, FieldOfView fov, FiniteStateMachine fsm) : base(Agent, fov, fsm)
    {
        agent = Agent;
        FOV = fov;
        FSM = fsm;
    }

    public override void TransitionCheck()
    {
        if (!FOV.canSeePlayer)
        {
            FSM.changeState(FSM.searching);
        }
    }
}

