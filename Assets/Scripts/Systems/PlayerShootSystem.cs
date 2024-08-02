using UnityEngine;
using Unity.Entities;

public class PlayerShootSystem : ComponentSystem
{
    private EntityQuery _shootQuery;

    protected override void OnCreate()
    {
        _shootQuery = GetEntityQuery(ComponentType.ReadOnly<InputData>(), ComponentType.ReadOnly<ShootData>(), ComponentType.ReadOnly<UserInputData>());
    }
    protected override void OnUpdate()
    {
        Entities.With(_shootQuery).ForEach((Entity entity, UserInputData inputData, ref InputData input) =>
        {
            if (input.Shoot > 0f && inputData.ShootAction != null && inputData.ShootAction is IAbility ability)
            {
                ability.Execute();
            }
            foreach (var abilityBullet in inputData.ApplyShootActions)
            {
                if (abilityBullet is IAbilityBullet _abilityBullet)
                {
                    _abilityBullet.Execute();
                }
            }
        });
    }


}
