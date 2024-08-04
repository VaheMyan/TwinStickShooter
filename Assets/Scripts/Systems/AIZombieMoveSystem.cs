using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using static AIBehaviourManager;

public class AIZombieMoveSystem : ComponentSystem
{
    private EntityQuery _behaviourQuery;

    protected override void OnCreate()
    {
        _behaviourQuery = GetEntityQuery(ComponentType.ReadOnly<AIBehaviourManager>());
    }
    protected override void OnUpdate()
    {
        Entities.With(_behaviourQuery).ForEach((Entity entity, AIBehaviourManager manager) =>
        {
            manager._activeBehaviour?.Behaviour();
        });
    }

}
