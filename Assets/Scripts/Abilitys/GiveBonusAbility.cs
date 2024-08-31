using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using System;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Cell
{
    public GameObject UICell;
    public Image UIImages;
    public Text TimerText;
}
[System.Serializable]
public class KilledZombie
{
    public string ZombieTag;
    public int KilledZombieCout;
}
[System.Serializable]
public class BonusesText
{
    public string TextType;
    public Text Cout;
}
public class GiveBonusAbility : MonoBehaviour
{
    public int currentScore = 0;
    public Text scoreText;

    private List<Action> actions = new List<Action>();
    public Cell[] cells = new Cell[4];
    public List<KilledZombie> killedZombies = new List<KilledZombie>();

    public BonusesText[] BonusesTexts = new BonusesText[6];
    public GameObject DeathBonusesPanel;

    public Sprite[] ItemsSprites = new Sprite[4];

    private ApplyPlayerState playerState;
    private ApplyPlayerAmmo playerAmmo;
    private ApplyPlayerAnimDirection playerAnimDirection;
    public List<ApplyShoot> applyShoot = new List<ApplyShoot>();
    private ShootAbility shootAbility;
    private int index = 0;

    private void Start()
    {
        playerState = GameObject.Find("GameManager").GetComponent<ApplyPlayerState>();

        actions.Add(DefenseBonus);
        actions.Add(SpeedBonus);
        actions.Add(DamageBonus);
        actions.Add(WeaponBonus);

        //Multiplayer
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            FindPlayer();
            return;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            playerAmmo = GameObject.FindObjectOfType<ApplyPlayerAmmo>();
            playerAnimDirection = GameObject.FindObjectOfType<ApplyPlayerAnimDirection>();
            shootAbility = GameObject.FindObjectOfType<ShootAbility>();

            //Score
            GivePlayerScore(1);
        }
    }

    //Update Bonuses Panel
    public void UpdateBonusesPanel(int _coins)
    {
        if (playerAmmo == null) return;

        DeathBonusesPanel.SetActive(true);

        BonusesTexts[0].Cout.text = currentScore.ToString();
        for (int i = 0; i < BonusesTexts.Length; i++)
        {
            foreach (var killedZombie in killedZombies)
            {
                if (killedZombie.ZombieTag == BonusesTexts[i].TextType)
                {
                    BonusesTexts[i].Cout.text = killedZombie.KilledZombieCout.ToString();
                }
            }
        }
        BonusesTexts[4].Cout.text = _coins.ToString();
        BonusesTexts[5].Cout.text = playerAmmo.spentBullets.ToString();

        scoreText = null;
    }
    //KilldeZombie
    public void UpdateKilledZombiesCout(string _zombieTag)
    {
        foreach (var killedZombie in killedZombies)
        {
            if (killedZombie.ZombieTag == _zombieTag)
            {
                killedZombie.KilledZombieCout++;
            }
        }
    }
    public void CreateKilledZombieCell(string _zombieTag)
    {
        foreach (var killedZombie in killedZombies)
        {
            if (killedZombie.ZombieTag == _zombieTag)
            {
                return;
            }
        }
        killedZombies.Add(new KilledZombie { ZombieTag = _zombieTag, KilledZombieCout = 0 });
    }

    //Score
    public async void GivePlayerScore(int score)
    {
        if (scoreText == null) return;
        currentScore += score * (int)Time.timeScale;
        scoreText.text = currentScore.ToString();

        await Task.Delay(500);
        GivePlayerScore(1);
    }

    //Give Bonuses
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
    public void GiveBonus(int _bonusIndex, float _duration)
    {
        foreach (var cell in cells)
        {
            if (cell.UIImages.sprite == null)
            {
                cell.UIImages.sprite = ItemsSprites[_bonusIndex];
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

    private async void FindPlayer()
    {
        GameObject Player = null;
        while (Player == null)
        {
            await Task.Delay(500);
            Player = PlayerMultiplayerData.Instance.Player;
        }

        playerAmmo = Player.GetComponent<ApplyPlayerAmmo>();
        playerAnimDirection = Player.GetComponent<ApplyPlayerAnimDirection>();
        shootAbility = Player.GetComponent<ShootAbility>();

        //Score
        GivePlayerScore(1);
        return;
    }
}
