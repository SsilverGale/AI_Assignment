using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "getPlayerSeen", story: "get [PlayerSeen] from [FOV]", category: "Action", id: "736f34d4982daf10804370a69a54e441")]
public partial class GetPlayerSeenAction : Action
{
    [SerializeReference] public BlackboardVariable<bool> PlayerSeen;
    [SerializeReference] public BlackboardVariable<FieldOfView> FOV;

    protected override Status OnStart()
    {
        PlayerSeen.Value = FOV.Value.canSeePlayer;
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

