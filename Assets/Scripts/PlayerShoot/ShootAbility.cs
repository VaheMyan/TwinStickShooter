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
    public ShootingModel model;
    public ShootingView view;

    private ShootingPresenter shootingPresenter;
    private float _shootTime = float.MinValue;

    public void CanShoot(bool value)
    {
        isShooting = value;
    }
    private void Start()
    {
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
        }
    }
}