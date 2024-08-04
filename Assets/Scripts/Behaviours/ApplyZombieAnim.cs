using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyZombieAnim : MonoBehaviour
{
    public string IdleAnimHash;
    public string WalkAnimHash;
    public string AttackAnimHash;
    public string GetDamageAnimHash;
    public string DeathAnimHash;

    private Animator animator;
    private List<string> anims = new List<string>();

    private void Start()
    {
        animator = GetComponent<Animator>();

        //Adding anim in List to check them out
        anims.Add(IdleAnimHash);
        anims.Add(WalkAnimHash);
        anims.Add(AttackAnimHash);
        anims.Add(GetDamageAnimHash);
        anims.Add(DeathAnimHash);
    }

    public void ApplyAnim(string animType, bool value) // Perform anim turn on
    {
        animator.SetBool(animType, value);
        ChackOtherAnims(animType);
    }
    public void ApplyAnim(string animType)
    {
        animator.SetTrigger(animType);
    }

    public void ChackOtherAnims(string applyedAnim) //Chacking anims
    {
        foreach (var anim in anims)
        {
            if (anim != applyedAnim)
            {
                animator.SetBool(anim, false);
            }
        }
    }
}
