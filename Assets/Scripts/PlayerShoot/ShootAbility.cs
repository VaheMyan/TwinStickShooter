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
    public ParticleSystem shootPartical;

    [HideInInspector] public bool isBonusShootDelay = false;
    private AudioManager audioManager;
    private ShootingPresenter shootingPresenter;
    private ApplyPlayerAmmo applyPlayerAmmo;
    private float _shootTime = float.MinValue;

    public float Value()
    {
        return shootDelay * 30;
    }
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
        audioManager = FindObjectOfType<AudioManager>();
        applyPlayerAmmo = GetComponent<ApplyPlayerAmmo>();
        model = new ShootingModel(bulletSpeed, fireRate, bulletDamage);
        shootingPresenter = new ShootingPresenter(model, view);
    }
    public List<GameObject> Targets { get; set; }
    public void Execute()
    {
        if (isBonusShootDelay) shootDelay = 0.1f;
        else shootDelay = 0.2f;

        if (Time.time < _shootTime + shootDelay) return;
        if (isShooting )
        {
            _shootTime = Time.time;
            shootingPresenter.StartShooting();
            audioManager?.PlaySFX("AkShoot");
            shootPartical.Play();
            applyPlayerAmmo.CheckGunBullets(0);
        }
        if (isReload)
        {
            isShooting = false;
        }

    }
}