using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingPresenter
{
    private ShootingModel model;
    private ShootingView view;
    private Coroutine shootingCoroutine;

    public ShootingPresenter(ShootingModel model, ShootingView view)
    {
        this.model = model;
        this.view = view;
    }

    public void StartShooting()
    {
        if (shootingCoroutine == null)
        {
            shootingCoroutine = view.StartCoroutine(ShootingRoutine());
        }
        else
        {
            view.StartCoroutine(ShootingRoutine());
        }
    }
    public void StopShooting()
    {
        if (shootingCoroutine != null)
        {
            view.StopCoroutine(shootingCoroutine);
            shootingCoroutine = null;
        }
    }
    private IEnumerator ShootingRoutine()
    {
        view.ShootBullet(view.firePoint.forward, model.BulletSpeed);
        yield return new WaitForSeconds(1f * model.FireRate);
    }
}