using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class UserInputData : MonoBehaviour, IConvertGameObjectToEntity
{
    public List<MonoBehaviour> ApplyShootActions = new List<MonoBehaviour>();
    public MonoBehaviour ShootAction;
    public MonoBehaviour RunAction;
    public MonoBehaviour ChangeMaterialAction;
    public MonoBehaviour AimAction;

    public List<MonoBehaviour> BillboardActions = new List<MonoBehaviour>();
    private Billboard[] billboards;

    public float speed;
    public float runSpeed;
    public string moveAnimHash;
    public string moveAnimSpeedHash;
    public string shootAnimHash;
    public string getHitAnimHash;
    public string dieAnimHash;

    [HideInInspector] public bool isTestRun;

    private void Start()
    {
        billboards = GameObject.FindObjectsOfType<Billboard>();
        foreach (var billboard in billboards)
        {
            BillboardActions.Add(billboard);
        }
    }

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponentData(entity, new InputData());

        dstManager.AddComponentData(entity, new MoveData
        {
            Speed = speed / 1000

        });

        if (ShootAction != null && ShootAction is IAbility)
        {
            dstManager.AddComponentData(entity, new ShootData());
        }
        if (AimAction != null && AimAction is IAbility)
        {
            dstManager.AddComponentData(entity, new AimData());
        }

        dstManager.AddComponentData(entity, new RunData
        {
            _runSpeed = runSpeed,
            _speed = speed,
            _isTestRun = isTestRun
        });

        if (ChangeMaterialAction != null && ChangeMaterialAction is IAbility)
        {
            dstManager.AddComponentData(entity, new ChangeMaterialData());
        }

        if (moveAnimHash != String.Empty)
        {
            dstManager.AddComponentData(entity, new MoveAnimData());
        }
        if (shootAnimHash != String.Empty)
        {
            dstManager.AddComponentData(entity, new AttackAnimData());
        }
        if (getHitAnimHash != String.Empty)
        {
            dstManager.AddComponentData(entity, new GetHitAnimData());
        }
        if (dieAnimHash != String.Empty)
        {
            dstManager.AddComponentData(entity, new DieAnimData());
        }
    }
}


// Components
public struct InputData : IComponentData
{
    public float2 Move;
    public float Shoot;
    public float Run;
    public float ChangeMat;
    public int CollideInput;
}

public struct MoveData : IComponentData
{
    public float Speed;
}

public struct ShootData : IComponentData
{

}
public struct AimData : IComponentData
{

}

public struct RunData : IComponentData
{
    public float _runSpeed;
    public float _speed;
    public bool _isTestRun;
}

public struct ChangeMaterialData : IComponentData
{
    public int isDissolve;
}
public struct MoveAnimData : IComponentData
{

}
public struct AttackAnimData : IComponentData
{

}
public struct GetHitAnimData : IComponentData
{

}
public struct DieAnimData : IComponentData
{

}