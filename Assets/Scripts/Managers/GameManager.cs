using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Entities;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class Event
{
    public GameObject[] enamys;
    public int countEnemies;
}

public class GameManager : MonoBehaviour
{
    public int Coins;
    public Event[] Events;
    public Transform[] SpawnPoints;
    public Text waveText;

    public GameObject[] Potions = new GameObject[5];

    [HideInInspector] public GameMode gameMode;

    [HideInInspector] public List<GameObject> currentZombies = new List<GameObject>();
    [HideInInspector] public int waveIndex = 0;

    private void Awake()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            gameMode = (GameMode)0;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            gameMode = (GameMode)1;
        }
    }
    private void Start()
    {
        Coins = PlayerPrefs.GetInt("PlayerCoins");

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            waveIndex = 0;
            SetWave(Events[waveIndex], waveIndex);
            waveText.text = waveIndex.ToString();
            CheckZombieCount();
        }
    }
    public void GiveCoin(int _coin)
    {
        Coins += _coin;
        PlayerPrefs.SetInt("PlayerCoins", Coins);
    }
    private GameObject Potion()
    {
        int probability = Random.Range(0, 3);
        if (Potions.Length > 0 && probability == 0)
        {
            return Potions[Random.Range(0, Potions.Length)];
        }
        else
        {
            return null;
        }
    }
    public void InstantiatePotion(Vector3 _zombiePosition)
    {
        GameObject potion = Potion();
        if (potion == null) return;

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (potion.tag == "MedBox")
            {
                Instantiate(potion, new Vector3(_zombiePosition.x, potion.transform.position.y, _zombiePosition.z), Quaternion.identity);
            }
            else
            {
                Instantiate(potion, new Vector3(_zombiePosition.x, potion.transform.position.y, _zombiePosition.z), Quaternion.Euler(-90, 0, 0));
            }
        }
        else if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            if (potion.tag == "MedBox")
            {
                PhotonNetwork.Instantiate(potion.name, new Vector3(_zombiePosition.x, potion.transform.position.y, _zombiePosition.z), Quaternion.identity);
            }
            else
            {
                PhotonNetwork.Instantiate(potion.name, new Vector3(_zombiePosition.x, potion.transform.position.y, _zombiePosition.z), Quaternion.Euler(-90, 0, 0));
            }
        }
    }

    private void SetWave(Event _event, int index)
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            for (int i = 0; i < _event.countEnemies; i++)
            {
                GameObject enamyPrefab = _event.enamys[Random.Range(0, _event.enamys.Length)];

                var enamy = Instantiate(enamyPrefab, SpawnPoints[index]);
                currentZombies.Add(enamy);
            }
        }
        
    }
    public async void CheckZombieCount()
    {
        while (true)
        {
            bool isEmpty = true;
            foreach (var zombie in currentZombies)
            {
                if (zombie != null)
                {
                    await Task.Delay(100);
                    isEmpty = false;
                }
            }

            if (isEmpty)
            {
                waveIndex++;
                SetWave(Events[waveIndex], waveIndex);
                if (waveText == null)
                {
                    break;
                }
                waveText.text = waveIndex.ToString();
                await Task.Delay(3000);
                CheckZombieCount();
                break;
            }
        }
    }
}

public enum GameMode
{
    Single = 0,
    Multiplayer = 1,
}