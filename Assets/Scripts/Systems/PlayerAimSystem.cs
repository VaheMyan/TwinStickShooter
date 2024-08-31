using System.Threading.Tasks;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class PlayerAimSystem : MonoBehaviour
{
    private UserInputData userInputData;
    private AimAbility aimAbility;

    private void Start()
    {
        userInputData = GetComponent<UserInputData>();
        aimAbility = GetComponent<AimAbility>();

        OnUpdate();
    }
    private async void OnUpdate()
    {
        while (true)
        {
            if (this == null || SceneManager.GetActiveScene().buildIndex == 2 && !PhotonView.Get(this.gameObject).IsMine) return;

            if (userInputData.AimAction != null && userInputData.AimAction is IAbility ability)
            {
                aimAbility.mousePosition = Input.mousePosition;
                aimAbility.crosshairs.transform.position = new Vector2(aimAbility.mousePosition.x, aimAbility.mousePosition.y);

                ability.Execute();
            }

            await Task.Delay(10);
        }
    }


}
