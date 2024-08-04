using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyPlayerState : MonoBehaviour
{
    private PlayerHealth playerHealth;

    private void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    public void TakeDamage(int damage)
    {
        playerHealth.TakeDamage(damage);
    }
    public void GiveBenefit(int benefit)
    {
        playerHealth.GiveBenefit(benefit);
    }
}
