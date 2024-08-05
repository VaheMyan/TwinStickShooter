using UnityEngine;
using Unity.Entities;

public class PlayerAmmoSystem : ComponentSystem
{
    private EntityQuery _ammoQuery;

    protected override void OnCreate()
    {
        _ammoQuery = GetEntityQuery(ComponentType.ReadOnly<ApplyReload>(), ComponentType.ReadOnly<UserInputData>(), ComponentType.ReadOnly<ApplyPlayerAmmo>());
    }
    protected override void OnUpdate()
    {
        Entities.With(_ammoQuery).ForEach((Entity entity, ref InputData input, ApplyReload applyReload, UserInputData inputData, ApplyPlayerAmmo playerAmmo) =>
        {
            if (inputData.AmmoAction != null)
            {
                //playerAmmo.Execute();
            }
        });
    }


}
