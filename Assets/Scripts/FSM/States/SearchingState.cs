using UnityEngine;
using Unity.Behavior;
using System.Threading.Tasks;

public class SearchingState : FSM_State
{
    private BehaviorGraphAgent agent;
    private FieldOfView FOV;
    private FiniteStateMachine FSM;
    private bool isBored;

    public SearchingState(BehaviorGraphAgent Agent, FieldOfView fov, FiniteStateMachine fsm) : base(Agent, fov, fsm)
    {
        agent = Agent;
        FOV = fov;
        FSM = fsm;
        isBored = false;
    }

    public override void Enter()
    {
        agent.enabled = true;
        isExiting = false;
        becomeBored();
    }
    public override void TransitionCheck()
    {
        if (FOV.canSeePlayer)
        {
            FSM.changeState(FSM.chasing);
        }
        if (isBored)
        {
            FSM.changeState(FSM.roaming);
        }
    }
    //After a certain amount of time, AI will get bored and go back to raoming
    private async void becomeBored()
    {
        await Task.Delay(150);
        isBored = true;
    }
}

