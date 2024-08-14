using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
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

    public GameObject[] Potions = new GameObject[5];

    private MainMenu mainMenu;

    private void Start()
    {
        Coins = PlayerPrefs.GetInt("PlayerCoins");
        mainMenu = GameObject.FindObjectOfType<MainMenu>();

        SetWave(Events[0]);
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

        if (potion.tag == "MedBox") Instantiate(potion, new Vector3(_zombiePosition.x, potion.transform.position.y, _zombiePosition.z), Quaternion.identity);
        else Instantiate(potion, new Vector3(_zombiePosition.x, potion.transform.position.y, _zombiePosition.z), Quaternion.Euler(-90, 0, 0));
    }

    private void SetWave(Event _event)
    {
        for (int i = 0; i < _event.countEnemies; i++)
        {
            GameObject enamyPrefab = _event.enamys[Random.Range(0, _event.enamys.Length)];
            //ConvertToEntity convertToEntity = enamyPrefab.AddComponent<ConvertToEntity>();
            //convertToEntity.ConversionMode = ConvertToEntity.Mode.ConvertAndDestroy;

            var enamy = Instantiate(enamyPrefab, SpawnPoints[0]);
        }
    }
}
