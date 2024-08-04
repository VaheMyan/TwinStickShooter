using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyZombieState : MonoBehaviour
{
    public ZombieHealth zombieHealth;

    public void TakeZombieDamage(int damage)
    {
        zombieHealth.TakeDamageZombie(damage);
    }
    public void GiveZombieBenefit(int benefit)
    {
        zombieHealth.GiveBenefitZombie(benefit);
    }
}
