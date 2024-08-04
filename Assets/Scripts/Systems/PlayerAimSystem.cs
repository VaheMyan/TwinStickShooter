using UnityEngine;
using Unity.Entities;

public class PlayerAimSystem : ComponentSystem
{
    private EntityQuery _aimQuery;

    protected override void OnCreate()
    {
        _aimQuery = GetEntityQuery(ComponentType.ReadOnly<AimAbility>(), ComponentType.ReadOnly<UserInputData>());
    }
    protected override void OnUpdate()
    {
        Entities.With(_aimQuery).ForEach((Entity entity, ref AimData aimData, AimAbility aimAbility, UserInputData inputData) =>
        {
            if (inputData.AimAction != null && inputData.AimAction is IAbility ability)
            {
                aimAbility.mousePosition = Input.mousePosition;
                aimAbility.crosshairs.transform.position = new Vector2(aimAbility.mousePosition.x, aimAbility.mousePosition.y);

                ability.Execute();
            }
            foreach (var billboard in inputData.BillboardActions)
            {
                if (billboard != null && billboard is IUpdate update)
                {
                    update.PerUpdate();
                }
            }
        });
    }


}
