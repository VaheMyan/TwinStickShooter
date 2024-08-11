using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyPlayerDamage : MonoBehaviour
{
    public int _damage;
    private ApplyPlayerState playerState;

    private void Start()
    {
        playerState = GameObject.FindObjectOfType<ApplyPlayerState>();
    }

    public void TakeDamage()
    {
        playerState.TakeDamage(_damage);
    }
}
