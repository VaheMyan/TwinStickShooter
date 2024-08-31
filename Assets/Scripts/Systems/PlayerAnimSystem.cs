using System;
using System.Threading.Tasks;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class PlayerAnimSystem : MonoBehaviour
{
    private UserInputData userInputData;
    private ApplyPlayerAnimDirection playerAnimDirection;
    private AimAbility aimAbility;
    private PlayerHealth playerHealth;
    private ShootAbility shootAbility;
    private Animator animator;

    private void Start()
    {
        userInputData = GetComponent<UserInputData>();
        playerAnimDirection = GetComponent<ApplyPlayerAnimDirection>();
        aimAbility = GetComponent<AimAbility>();
        playerHealth = GetComponent<PlayerHealth>();
        shootAbility = GetComponent<ShootAbility>();
        animator = GetComponent<Animator>();

        OnUpdate();
    }
    private async void OnUpdate()
    {
        while (true)
        {
            if (this == null) return;

            if (animator != null && userInputData != null && playerHealth != null)
            {
                if (SceneManager.GetActiveScene().buildIndex == 2 && !PhotonView.Get(this.gameObject).IsMine) return;

                animator.SetBool(userInputData.moveAnimHash, Math.Abs(userInputData.inputData.Move.x) > 0.05f || Math.Abs(userInputData.inputData.Move.y) > 0.05f); // Walk anim
                animator.SetBool(userInputData.shootAnimHash, Math.Abs(userInputData.inputData.Shoot) > 0f && shootAbility.isShooting); // Attack aim

                animator.SetBool(userInputData.reloadAnimHash, shootAbility.isReload);


                float x = 0;
                float y = 0;
                if (userInputData.inputData.Move.x < 0)
                {
                    x = -userInputData.inputData.Move.x;
                }
                else
                {
                    x = userInputData.inputData.Move.x;
                }
                if (userInputData.inputData.Move.y < 0)
                {
                    y = -userInputData.inputData.Move.y;
                }
                else
                {
                    y = userInputData.inputData.Move.y;
                }
                float result = Mathf.Max(x, y);

                if (userInputData.moveAnimSpeedHash == String.Empty) return;
                animator.SetFloat(userInputData.moveAnimSpeedHash, 1.5f * result);
            }

            playerAnimDirection.Execute(userInputData.inputData.Move.x, userInputData.inputData.Move.y);

            await Task.Delay(10);
        }
    }
}