using UnityEngine;
using Unity.Entities;

public class PlayerCameraSystem : ComponentSystem
{
    private EntityQuery _cameraQuery;

    protected override void OnCreate()
    {
        _cameraQuery = GetEntityQuery(ComponentType.ReadOnly<PlayerCamera>());
    }
    protected override void OnUpdate()
    {
        Entities.With(_cameraQuery).ForEach((Entity entity, PlayerCamera camera, ref InputData inputData, ref MoveData move) =>
        {
            Debug.Log("aaaaaaaaaaaa");
            Debug.Log(move.Speed);
            //camera.Execute();
        });
    }


}