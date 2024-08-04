using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class AIBehaviourManager : MonoBehaviour, IConvertGameObjectToEntity
{
    public List<MonoBehaviour> behaviours;
    public IBehaviour _activeBehaviour;

    void Start()
    {

    }
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponent<AIAgent>(entity);
    }
    public struct AIAgent : IComponentData
    {

    }
}
