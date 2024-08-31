using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private NetworkManager networkManager;

    public TrailRenderer trailRenderer;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        var applyShootActions = player.GetComponent<UserInputData>().ApplyShootActions;
        trailRenderer = GetComponentInChildren<TrailRenderer>();
        applyShootActions.Add(this);
        giveBonusAbility = FindObjectOfType<GiveBonusAbility>();
        giveBonusAbility.applyShoot.Add(this);

        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            networkManager = GameObject.FindObjectOfType<NetworkManager>();
        }
    }
    public void Execute()
    {
        if (this == null) return;

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
        if (other.tag == "Zombie" || other.tag == "ZombieArrow" || other.tag == "SmallZombie")
        {
            if (isBonusBulletDamage)
            {
                BulletDamage = 10;

            }
            else
            {
                BulletDamage = 2;
            }

            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                other.GetComponent<ApplyZombieState>().TakeZombieDamage(BulletDamage);
                isDamaging = true;
            }
            else if (SceneManager.GetActiveScene().buildIndex == 2 && PhotonNetwork.LocalPlayer.IsMasterClient)
            {
                other.GetComponent<ApplyZombieState>().TakeZombieDamage(BulletDamage);
                isDamaging = true;
            }
            else if (SceneManager.GetActiveScene().buildIndex == 2 && !PhotonNetwork.LocalPlayer.IsMasterClient)
            {
                networkManager.SendZombieDamage(other.GetComponent<PhotonView>().ViewID, BulletDamage);

                other.GetComponent<ApplyZombieState>().TakeZombieDamage(BulletDamage);
                isDamaging = true;
            }
        }
        else if (other.tag == "Barrier")
        {
            DestroyBulletFroSeconds(0);
        }
    }

}