using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using System;

[System.Serializable]
public class Cell
{
    public GameObject UICell;
    public Image UIImages;
    public Text TimerText;
}
public class GiveBonusAbility : MonoBehaviour
{
    private List<Action> actions = new List<Action>();
    public Cell[] cells = new Cell[4];

    private ApplyPlayerState playerState;
    private ApplyPlayerAnimDirection playerAnimDirection;
    public List<ApplyShoot> applyShoot = new List<ApplyShoot>();
    private ShootAbility shootAbility;
    private int index = 0;

    private void Start()
    {
        playerState = GameObject.Find("GameManager").GetComponent<ApplyPlayerState>();
        playerAnimDirection = GameObject.FindObjectOfType<ApplyPlayerAnimDirection>();
        shootAbility = GameObject.FindObjectOfType<ShootAbility>();

        actions.Add(DefenseBonus);
        actions.Add(SpeedBonus);
        actions.Add(DamageBonus);
        actions.Add(WeaponBonus);
    }
    public void GiveBonus(int _bonusIndex, Sprite _itemSprite, float _duration)
    {
        foreach (var cell in cells)
        {
            if (cell.UIImages.sprite == null)
            {
                cell.UIImages.sprite = _itemSprite;
                cell.UIImages.SetNativeSize();
                cell.UICell.SetActive(true);
                Timer(_bonusIndex, cell.TimerText, _duration, index);

                actions[_bonusIndex].Invoke();
                index = 0;
                return;
            }
            index++;
        }
    }


    private async void Timer(int _bonusIndex, Text _timerText, float _remainingTime, int _cellIndex)
    {
        while (true)
        {
            if (_remainingTime > 0)
            {
                _remainingTime -= Time.deltaTime;
            }
            else if (_remainingTime < 0)
            {
                _remainingTime = 0;
                actions[_bonusIndex].Invoke();
                ResetCell(_cellIndex);
                return;
            }
            int minutes = Mathf.FloorToInt(_remainingTime / 60);
            int seconds = Mathf.FloorToInt(_remainingTime % 60);
            _timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

            await Task.Delay(1);
        }
    }
    private void ResetCell(int _index)
    {
        cells[_index].UICell.SetActive(false);
        cells[_index].UIImages.sprite = null;
    }

    //Actions
    private void DefenseBonus()
    {
        playerState.isReduce = !playerState.isReduce;
    }
    private void SpeedBonus()
    {
        playerAnimDirection.isBonus = !playerAnimDirection.isBonus;
    }
    private void DamageBonus()
    {
        foreach (var shoot in applyShoot)
        {
            shoot.isBonusBulletDamage = !shoot.isBonusBulletDamage;
        }
    }
    private void WeaponBonus()
    {
        shootAbility.isBonusShootDelay = !shootAbility.isBonusShootDelay;
    }
}
