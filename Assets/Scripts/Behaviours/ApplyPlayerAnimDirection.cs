using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ApplyPlayerAnimDirection : MonoBehaviour
{
    public float speed = 3.5f;
    public string movementDirection;

    [HideInInspector] public bool isBonus = false;
    private float bonusMoveSpeed = 1;
    private float bonusMoveDirection = 0.4f;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    public float Value()
    {
        return speed;
    }
    public void Execute(float horizontal, float vertical)
    {
        if (SceneManager.GetActiveScene().buildIndex == 2 && PhotonView.Get(this.gameObject).IsMine)
        {
            //Bounse
            if (isBonus)
            {
                bonusMoveSpeed = 1;
                bonusMoveDirection = 0.4f;
            }
            else
            {
                bonusMoveSpeed = 0;
                bonusMoveDirection = 0;
            }

            //Animation Direction
            Vector3 movement = new Vector3(horizontal, 0.0f, vertical).normalized * (speed + bonusMoveSpeed) * Time.deltaTime;

            if (movement != Vector3.zero)
            {
                transform.Translate(movement, Space.World);
                UpdateAnimatorParameters(movement);
            }
            else
            {
                animator.SetFloat(movementDirection, 0);
            }
        }
        else
        {
            //Bounse
            if (isBonus)
            {
                bonusMoveSpeed = 1;
                bonusMoveDirection = 0.4f;
            }
            else
            {
                bonusMoveSpeed = 0;
                bonusMoveDirection = 0;
            }

            //Animation Direction
            Vector3 movement = new Vector3(horizontal, 0.0f, vertical).normalized * (speed + bonusMoveSpeed) * Time.deltaTime;

            if (movement != Vector3.zero)
            {
                transform.Translate(movement, Space.World);
                UpdateAnimatorParameters(movement);
            }
            else
            {
                animator.SetFloat(movementDirection, 0);
            }
        }
    }
    void UpdateAnimatorParameters(Vector3 movement)
    {
        Vector3 localMovement = transform.InverseTransformDirection(movement);

        if (localMovement.z > 0)
        {
            animator.SetFloat(movementDirection, 1 + bonusMoveDirection); // Вперед
        }
        else if (localMovement.z < 0)
        {
            animator.SetFloat(movementDirection, -1 + bonusMoveDirection); // Назад
        }
        else if (localMovement.x > 0)
        {
            animator.SetFloat(movementDirection, 2 + bonusMoveDirection); // Вправо
        }
        else if (localMovement.x < 0)
        {
            animator.SetFloat(movementDirection, -2 + bonusMoveDirection); // Влево
        }
        else
        {
            animator.SetFloat(movementDirection, 0); // Нет движения
        }
    }
}
