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
    //private ApplyEnamyDamage enamyDamage;
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
            //Collider2D[] hitEnamies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enamyLayers);
            //foreach (Collider2D enamy in hitEnamies)
            //{
            //    //enamyDamage = enamy.GetComponent<ApplyEnamyDamage>();
            //    //enamyDamage.Execute();
            //    isDamaging = true;
            //}

            transform.position += transform.up * Speed * Time.deltaTime;
            DestroyBulletFroTwoSeconds();
        }
        if (isDamaging)
        {
            DestroyBulletFroSeconds(0);
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

}