using UnityEngine;
using Unity.Entities;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour, IConvertGameObjectToEntity
{
    public int _maxHealth = 10;
    public int _currenthealth;

    private HealthBar healthBar;
    private Entity _entity;
    private EntityManager _dsManager;

    private void Start()
    {
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
            await Task.Delay(410);
            _dsManager.DestroyEntity(_entity);
            await Task.Delay(410);

            SceneManager.LoadScene(0);
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
        _currenthealth += benefit;
    }

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        _entity = entity;
        _dsManager = dstManager;
    }
}
