using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ApplyPlayerState : MonoBehaviour
{
    [HideInInspector] public bool isReduce = false;
    private PlayerHealth playerHealth;

    private void Start()
    {
        //Multiplayer
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            FindPlayer();
            return;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        }
    }

    public void TakeDamage(int _damage)
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (isReduce)
            {
                playerHealth.TakeDamage(_damage -= (_damage - (_damage / 3)));
            }
            else
            {
                playerHealth.TakeDamage(_damage);
            }
        }
        if (SceneManager.GetActiveScene().buildIndex == 2 && PhotonView.Get(playerHealth.gameObject).IsMine)
        {
            if (isReduce)
            {
                playerHealth.TakeDamage(_damage -= (_damage - (_damage / 3)));
            }
            else
            {
                playerHealth.TakeDamage(_damage);
            }
        }
    }

    public void GiveBenefit(int benefit)
    {
        playerHealth.GiveBenefit(benefit);
    }

    private async void FindPlayer()
    {
        //Multiplayer
        GameObject Player = null;
        while (Player == null)
        {
            await Task.Delay(500);
            Player = PlayerMultiplayerData.Instance.Player;
        }
        playerHealth = Player.GetComponent<PlayerHealth>();
        return;
    }
}
