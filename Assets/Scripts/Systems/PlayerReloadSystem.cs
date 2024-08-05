using UnityEngine;
using Unity.Entities;

public class PlayerReloadSystem : ComponentSystem
{
    private EntityQuery _reloadQuery;

    protected override void OnCreate()
    {
        _reloadQuery = GetEntityQuery(ComponentType.ReadOnly<ApplyReload>(), ComponentType.ReadOnly<UserInputData>());
    }
    protected override void OnUpdate()
    {
        Entities.With(_reloadQuery).ForEach((Entity entity, ref InputData input, ApplyReload applyReload, UserInputData inputData) =>
        {
            if (inputData.ReloadAction != null && input.Reload == 1f && inputData.ReloadAction is IAbility ability)
            {
                ability.Execute();
            }
        });
    }


}
