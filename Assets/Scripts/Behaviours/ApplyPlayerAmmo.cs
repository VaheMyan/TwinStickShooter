using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Gun
{
    public int bulletsInClip;
}

public class ApplyPlayerAmmo : MonoBehaviour
{
    public Gun[] Guns;

    [HideInInspector] public int spentBullets;
    private Text bulletCout;
    private ApplyReload applyReload;
    private ShootAbility shootAbility;

    private void Start()
    {
        bulletCout = GameObject.FindGameObjectWithTag("BulletCoutText").GetComponent<Text>();

        applyReload = GetComponent<ApplyReload>();
        shootAbility = GetComponent<ShootAbility>();

        SetGunBullets(0);
        spentBullets = 0;
    }

    public void CheckGunBullets(int i)
    {
        if (Guns[i].bulletsInClip > 0)
        {
            Guns[i].bulletsInClip -= 1;
            spentBullets++;
            UpdateBulletCoutText();
        }
        if (Guns[i].bulletsInClip < 30) // Reload
        {
            applyReload.canRealoading = true;
        }
        if (Guns[i].bulletsInClip <= 0) // Reload
        {
            shootAbility.CanShoot(0);
            applyReload.canRealoading = true;
        }
    }
    public void SetGunBullets(int i)
    {
        Guns[i].bulletsInClip = 30;
        UpdateBulletCoutText();
    }
    private void UpdateBulletCoutText()
    {
        bulletCout.text = Guns[0].bulletsInClip.ToString();
    }
}
