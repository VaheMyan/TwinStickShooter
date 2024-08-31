using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMultiplayerData : MonoBehaviour
{
    public static PlayerMultiplayerData Instance;

    public GameObject Player;
    public GameManager gameManager;
    public List<Transform> PlayersTransforms;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        gameManager = GameObject.FindObjectOfType<GameManager>();
        if (gameManager == null) Debug.Log("GameManager is null");
    }
    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            PlayersTransforms.Add(Player.transform);
        }
    }
    public void AddPlayerTransform(Transform _playerTransform)
    {
        PlayersTransforms.Add(_playerTransform);
    }
    public void RemovePlayerTransform(Transform _playerTransform)
    {
        PlayersTransforms.Remove(_playerTransform);
    }
}
