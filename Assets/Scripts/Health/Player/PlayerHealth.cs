using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using Photon.Pun;

public class PlayerHealth : MonoBehaviour
{
    public int _maxHealth = 10;
    public int _currenthealth;

    private GiveBonusAbility giveBonusAbility;
    private GameManager gameManager;
    private HealthBar healthBar;
    private NetworkManager networkManager;
    private bool startDeath = false;

    private void Start()
    {
        giveBonusAbility = GameObject.FindObjectOfType<GiveBonusAbility>();
        gameManager = GameObject.FindObjectOfType<GameManager>();
        healthBar = FindObjectOfType<HealthBar>();
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            networkManager = GameObject.FindObjectOfType<NetworkManager>();
        }

        _currenthealth = _maxHealth;
        healthBar.SetMaxHealth(_maxHealth);

        //aimWeapon = GetComponent<ApplyAimWeapon>();
    }
    public int Value()
    {
        return _maxHealth / 17;
    }
    public int Health
    {
        get => _currenthealth;
        set
        {
            if (_currenthealth == value) return;
            _currenthealth = value;
            if (_currenthealth <= 0)
            {
                Die();
            }
        }
    }

    public async void Die()
    {
        if (!startDeath)
        {
            startDeath = true;

            await Task.Delay(100);
            gameManager.GiveCoin(100);
            giveBonusAbility.UpdateBonusesPanel(100);
            if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                PlayerMultiplayerData.Instance.RemovePlayerTransform(transform);
                networkManager.StartDisconnected();
                Destroy(this.gameObject);
                return;
            }
            Time.timeScale = 0f;
        }
    }

    public void TakeDamage(int damage)
    {
        if (SceneManager.GetActiveScene().buildIndex == 2 && PhotonView.Get(this.gameObject).IsMine)
        {
            _currenthealth -= damage;
            healthBar.SetHealth(_currenthealth);

            if (_currenthealth <= 0) Die();
        }
        else if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            _currenthealth -= damage;
            healthBar.SetHealth(_currenthealth);

            if (_currenthealth <= 0) Die();
        }
    }

    public void GiveBenefit(int benefit)
    {
        if ((_currenthealth + benefit) > _maxHealth)
        {
            _currenthealth = _maxHealth;
            healthBar.SetHealth(_currenthealth);
            return;
        }
        _currenthealth += benefit;
        healthBar.SetHealth(_currenthealth);
    }
}
