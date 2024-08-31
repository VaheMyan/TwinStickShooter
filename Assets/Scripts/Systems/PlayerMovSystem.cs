using UnityEngine;
using Unity.Mathematics;
using System.Threading.Tasks;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class PlayerMovSystem : MonoBehaviour
{
    private UserInputData userInputData;
    private Animator animator;

    private Vector3 boundaryMin = new Vector3(-20f, 0f, -20f);
    private Vector3 boundaryMax = new Vector3(20f, 0f, 20f);

    private void Start()
    {
        userInputData = GetComponent<UserInputData>();
        animator = GetComponent<Animator>();

        OnUpdate();
    }
    private async void OnUpdate()
    {
        while (true)
        {
            if (this == null) return;

            if (transform != null && SceneManager.GetActiveScene().buildIndex == 2 && PhotonView.Get(this.gameObject).IsMine)
            {
                transform.position += new Vector3(userInputData.inputData.Move.x * userInputData.moveData.Speed * Time.deltaTime, y: 0, userInputData.inputData.Move.y * userInputData.moveData.Speed * Time.deltaTime);
                transform.position = (Vector3)transform.position;

                //transform.position += animator.deltaPosition;
                //transform.forward = animator.deltaRotation * transform.forward;
            }
            else
            {
                transform.position += new Vector3(userInputData.inputData.Move.x * userInputData.moveData.Speed * Time.deltaTime, y: 0, userInputData.inputData.Move.y * userInputData.moveData.Speed * Time.deltaTime);
                transform.position = (Vector3)transform.position;
            }

            transform.position = math.clamp(transform.position, boundaryMin, boundaryMax);

                if (transform.position.y < 0)
                {
                    transform.position = new Vector3(transform.position.x, 0, transform.position.z);
                }

            await Task.Delay(1);
        }
    }
    private void OnAnimatorMove()
    {

    }
}