using UnityEngine;
using Unity.Entities;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour, IConvertGameObjectToEntity
{
    public int _maxHealth = 10;
    public int _currenthealth;

    private GiveBonusAbility giveBonusAbility;
    private GameManager gameManager;
    private HealthBar healthBar;
    private Entity _entity;
    private EntityManager _dsManager;

    private void Start()
    {
        giveBonusAbility = GameObject.FindObjectOfType<GiveBonusAbility>();
        gameManager = GameObject.FindObjectOfType<GameManager>();
        //deathBonusesPanel = GameObject.FindGameObjectWithTag("BonusesPanel");
        healthBar = FindObjectOfType<HealthBar>();

        _currenthealth = _maxHealth;
        healthBar.SetMaxHealth(_maxHealth);

        //aimWeapon = GetComponent<ApplyAimWeapon>();
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
        if (_entity != Entity.Null && _dsManager != null)
        {
            await Task.Delay(100);
            gameManager.GiveCoin(100);
            giveBonusAbility.UpdateBonusesPanel(100);
            Time.timeScale = 0f;
        }
        else
        {
            Debug.LogWarning("Entity or EntityManager is not set properly.");
        }
    }

    public void TakeDamage(int damage)
    {
        _currenthealth -= damage;
        healthBar.SetHealth(_currenthealth);

        if (_currenthealth <= 0) Die();
    }

    public void GiveBenefit(int benefit)
    {
        if ((_currenthealth + benefit) > _maxHealth)
        {
            _currenthealth = _maxHealth;
            return;
        }
        _currenthealth += benefit;
    }

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        _entity = entity;
        _dsManager = dstManager;
    }
}
