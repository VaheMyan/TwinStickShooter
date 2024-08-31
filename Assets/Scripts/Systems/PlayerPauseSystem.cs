using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class PlayerPauseSystem : MonoBehaviour
{
    private UserInputData userInputData;
    private Menu menu;

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            userInputData = GameObject.FindObjectOfType<UserInputData>();
        }
        else if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            FindPlayer();
        }

        menu = GetComponent<Menu>();

        if (userInputData == null) return;
        OnUpdate();
    }
    private async void OnUpdate()
    {
        while (true)
        {
            if (this == null) return;

            if (userInputData.inputData.Pause == 1f && PhotonView.Get(this.gameObject).IsMine)
            {
                menu.Pause();
            }

            await Task.Delay(200);
        }
    }

    private async void FindPlayer()
    {
        //Multiplayer
        while (userInputData == null)
        {
            await Task.Delay(1500);
            userInputData = GameObject.FindObjectOfType<UserInputData>();
        }
        OnUpdate();
        return;
    }
}