using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "assignPlayer", story: "Assign [Player]", category: "Action", id: "b8468947ed021255f9daf2f4e5f83c32")]
public partial class AssignPlayerAction : Action
{
    [SerializeReference] public BlackboardVariable<Player> Player;

    protected override Status OnStart()
    {
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

