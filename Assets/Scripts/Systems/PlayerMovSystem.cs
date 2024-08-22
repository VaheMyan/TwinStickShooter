using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Entities;
using Unity.Mathematics;

public class PlayerMovSystem : ComponentSystem
{
    private EntityQuery _moveQuery;

    protected override void OnCreate()
    {
        _moveQuery = GetEntityQuery(ComponentType.ReadOnly<PlayerMoveComponent>()); // move


    }
    protected override void OnUpdate()
    {
        Vector3 boundaryMin = new Vector3(-20f, 0f, -20f);
        Vector3 boundaryMax = new Vector3(20f, 0f, 20f);

        Entities.With(_moveQuery).ForEach(
            (Entity entity, PlayerMoveComponent playerMoveComponent, ref InputData inputData, ref MoveData move) =>
            {
                if (playerMoveComponent.transform != null)
                {
                    var pos = playerMoveComponent.transform?.position;
                    pos += new Vector3(inputData.Move.x * move.Speed * Time.DeltaTime, y: 0, inputData.Move.y * move.Speed * Time.DeltaTime);
                    playerMoveComponent.transform.position = (Vector3)pos;

                    playerMoveComponent.transform.position = math.clamp(playerMoveComponent.transform.position, boundaryMin, boundaryMax);

                    if (playerMoveComponent.transform.position.y < 0)
                    {
                        playerMoveComponent.transform.position = new Vector3(playerMoveComponent.transform.position.x, 0, playerMoveComponent.transform.position.z);
                    }
                }
            });
    }


}