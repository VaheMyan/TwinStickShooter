using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ApplyZombieMove : MonoBehaviour, IBehaviour
{
    public bool isBoosted = false;
    public float MoveSpeed;
    [HideInInspector] public Transform targetTransform;
    [HideInInspector] public bool isAttack = false;
    [HideInInspector] public bool isDamaging = false;
    private NavMeshAgent navMeshAgent;
    private ApplyZombieAnim zombieAnim;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        zombieAnim = GetComponent<ApplyZombieAnim>();
        targetTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }
    public float Evaluate()
    {
        if (targetTransform == null) return 0;
        return 1 / (this.gameObject.transform.position - targetTransform.position).magnitude;
    }
    public void Behaviour()
    {
        if (isBoosted)
        {
            if (Vector3.Distance(targetTransform.position, transform.position) < 15f && Vector3.Distance(targetTransform.position, transform.position) > 9.8f && !isAttack)
            {
                MoveToTarget(_speed: 2, _targetPosition: targetTransform.position);

                //Animacion
                zombieAnim.ApplyAnim(zombieAnim.WalkAnimHash, true);
                zombieAnim.ApplyAnim(zombieAnim.WalkAnimHash, true, 2);
            }
            else if (Vector3.Distance(targetTransform.position, transform.position) < 10f)
            {
                isAttack = true;
            }
        }
        else if (Vector3.Distance(targetTransform.position, transform.position) < 12f || isDamaging)
        {
            isAttack = true;
        }
        if (isDamaging)
        {
            isAttack = true;
        }

        if (targetTransform != null && isAttack)
        {
            if (Vector3.Distance(targetTransform.position, transform.position) > 2f)
            {
                MoveToTarget(_speed: MoveSpeed, _targetPosition: targetTransform.position);

                //Animacion
                zombieAnim.ApplyAnim(zombieAnim.WalkAnimHash, true);
                zombieAnim.ApplyAnim(zombieAnim.WalkAnimHash, true, zombieAnim.walkAnimSpeed);
            }
            else if (Vector3.Distance(targetTransform.position, transform.position) < 2f && Vector3.Distance(targetTransform.position, transform.position) > 1f)
            {
                MoveToTarget(_speed: MoveSpeed, _targetPosition: navMeshAgent.transform.position);

                //Animacion
                zombieAnim.ApplyAnim(zombieAnim.AttackAnimHash, true);
            }
            else
            {
                MoveToTarget(_speed: MoveSpeed, _targetPosition: -targetTransform.position);

                //Animacion
                zombieAnim.ApplyAnim(zombieAnim.WalkAnimHash, true, -zombieAnim.walkAnimSpeed);
            }
        }
        else if (targetTransform == null)
        {
            navMeshAgent.destination = navMeshAgent.transform.position;
        }
    }
    private void SetZombieRotation(Transform _targetTransform, float _multiplier)
    {
        Quaternion targetRotation = Quaternion.LookRotation(_targetTransform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * _multiplier);
    }
    private void MoveToTarget(float _speed, Vector3 _targetPosition)
    {
        navMeshAgent.speed = _speed;
        navMeshAgent.destination = _targetPosition;

        SetZombieRotation(targetTransform, 10);
    }
}
