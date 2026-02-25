using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "CapturePlayer", story: "[Self] captures [player]", category: "Action", id: "b65e28fd4c5d300f5e9a7b4ffe4bfc03")]
public partial class CapturePlayerAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<Player> Player;

    protected override Status OnStart()
    {
        Player.Value.capturePlayer();
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

