using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadPlayer : MonoBehaviour
{
    public GameObject[] players;
    public Transform playerSpawnPoint;

    private GameManager gameManager;

    private void Awake()
    {
        gameManager = GetComponent<GameManager>();
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            int selsectedPlayer = PlayerPrefs.GetInt("selectedPlayer");
            GameObject prefab = players[selsectedPlayer];
            Instantiate(prefab, playerSpawnPoint);
        }
    }
    public GameObject Player()
    {
        int selsectedPlayer = PlayerPrefs.GetInt("selectedPlayer");
        GameObject prefab = players[selsectedPlayer];
        return prefab;
    }
}
