using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using static AIBehaviourManager;

public class ZombieArrowSystem : ComponentSystem
{
    private EntityQuery _arrowQuery;

    protected override void OnCreate()
    {
        _arrowQuery = GetEntityQuery(ComponentType.ReadOnly<ApplyZombieArrow>());
    }
    protected override void OnUpdate()
    {
        Entities.With(_arrowQuery).ForEach((Entity entity, ZombieArrowAbility zombieArrow) =>
        {
            foreach (var abilityArrow in zombieArrow.ApplyArrowActions)
            {
                if (abilityArrow is IArrow _abilityArrow && abilityArrow.gameObject.activeInHierarchy)
                {
                    //_abilityArrow.ArrowMove();
                }
            }
        });
    }

}
