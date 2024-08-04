using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using System;

public class ZombieAnimSystem : ComponentSystem
{
    private EntityQuery _query;
    protected override void OnCreate()
    {
        _query = GetEntityQuery(ComponentType.ReadOnly<Animator>(), ComponentType.ReadOnly<ApplyZombieAnim>());
    }

    protected override void OnUpdate()
    {
        Entities.With(_query).ForEach((Entity entity, Animator animator, ApplyZombieAnim zombieAnim) =>
        {
            if (animator != null)
            {
                //
            }
            else
            {
                Debug.LogError("Problem");
            }
        });
    }
}
