using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Event
{
    public GameObject[] enamys;
    public int countEnemies;
}

public class GameManager : MonoBehaviour
{
    public Event[] Events;
    public Transform[] SpawnPoints;

    private void Start()
    {
        SetWave(Events[0]);
    }

    private void SetWave(Event _event)
    {
        Debug.Log("Do something");
    }
}
