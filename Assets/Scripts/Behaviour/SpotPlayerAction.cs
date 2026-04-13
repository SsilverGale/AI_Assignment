using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using System.Threading.Tasks;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "SpotPlayer", story: "[Self] looks at [Player] toggles [Suprise]", category: "Action", id: "ba3cc24f7f24c902de2053f277768450")]
public partial class SpotPlayerAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<GameObject> Player;
    [SerializeReference] public BlackboardVariable<GameObject> Suprise;
    [SerializeReference] public BlackboardVariable<bool> PlayerSeen;
    bool complete =false;

    protected override Status OnStart()
    {
        Suprise.Value.SetActive(true);
        complete = false;
        wait();
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        Self.Value.transform.LookAt(Player.Value.transform.position);
        
        if(!PlayerSeen)
        Suprise.Value.SetActive(false);
        else
        Suprise.Value.SetActive(true);

        if(complete)
        return Status.Success;
        else
        return Status.Running;

    }

    protected override void OnEnd()
    {
        Suprise.Value.SetActive(false);
    }
    private async void wait()
    {
        await Task.Delay(1000);
        complete = true;
    }
}

