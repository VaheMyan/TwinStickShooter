using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public GameObject PlayerSample;
    public GameObject CurrentPlayer;
    public GameObject[] Items;
    public Transform SpawnPoint;
    public float spawnRadius = 5;

    public bool endDisconnected = false;

    private LoadPlayer loadPlayer;
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        loadPlayer = GameObject.FindObjectOfType<LoadPlayer>();
        PlayerSample = loadPlayer.Player();
        StartConected();
    }
    public void StartConected()
    {
        Debug.Log("Connecting...");

        PhotonNetwork.ConnectUsingSettings();
    }
    public void StartDisconnected()
    {
        endDisconnected = false;
        Debug.Log("Disconnecting...");

        PhotonNetwork.Disconnect();
    }

    public override void OnConnectedToMaster()
    {
        RoomOptions options = new RoomOptions
        {
            MaxPlayers = 4,
            IsVisible = false
        };
        PhotonNetwork.JoinOrCreateRoom("Test", options, TypedLobby.Default);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Disconnection was successful.");
        endDisconnected = true;
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Connection was successful.");

        var id = PhotonNetwork.LocalPlayer.ActorNumber;
        Debug.Log("Joined Room with " + PhotonNetwork.CurrentRoom.PlayerCount + " players and ID is " + id);

        Vector3 randomOffset = new Vector3(Random.Range(-spawnRadius, spawnRadius), 0, Random.Range(-spawnRadius, spawnRadius));
        SpawnPoint.position += randomOffset;

        CurrentPlayer = PhotonNetwork.Instantiate(PlayerSample.name, SpawnPoint.position, Quaternion.identity, 0);
        CurrentPlayer.name = "Player " + id;
        CurrentPlayer.transform.SetParent(SpawnPoint);
        PlayerMultiplayerData.Instance.Player = CurrentPlayer;

        if (PhotonNetwork.CurrentRoom.PlayerCount > 1)
        {
            this.photonView.RPC("RPC_ChangePlayerName", RpcTarget.Others, (byte)PhotonNetwork.LocalPlayer.ActorNumber);
        }

        if (PhotonNetwork.IsMasterClient)
        {
            PlayerMultiplayerData.Instance.PlayersTransforms.Add(CurrentPlayer.transform);

            gameManager.waveIndex = 0;
            ZombieSpawn(gameManager.Events[gameManager.waveIndex], gameManager.waveIndex);
            gameManager.waveText.text = gameManager.waveIndex.ToString();
            CheckZombieCount();
        }
        SendPlayerTransform();
    }
    public void ZombieSpawn(Event _event, int index)
    {
        for (int i = 0; i < _event.countEnemies; i++)
        {
            GameObject enamyPrefab = _event.enamys[Random.Range(0, _event.enamys.Length)];

            var enamy = PhotonNetwork.Instantiate(enamyPrefab.name, gameManager.SpawnPoints[index].position, Quaternion.identity);
            gameManager.currentZombies.Add(enamy);
        }
    }
    async void CheckZombieCount()
    {
        while (true)
        {
            bool isEmpty = true;
            foreach (var zombie in gameManager.currentZombies)
            {
                if (zombie != null)
                {
                    await Task.Delay(100);
                    isEmpty = false;
                }
            }

            if (isEmpty)
            {
                gameManager.waveIndex++;
                ZombieSpawn(gameManager.Events[gameManager.waveIndex], gameManager.waveIndex);
                if (gameManager.waveText == null)
                {
                    break;
                }
                gameManager.waveText.text = gameManager.waveIndex.ToString();
                SendWaveIndex(gameManager.waveIndex);
                await Task.Delay(3000);
                CheckZombieCount();
                break;
            }
        }
    }
    
    public void SendZombieDamage(int viewId, int damage)
    {
        this.photonView.RPC("RPC_SendZombieDamage", RpcTarget.MasterClient, viewId, damage);
    }
    public void SendPlayerBonuse(int viewId)
    {
        this.photonView.RPC("RPC_SendPlayerBonuse", RpcTarget.MasterClient, viewId);
    }
    public void SendPlayer(Player player, int damage)
    {
        this.photonView.RPC("RPC_SetPlayer", player, damage);
    }
    public void SendPlayerTransform()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            this.photonView.RPC("RPC_SetPlayerTransform", RpcTarget.MasterClient, (byte)PhotonNetwork.LocalPlayer.ActorNumber);
        }
    }
    public void SendWaveIndex(int _waveIndex)
    {
        this.photonView.RPC("RPC_SetWaveIndex", RpcTarget.Others, _waveIndex);
    }
    public void SayHello()
    {
        this.photonView.RPC("Hello", RpcTarget.Others, (byte)PhotonNetwork.LocalPlayer.ActorNumber);
    }
    public void SendPlayerID(Player targetPlayer)
    {
        this.photonView.RPC("RPC_ChangePlayerName", targetPlayer, (byte)PhotonNetwork.LocalPlayer.ActorNumber);
    }
    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        SendPlayerID(newPlayer);

        ((GameObject)newPlayer.TagObject).transform.SetParent(SpawnPoint);
    }

    [PunRPC]
    public void Hello(byte playerID)
    {
        Debug.Log($"Player ID {playerID} said Hello!");
    }
    [PunRPC]
    public void RPC_ChangePlayerName(byte myActorNumber)
    {
        var otherPlayer = GameObject.Find("Player(Clone)");
        if (otherPlayer != null)
        {
            otherPlayer.name = "Player" + myActorNumber;
        }
    }
    [PunRPC]
    public void RPC_SetWaveIndex(int _waveIndex)
    {
        gameManager.waveIndex = _waveIndex;
        gameManager.waveText.text = _waveIndex.ToString();
    }
    [PunRPC]
    public void RPC_SetPlayerTransform(byte myActorNumber)
    {
        string _playerName = PhotonNetwork.CurrentRoom.GetPlayer(myActorNumber).NickName;

        PlayerMultiplayerData.Instance.AddPlayerTransform(GameObject.Find("Player" + myActorNumber).transform);
    }
    [PunRPC]
    public void RPC_SetPlayer(int _damage)
    {
        ApplyPlayerState playerState = GameObject.FindObjectOfType<ApplyPlayerState>();
        playerState.TakeDamage(_damage);
        Debug.Log("Take damage on other player");
    }
    [PunRPC]
    public void RPC_SendPlayerBonuse(int _viewId)
    {
        if (PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            PhotonNetwork.Destroy(PhotonNetwork.GetPhotonView(_viewId).gameObject);
            Debug.Log("I am master client and I destroyed item");
        }
        else
        {
            Debug.Log("I am not master client");
        }
    }
    [PunRPC]
    public void RPC_SendZombieDamage(int _viewId, int _damage)
    {
        if (PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            GameObject zombie = PhotonNetwork.GetPhotonView(_viewId).gameObject;
            zombie.GetComponent<ApplyZombieState>().TakeZombieDamage(_damage);
            Debug.Log("I am master client and I damaging zombie");
        }
        else
        {
            Debug.Log("I am not master client");
        }
    }
}