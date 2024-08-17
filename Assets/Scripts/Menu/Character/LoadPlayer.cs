using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPlayer : MonoBehaviour
{
    public GameObject[] players;
    public Transform playerSpawnPoint;

    private void Awake()
    {
        int selsectedPlayer = PlayerPrefs.GetInt("selectedPlayer");
        GameObject prefab = players[selsectedPlayer];
        Instantiate(prefab, playerSpawnPoint);
    }
}
