using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class ApplyPlayerDamage : MonoBehaviour
{
    public int _damage;
    public int DamageDistance = 4;
    public bool isArrow = false;
    private ApplyPlayerState playerState;
    private ApplyZombieMove zombieMove;
    private NetworkManager networkManager;

    private void Start()
    {
        playerState = GameObject.FindObjectOfType<ApplyPlayerState>();
        zombieMove = GetComponent<ApplyZombieMove>();

        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            networkManager = GameObject.FindObjectOfType<NetworkManager>();
        }
    }

    public void TakeDamage()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            foreach (Player player in PhotonNetwork.PlayerList)
            {
                Vector3 playerPosition = PhotonNetwork.GetPhotonView(int.Parse(player.ActorNumber.ToString() + "001")).transform.position;

                if (Vector3.Distance(transform.position, playerPosition) < DamageDistance || isArrow)
                {
                    if (PhotonView.Get(PhotonNetwork.GetPhotonView(int.Parse(player.ActorNumber.ToString() + "001")).gameObject).IsMine)
                    {
                        playerState.TakeDamage(_damage);
                    }
                    else if(!PhotonView.Get(PhotonNetwork.GetPhotonView(int.Parse(player.ActorNumber.ToString() + "001")).gameObject).IsMine)
                    {
                        networkManager.SendPlayer(player, _damage);
                    }
                }
            }
            return;
        }
        playerState.TakeDamage(_damage);
    }
}
