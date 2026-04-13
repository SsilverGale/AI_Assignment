using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using System.Collections;
using Unity.VisualScripting;
using System.Threading.Tasks;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "lookAround", story: "[Self] looks around", category: "Action", id: "10584241bd115235c259d3aaddd6ab7e")]
public partial class LookAroundAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<bool> PlayerSeen;

    private bool hasLookedAround = false;

    protected override Status OnStart()
    {
        hasLookedAround = false;
        LookAroundAsync();
        //System.Collections.StartCoroutine(LookAroundCoroutine());
        return Status.Running;

    }

    protected override Status OnUpdate()
    { 

        if (PlayerSeen)
            return Status.Success;
        else if (hasLookedAround)
            return Status.Success;
        else 
            return Status.Running; 
    }

    private async void LookAroundAsync()
    {
        for (int i = 0;i < 10;i++)
        {
            if (!PlayerSeen.Value){
            Self.Value.transform.Rotate(0,10,0);
            await Task.Delay(50);}
            else break;
        }
        for (int i = 0;i < 20;i++)
        {
            if (!PlayerSeen.Value){
            Self.Value.transform.Rotate(0,-10,0);
            await Task.Delay(50);}
            else break;
        }
        for (int i = 0;i < 20;i++)
        {
            if (!PlayerSeen.Value){
            Self.Value.transform.Rotate(0,10,0);
            await Task.Delay(50);}
            else break;
        }
        hasLookedAround = true;
    }

    protected override void OnEnd()
    {
    }
}

