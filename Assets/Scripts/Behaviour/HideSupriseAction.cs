using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "hideSuprise", story: "hide [suprise]", category: "Action", id: "a5ec97d4aa203a9597b4b8aa62a7aa9e")]
public partial class HideSupriseAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Suprise;

    protected override Status OnStart()
    {
        Suprise.Value.SetActive(false);
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

