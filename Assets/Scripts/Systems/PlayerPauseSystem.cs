using UnityEngine;
using Unity.Entities;

public class PlayerPauseSystem : ComponentSystem
{
    private bool canCall = true;
    private EntityQuery _pauseQuery;

    protected override void OnCreate()
    {
        _pauseQuery = GetEntityQuery(ComponentType.ReadOnly<ApplyReload>(), ComponentType.ReadOnly<Menu>());
    }
    protected override void OnUpdate()
    {
        Debug.Log("aaaaaa");
        Entities.With(_pauseQuery).ForEach((Entity entity, ref InputData input, Menu menu) =>
        {
            if (input.Pause == 1f)
            {
                if (canCall == false) return;
                menu.Pause();
                canCall = false;
            }
            else
            {
                canCall = true;
            }
        });
    }


}
