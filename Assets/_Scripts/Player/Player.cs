using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Stats")]
    [SerializeField] float maxHealth = 100f;

    public HealthBar healthBar;

    private float _currentHealth;

    void Start()
    {
        _currentHealth = maxHealth;
        healthBar.SetMaxHealth((int)maxHealth);
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;

        healthBar.SetHealth((int)_currentHealth);

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player died!");
    }

    public float GetHealth()
    {
        return _currentHealth;
    }
}