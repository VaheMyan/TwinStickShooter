using UnityEngine;
using Unity.Entities;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class ZombieHealth : MonoBehaviour, IConvertGameObjectToEntity
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
    private Entity _entity;
    private EntityManager _dsManager;
    private bool isDeathing = false;

    private void Start()
    {
        _currenthealth = _maxHealth;
        healthBar.SetMaxHealthZombie(_maxHealth);

        giveBonusAbility = GameObject.FindObjectOfType<GiveBonusAbility>();
        gameManager = GameObject.FindObjectOfType<GameManager>();

        giveBonusAbility.CreateKilledZombieCell(gameObject.tag);
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
        if (_entity != Entity.Null && _dsManager != null && isDeathing == false)
        {
            isDeathing = true;

            zombieMove.targetTransform = null;

            zombieAnim.ApplyAnim(zombieAnim.DeathAnimHash);
            await Task.Delay(100);

            Canvas.SetActive(false);
            await Task.Delay(100);

            giveBonusAbility.GivePlayerScore(givePlayerScore);
            giveBonusAbility.UpdateKilledZombiesCout(gameObject.tag);
            _dsManager.DestroyEntity(_entity);
            await Task.Delay(1000);
            gameManager.InstantiatePotion(transform.position);
            if (this != null) Destroy(gameObject);
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

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        _entity = entity;
        _dsManager = dstManager;
    }
}
