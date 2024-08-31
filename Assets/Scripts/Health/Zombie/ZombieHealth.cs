using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class ZombieHealth : MonoBehaviour
{
    public int _maxHealth = 10;
    public int _currenthealth;
    public int givePlayerScore;
    public ZombieHealthBar healthBar;
    public ApplyZombieAnim zombieAnim;
    public ApplyZombieMove zombieMove;
    public GameObject Canvas;

    private GiveBonusAbility giveBonusAbility;
    private GameManager gameManager;
    private bool isDeathing = false;

    private void Start()
    {
        _currenthealth = _maxHealth;
        healthBar.SetMaxHealthZombie(_maxHealth);

        giveBonusAbility = GameObject.FindObjectOfType<GiveBonusAbility>();
        gameManager = GameObject.FindObjectOfType<GameManager>();

        if (giveBonusAbility != null)
        {
            giveBonusAbility.CreateKilledZombieCell(gameObject.tag);
        }
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
                //Die();
            }
        }
    }
    public async void Die()
    {
        if (isDeathing == false)
        {
            isDeathing = true;

            zombieMove.targetTransform = null;

            zombieAnim.ApplyAnim(zombieAnim.DeathAnimHash, true);
            await Task.Delay(100);

            Canvas.SetActive(false);
            await Task.Delay(100);

            giveBonusAbility.GivePlayerScore(givePlayerScore);
            giveBonusAbility.UpdateKilledZombiesCout(gameObject.tag);
            await Task.Delay(1000);
            gameManager.InstantiatePotion(transform.position);
            if (this != null && SceneManager.GetActiveScene().buildIndex == 2) PhotonNetwork.Destroy(gameObject);
            else if (this != null && SceneManager.GetActiveScene().buildIndex == 1) Destroy(gameObject);
        }
    }

    public void TakeDamageZombie(int damage)
    {
        _currenthealth -= damage;
        healthBar.SetHealthZombie(_currenthealth);

        if (_currenthealth <= 0) Die();
    }

    public void GiveBenefitZombie(int benefit)
    {
        _currenthealth += benefit;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // Send the current health to other players
            stream.SendNext(_currenthealth);
        }
        else
        {
            // Receive the current health from other players
            _currenthealth = (int)stream.ReceiveNext();
            healthBar.SetHealthZombie(_currenthealth);
        }
    }
}
