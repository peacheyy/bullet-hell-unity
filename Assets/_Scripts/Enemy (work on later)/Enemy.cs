using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] float maxHealth = 25f;

    private float _currentHealth;
    public static event System.Action<Enemy> OnEnemyDeath;

    void Start()
    {
        _currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Enemy died!");

        OnEnemyDeath?.Invoke(this);

        Destroy(gameObject);
    }

    public float GetHealth()
    {
        return _currentHealth;
    }
}