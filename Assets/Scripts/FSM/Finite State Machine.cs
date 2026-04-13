using UnityEngine;
using Unity.Behavior;
using Unity.VisualScripting;


public class FiniteStateMachine : MonoBehaviour
{
    //BehaviourTreeAgents for toggling different behaviour trees
    [SerializeField] private BehaviorGraphAgent roamingTree;
    [SerializeField] private BehaviorGraphAgent chasingTree;
    [SerializeField] private BehaviorGraphAgent searchingTree;
    //Feild of view sript to check if enemy can see player
    [SerializeField] private FieldOfView fov;
    //All states
    public RoamingState roaming;
    public ChasingState chasing;
    public SearchingState searching;


    FSM_State currentState;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Assigning all the states
        roaming = new RoamingState(roamingTree,fov,this);
        chasing = new ChasingState(chasingTree,fov,this);
        searching = new SearchingState(searchingTree,fov,this);
        
        //Default state is roaming
        currentState = roaming;
    }

    // Update is called once per frame
    void Update()
    {
        //Makes sure that current state keeps running update funtion, mostly used for transition detection
        currentState.Update();
    }

    //Changes active state. This is called by the states themself when they have their tranisitons
    public void changeState(FSM_State newState)
    {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }
}

