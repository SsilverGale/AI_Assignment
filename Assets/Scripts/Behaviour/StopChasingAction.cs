using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "StopChasing", story: "[Self] stop", category: "Action", id: "12a72c2f762de0f43614684346fe9199")]
public partial class StopChasingAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<Mover> Mover;

    protected override Status OnStart()
    {
        Mover.Value.SetDestination(Self.Value.transform.position);
        return Status.Running;
        
    }

    protected override Status OnUpdate()
    {
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

