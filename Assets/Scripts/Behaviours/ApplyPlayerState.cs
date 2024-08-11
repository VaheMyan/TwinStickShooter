using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyPlayerState : MonoBehaviour
{
    [HideInInspector] public bool isReduce = false;
    private PlayerHealth playerHealth;

    private void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    public void TakeDamage(int _damage)
    {
        if (isReduce)
        {
            playerHealth.TakeDamage(_damage -= (_damage - (_damage / 3)));
        }
        else
        {
            playerHealth.TakeDamage(_damage);
        }
    }

    public void GiveBenefit(int benefit)
    {
        playerHealth.GiveBenefit(benefit);
    }
}
