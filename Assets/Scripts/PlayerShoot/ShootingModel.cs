using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingModel
{
    public float BulletSpeed { get; set; }
    public float FireRate { get; set; }
    public float BulletDamage { get; set; }

    public ShootingModel(float bulletSpeed, float fireRate, float bulletDamage)
    {
        BulletSpeed = bulletSpeed;
        FireRate = fireRate;
        BulletDamage = bulletDamage;
    }
}