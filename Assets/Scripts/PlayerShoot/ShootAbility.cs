using System;
using System.Collections.Generic;
using UnityEngine;

public class ShootAbility : MonoBehaviour, IAbility, IAbilityTarget
{
    public float shootDelay;
    public float bulletSpeed;
    public float fireRate;
    public float bulletDamage;
    public bool isShooting = false;
    public bool isReload = false;
    public ShootingModel model;
    public ShootingView view;

    private ShootingPresenter shootingPresenter;
    private ApplyPlayerAmmo applyPlayerAmmo;
    private float _shootTime = float.MinValue;

    public void CanShoot(int value)
    {
        isShooting = (value != 0);
    }
    public void Reload(int value)
    {
        isReload = (value != 0);
    }
    private void Start()
    {
        applyPlayerAmmo = GetComponent<ApplyPlayerAmmo>();
        model = new ShootingModel(bulletSpeed, fireRate, bulletDamage);
        shootingPresenter = new ShootingPresenter(model, view);
    }
    public List<GameObject> Targets { get; set; }
    public void Execute()
    {
        if (Time.time < _shootTime + shootDelay) return;
        if (isShooting)
        {
            _shootTime = Time.time;
            shootingPresenter.StartShooting();
            applyPlayerAmmo.CheckGunBullets(0);
        }
        if (isReload)
        {
            isShooting = false;
        }

    }
}