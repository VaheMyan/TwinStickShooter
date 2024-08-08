using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyReload : MonoBehaviour, IAbility
{
    [HideInInspector] public bool canRealoading = false;

    public bool isReloading = false;

    private ApplyPlayerAmmo playerAmmo;
    private ShootAbility shootAbility;
    private Animator animator;
    private AudioManager audioManager;

    private void Start()
    {
        animator = GetComponent<Animator>();
        playerAmmo = GetComponent<ApplyPlayerAmmo>();
        shootAbility = GetComponent<ShootAbility>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    public void Execute()
    {
        if (isReloading == false && canRealoading)
        {
            isReloading = true;
            canRealoading = false;

            shootAbility.CanShoot(0);
            shootAbility.Reload(1);
            audioManager.PlaySFX("AkReload");

            isReloading = false;
        }
    }
}
