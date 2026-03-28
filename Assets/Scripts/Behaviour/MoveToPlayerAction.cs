using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "MoveToPlayer", story: "[Mover] moves to [Player]", category: "Action", id: "031f368195fb1b21c19d711d17ffa354")]
public partial class MoveToPlayerAction : Action
{
    [SerializeReference] public BlackboardVariable<Mover> Mover;
    [SerializeReference] public BlackboardVariable<Player> Player;
    [SerializeReference] public BlackboardVariable<bool> PlayerSeen;


    protected override Status OnStart()
    {
        Mover.Value.SetDestination(Player.Value.transform.position);
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (!PlayerSeen)
        {
            return Status.Success;
        } else
        
        return Status.Running;

        
    }

    protected override void OnEnd()
    {
    }
}

