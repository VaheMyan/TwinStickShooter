using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public enum ItemType
{
    Defalut = -1,
    DefenseBonus = 0,
    SpeedBonus = 1,
    DamageBonus = 2,
    WeaponBonus = 3
}

public class GiveItemPickUpAbility : MonoBehaviour
{
    public bool isHealth;
    public int BonusHealth;
    public ItemType itemType;
    public float BonusDuration;
    public Sprite _ItemSprite;
    public Sprite ItemSprite => _ItemSprite;

    private ApplyPlayerState playerState;
    private GiveBonusAbility giveBonusAbility;

    private void Start()
    {
        if (isHealth)
        {
            playerState = FindObjectOfType<ApplyPlayerState>();
            return;
        }
        giveBonusAbility = GameObject.FindObjectOfType<GiveBonusAbility>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (isHealth)
            {
                GiveBenefit();
                Destroy(this.gameObject);
                return;
            }

            GiveBonus();
            Destroy(this.gameObject);
            return;
        }
    }
    private void GiveBonus()
    {
        giveBonusAbility.GiveBonus(((int)itemType), ItemSprite, BonusDuration);
    }
    private void GiveBenefit()
    {
        playerState.GiveBenefit(BonusHealth);
    }
}