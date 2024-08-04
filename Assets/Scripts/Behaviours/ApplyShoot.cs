using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ApplyShoot : MonoBehaviour, IAbilityBullet
{
    public int Speed = 5;
    public List<GameObject> Targets { get; set; }
    public Transform targetTransform;
    public float attackRange = 0.025f;
    public LayerMask enamyLayers;
    public Transform attackPoint;

    bool isStartDestroy = false;
    bool isStartDestroForTwoSeconds = false;
    private GameObject player;
    private bool isDamaging = false;

    public TrailRenderer trailRenderer;

    private void Awake()
    {
        player = GameObject.Find("Player");
        var applyShootActions = player.GetComponent<UserInputData>().ApplyShootActions;
        trailRenderer = GetComponentInChildren<TrailRenderer>();
        applyShootActions.Add(this);
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
                trailRenderer.enabled = false;
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
                trailRenderer.enabled = false;
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
            other.GetComponent<ApplyZombieState>().TakeZombieDamage(2);
            isDamaging = true;
        }
    }

}