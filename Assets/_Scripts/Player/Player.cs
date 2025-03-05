using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Stats")]
    [SerializeField] float maxHealth = 100f;

    public HealthBar healthBar;
    public bool isInvulnerable { get; private set; } = false;

    private float _currentHealth;

    void Start()
    {
        _currentHealth = maxHealth;
        healthBar.SetMaxHealth((int)maxHealth);
    }

    public void TakeDamage(float damage)
    {
        if(isInvulnerable) { return; }
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

    public void SetInvulnerable(bool invulnerable)
    {
        isInvulnerable = invulnerable;
    }
}