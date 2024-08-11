using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ApplyShoot : MonoBehaviour, IAbilityBullet
{
    public int Speed = 5;
    public int BulletDamage = 2;
    public List<GameObject> Targets { get; set; }
    public Transform attackPoint;

    [HideInInspector] public bool isBonusBulletDamage = false;
    bool isStartDestroy = false;
    bool isStartDestroForTwoSeconds = false;
    private GameObject player;
    private bool isDamaging = false;
    private GiveBonusAbility giveBonusAbility;

    public TrailRenderer trailRenderer;

    private void Awake()
    {
        player = GameObject.Find("Player");
        var applyShootActions = player.GetComponent<UserInputData>().ApplyShootActions;
        trailRenderer = GetComponentInChildren<TrailRenderer>();
        applyShootActions.Add(this);
        giveBonusAbility = FindObjectOfType<GiveBonusAbility>();
        giveBonusAbility.applyShoot.Add(this);
    }
    public void Execute()
    {
        attackPoint = this.transform;
        if (this.gameObject.activeInHierarchy && !isDamaging)
        {
            transform.position += transform.up * Speed * Time.deltaTime;
            DestroyBulletFroTwoSeconds();
        }
        if (isDamaging)
        {
            DestroyBulletFroSeconds(500);
        }
    }

    private async void DestroyBulletFroSeconds(int seconds)
    {
        if (isStartDestroy == false)
        {
            isStartDestroy = true;
            await Task.Delay(seconds);
            if (this != null)
            {
                this.gameObject.SetActive(false);
                isDamaging = false;
            }
            isStartDestroy = false;
        }
    }
    private async void DestroyBulletFroTwoSeconds()
    {
        if (isStartDestroForTwoSeconds == false)
        {
            isStartDestroForTwoSeconds = true;
            await Task.Delay(2000);
            if (this != null)
            {
                this.gameObject.SetActive(false);
                isDamaging = false;
            }
            isStartDestroForTwoSeconds = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Zombie")
        {
            if (isBonusBulletDamage)
            {
                BulletDamage = 5;

            }
            else
            {
                BulletDamage = 2;
            }
            other.GetComponent<ApplyZombieState>().TakeZombieDamage(BulletDamage);
            isDamaging = true;
        }
    }

}