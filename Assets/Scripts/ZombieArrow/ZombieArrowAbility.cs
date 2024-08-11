using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }
    public void StartArrowMove()
    {
        currentZombieArrow.ArrowMove();
    }
}
