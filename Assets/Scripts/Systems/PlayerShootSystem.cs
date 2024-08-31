using Photon.Pun;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerShootSystem : MonoBehaviour
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
            if (this == null || SceneManager.GetActiveScene().buildIndex == 2 && !PhotonView.Get(this.gameObject).IsMine) return;

            if (userInputData.inputData.Shoot > 0f && userInputData.ShootAction != null && userInputData.ShootAction is IAbility ability)
            {
                ability.Execute();
            }
            foreach (var abilityBullet in userInputData.ApplyShootActions)
            {
                if (abilityBullet is IAbilityBullet _abilityBullet)
                {
                    _abilityBullet.Execute();
                }
            }

            await Task.Delay(10);
        }
    }


}
