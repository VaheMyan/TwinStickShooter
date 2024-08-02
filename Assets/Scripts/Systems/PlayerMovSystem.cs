using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Entities;

public class PlayerMovSystem : ComponentSystem
{
    private EntityQuery _moveQuery;

    protected override void OnCreate()
    {
        _moveQuery = GetEntityQuery(ComponentType.ReadOnly<PlayerMoveComponent>()); // move


    }
    protected override void OnUpdate()
    {
        Entities.With(_moveQuery).ForEach(
            (Entity entity, PlayerMoveComponent playerMoveComponent, ref InputData inputData, ref MoveData move) =>
            {
                if (playerMoveComponent.transform != null)
                {
                    var pos = playerMoveComponent.transform?.position;
                    pos += new Vector3(inputData.Move.x * move.Speed * Time.DeltaTime, y: 0, inputData.Move.y * move.Speed * Time.DeltaTime);
                    playerMoveComponent.transform.position = (Vector3)pos;

                    if (playerMoveComponent.transform.position.y < 0)
                    {
                        playerMoveComponent.transform.position = new Vector3(playerMoveComponent.transform.position.x, 0, playerMoveComponent.transform.position.z);
                    }
                }
            });
    }


}