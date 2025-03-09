using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] float maxHealth = 25f;
    [SerializeField] bool isBoss = false;

    //public HealthBar healthBar;

    private float _currentHealth;
    public static event System.Action<Enemy> OnEnemyDeath;

    void Start()
    {
        _currentHealth = maxHealth;
        //healthBar.SetMaxHealth((int)maxHealth);
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        
        //healthBar.SetHealth((int)_currentHealth);

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        OnEnemyDeath?.Invoke(this);

        if (isBoss)
        {
            // Signal level completion directly
            LevelManager.Instance.BossDefeated();
        }

        Destroy(gameObject);
    }

    public float GetHealth()
    {
        return _currentHealth;
    }
}