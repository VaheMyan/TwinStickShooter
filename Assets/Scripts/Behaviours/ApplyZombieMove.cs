using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class ApplyZombieMove : MonoBehaviour, IBehaviour
{
    public bool isBoosted = false;
    public float MoveSpeed;
    public float AttackDistance = 2f;
    public float WalkkDistance = 12f;
    public Transform targetTransform;
    public List<Transform> TargetTransforms;
    public bool isAttack = false;

    [HideInInspector] public bool isDamaging = false;
    private NavMeshAgent navMeshAgent;
    private ApplyZombieAnim zombieAnim;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        zombieAnim = GetComponent<ApplyZombieAnim>();

        //Multiplayer
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            FindPlayer();
            return;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            targetTransform = GameObject.FindGameObjectWithTag("Player").transform;
            UpdateMove();
        }
    }
    private async void UpdateMove()
    {
        while (true)
        {
            if (this == null) return;
            if (transform == null) return;

            if (targetTransform == null && SceneManager.GetActiveScene().buildIndex == 2)
            {
                FindNearPlayer();
                await Task.Delay(50);
                if (targetTransform == null)
                {
                    StandStill();
                    return;
                }
            }
            else if (targetTransform == null && SceneManager.GetActiveScene().buildIndex == 1)
            {
                StandStill();
                return;
            }

            if (this == null) return;
            if (navMeshAgent == null) return;
            if (!navMeshAgent.isActiveAndEnabled) return;


            Behaviour();
            Evaluate();
            await Task.Delay(50);
        }
    }
    public float Evaluate()
    {
        if (targetTransform == null) return 0;
        return 1 / (this.gameObject.transform.position - targetTransform.position).magnitude;
    }
    public void Behaviour()
    {
        if (targetTransform == null) return;
        SetZombieRotation(targetTransform, 100);
        if (isBoosted)
        {
            if (Vector3.Distance(targetTransform.position, transform.position) > 9.8f && !isAttack)
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
        else if (Vector3.Distance(targetTransform.position, transform.position) < WalkkDistance || isDamaging)
        {
            isAttack = true;
        }
        if (isDamaging)
        {
            isAttack = true;
        }

        if (targetTransform != null && isAttack)
        {
            if (Vector3.Distance(targetTransform.position, transform.position) > AttackDistance)
            {
                //Walk
                MoveToTarget(_speed: MoveSpeed, _targetPosition: targetTransform.position);

                //Animacion
                zombieAnim.ApplyAnim(zombieAnim.WalkAnimHash, true);
                zombieAnim.ApplyAnim(zombieAnim.WalkAnimHash, true, zombieAnim.walkAnimSpeed);
            }
            else if (Vector3.Distance(targetTransform.position, transform.position) < AttackDistance + 1 && Vector3.Distance(targetTransform.position, transform.position) > (AttackDistance - 1))
            {
                //Attack
                MoveToTarget(_speed: MoveSpeed, _targetPosition: navMeshAgent.transform.position);

                //Animacion
                zombieAnim.ApplyAnim(zombieAnim.AttackAnimHash, true);
            }
            //else
            //{
            //    MoveToTarget(_speed: MoveSpeed, _targetPosition: -targetTransform.position);
            //    //Animacion
            //    zombieAnim.ApplyAnim(zombieAnim.WalkAnimHash, true, -zombieAnim.walkAnimSpeed);
            //}
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
        SetZombieRotation(targetTransform, 10);

        navMeshAgent.speed = _speed;
        navMeshAgent.destination = _targetPosition;
    }
    private async void FindNearPlayer()
    {
        await Task.Delay(10);

        TargetTransforms = PlayerMultiplayerData.Instance.PlayersTransforms;

        float nearPlayerDistance = 1000f;
        Transform nearPlayerTransform = null;
        if (TargetTransforms == null || this == null) return;
        foreach (var target in TargetTransforms)
        {
            if (target != null && Vector3.Distance(transform.position, target.position) < nearPlayerDistance)
            {
                nearPlayerDistance = Vector3.Distance(transform.position, target.position);
                nearPlayerTransform = target;
            }
        }
        targetTransform = nearPlayerTransform;
        UpdateMove();
    }

    private async void FindPlayer()
    {
        TargetTransforms = PlayerMultiplayerData.Instance.PlayersTransforms;
        //Multiplayer
        while (true)
        {
            await Task.Delay(2000);
            FindNearPlayer();
        }
    }
    private void StandStill()
    {
        if (navMeshAgent == null) return;
        if (!navMeshAgent.isActiveAndEnabled) return;

        navMeshAgent.destination = navMeshAgent.transform.position;
        zombieAnim.ApplyAnim(zombieAnim.IdleAnimHash, true);
    }
}
