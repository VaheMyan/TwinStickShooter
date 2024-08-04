using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using static AIBehaviourManager;

public class AIEvaluateSystem : ComponentSystem
{

    private EntityQuery _evaluteQuery;

    protected override void OnCreate()
    {
        _evaluteQuery = GetEntityQuery(ComponentType.ReadOnly<AIBehaviourManager>());
    }
    protected override void OnUpdate()
    {
        Entities.With(_evaluteQuery).ForEach((Entity entity, AIBehaviourManager manager) =>
        {
            float hightScore = float.MinValue;

            manager._activeBehaviour = null;

            foreach (var behaviour in manager.behaviours)
            {
                if (behaviour is IBehaviour ai)
                {
                    var currentScore = ai.Evaluate();
                    if (currentScore > hightScore)
                    {
                        hightScore = currentScore;
                        manager._activeBehaviour = ai;
                    }
                }
            }
        });
    }

}