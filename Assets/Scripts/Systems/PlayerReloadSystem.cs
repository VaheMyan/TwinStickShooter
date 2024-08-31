using Photon.Pun;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerReloadSystem : MonoBehaviour
{
    private UserInputData userInputData;

    private void Start()
    {
        userInputData = GetComponent<UserInputData>();

        OnUpdate();
    }
    private async void OnUpdate()
    {
        while (true)
        {
            if (this == null) return;
            if (SceneManager.GetActiveScene().buildIndex == 2 && !PhotonView.Get(this.gameObject).IsMine) return;

            if (userInputData.ReloadAction != null && userInputData.inputData.Reload == 1f && userInputData.ReloadAction is IAbility ability)
            {
                ability.Execute();
            }

            await Task.Delay(10);
        }
    }
}
