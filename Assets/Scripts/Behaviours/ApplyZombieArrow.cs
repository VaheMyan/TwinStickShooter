using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ApplyZombieArrow : MonoBehaviour, IArrow
{
    public float Speed = 50f;

    [HideInInspector] public ApplyPlayerDamage applyPlayerDamage;

    private CapsuleCollider capsuleCollider;
    private ZombieArrowAbility zombieArrowAbility;
    private bool isDamaging = false;
    bool isStartDestroy = false;
    bool isStartDestroForTwoSeconds = false;

    private void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
        zombieArrowAbility = GameObject.FindObjectOfType<ZombieArrowAbility>();
        zombieArrowAbility.ApplyArrowActions.Add(this);
    }

    public async void ArrowMove()
    {
        while (this != null && gameObject.activeInHierarchy)
        {
            transform.SetParent(null);
            if (!isDamaging)
            {
                transform.position += -transform.forward * Speed * Time.deltaTime;
                capsuleCollider.enabled = true;
                DestroyBulletFroTwoSeconds();
            }
            if (isDamaging)
            {
                DestroyBulletFroSeconds(100);
            }
            await Task.Delay(10);
        }
        
    }

    private async void DestroyBulletFroSeconds(int seconds)
    {
        if (isStartDestroy == false)
        {
            isStartDestroy = true;
            await Task.Delay(seconds);
            if (this != null && SceneManager.GetActiveScene().buildIndex == 1)
            {
                Destroy(this.gameObject);
                isDamaging = false;
            }
            else if (this != null && SceneManager.GetActiveScene().buildIndex == 2)
            {
                //PhotonNetwork.
                Destroy(this.gameObject);
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
            if (this != null && SceneManager.GetActiveScene().buildIndex == 1)
            {
                Destroy(this.gameObject);
                isDamaging = false;
            }
            else if (this != null && SceneManager.GetActiveScene().buildIndex == 2)
            {
                //PhotonNetwork.
                Destroy(this.gameObject);
                isDamaging = false;
            }
            isStartDestroForTwoSeconds = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            applyPlayerDamage.TakeDamage();
            isDamaging = true;
        }
    }
}