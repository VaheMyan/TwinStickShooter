using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyZombieState : MonoBehaviour
{
    public ZombieHealth zombieHealth;
    public ApplyZombieMove zombieMove;

    public void TakeZombieDamage(int damage)
    {
        zombieHealth.TakeDamageZombie(damage);
        zombieMove.isDamaging = true;
    }
    public void GiveZombieBenefit(int benefit)
    {
        zombieHealth.GiveBenefitZombie(benefit);
    }
}
