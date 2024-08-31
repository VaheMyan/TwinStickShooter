using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ZombieArrowAbility : MonoBehaviour
{
    public List<MonoBehaviour> ApplyArrowActions = new List<MonoBehaviour>();
    public GameObject RightHend;
    public GameObject Arrow;

    private GameObject currentArrow = null;
    private ApplyZombieArrow currentZombieArrow;
    private ApplyPlayerDamage playerDamage;

    private void Start()
    {
        playerDamage = GetComponent<ApplyPlayerDamage>();
    }
    public void InstantiateObject()
    {
        currentArrow = Instantiate(Arrow, RightHend.transform);
        currentZombieArrow = currentArrow.GetComponent<ApplyZombieArrow>();
        currentZombieArrow.applyPlayerDamage = playerDamage;

        //if (SceneManager.GetActiveScene().buildIndex == 1)
        //{

        //}
        //else if (SceneManager.GetActiveScene().buildIndex == 2)
        //{
        //    currentArrow = PhotonNetwork.Instantiate(Arrow.name, RightHend.transform.position, RightHend.transform.rotation);
        //    currentArrow.transform.SetParent(RightHend.transform);
        //    currentZombieArrow = currentArrow.GetComponent<ApplyZombieArrow>();
        //    currentZombieArrow.applyPlayerDamage = playerDamage;
        //}
    }
    public void StartArrowMove()
    {
        currentZombieArrow.ArrowMove();
    }
}
