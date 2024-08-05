using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using static AIBehaviourManager;

public class IncreasingComplexitySystem : ComponentSystem
{
    private EntityQuery _complexityQuery;

    protected override void OnCreate()
    {
        _complexityQuery = GetEntityQuery(ComponentType.ReadOnly<AIBehaviourManager>());
    }
    protected override void OnUpdate()
    {
        Entities.With(_complexityQuery).ForEach((Entity entity, AIBehaviourManager manager) =>
        {

        });
    }

}