using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ApplyZombieMove : MonoBehaviour, IBehaviour
{
    [HideInInspector] public Transform targetTransform;
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
        if (targetTransform != null)
        {
            if (Vector3.Distance(targetTransform.position, transform.position) > 2f)
            {
                navMeshAgent.destination = targetTransform.position;

                SetZombieRotation(targetTransform, 10);
                zombieAnim.ApplyAnim(zombieAnim.WalkAnimHash, true);
            }
            else
            {
                navMeshAgent.destination = navMeshAgent.transform.position;

                SetZombieRotation(targetTransform, 10);
                zombieAnim.ApplyAnim(zombieAnim.WalkAnimHash, false);
                zombieAnim.ApplyAnim(zombieAnim.AttackAnimHash, true);
            }
        }
        else
        {
            navMeshAgent.destination = navMeshAgent.transform.position;
        }
    }
    private void SetZombieRotation(Transform _targetTransform, float _multiplier)
    {
        Quaternion targetRotation = Quaternion.LookRotation(_targetTransform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * _multiplier);
    }
}
