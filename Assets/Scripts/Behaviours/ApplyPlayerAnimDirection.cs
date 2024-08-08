using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyPlayerAnimDirection : MonoBehaviour
{
    public float speed = 3.5f;
    public string movementDirection;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void Execute(float horizontal, float vertical)
    {
        Vector3 movement = new Vector3(horizontal, 0.0f, vertical).normalized * speed * Time.deltaTime;

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
    void UpdateAnimatorParameters(Vector3 movement)
    {
        Vector3 localMovement = transform.InverseTransformDirection(movement);

        if (localMovement.z > 0)
        {
            animator.SetFloat(movementDirection, 1); // ������
        }
        else if (localMovement.z < 0)
        {
            animator.SetFloat(movementDirection, -1); // �����
        }
        else if (localMovement.x > 0)
        {
            animator.SetFloat(movementDirection, 2); // ������
        }
        else if (localMovement.x < 0)
        {
            animator.SetFloat(movementDirection, -2); // �����
        }
        else
        {
            animator.SetFloat(movementDirection, 0); // ��� ��������
        }
    }
}
