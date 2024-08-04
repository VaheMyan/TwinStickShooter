using System;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class PlayerAnimSystem : ComponentSystem
{
    private EntityQuery _query;
    protected override void OnCreate()
    {
        _query = GetEntityQuery(ComponentType.ReadOnly<Animator>(), ComponentType.ReadOnly<InputData>(), ComponentType.ReadOnly<ApplyPlayerAnimDirection>());
    }

    protected override void OnUpdate()
    {
        Entities.With(_query).ForEach((Entity entity, ref InputData move, Animator animator, UserInputData inputData, PlayerHealth playerHealth, ApplyPlayerAnimDirection playerAnimDirection) =>
        {
            if (animator != null && inputData != null && playerHealth != null)
            {
                animator.SetBool(inputData.moveAnimHash, Math.Abs(move.Move.x) > 0.05f || Math.Abs(move.Move.y) > 0.05f); // Walk anim
                animator.SetBool(inputData.shootAnimHash, Math.Abs(move.Shoot) > 0f); // Attack aim



                float x = 0;
                float y = 0;
                if (move.Move.x < 0)
                {
                    x = -move.Move.x;
                }
                else
                {
                    x = move.Move.x;
                }
                if (move.Move.y < 0)
                {
                    y = -move.Move.y;
                }
                else
                {
                    y = move.Move.y;
                }
                float result = Mathf.Max(x, y);

                if (inputData.moveAnimSpeedHash == String.Empty) return;
                animator.SetFloat(inputData.moveAnimSpeedHash, 1.5f * result);
            }
            else
            {
                Debug.LogError("Problem");
            }

            playerAnimDirection.Execute(move.Move.x, move.Move.y);
        });
    }
}
