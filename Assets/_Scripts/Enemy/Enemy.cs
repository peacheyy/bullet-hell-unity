using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] float maxHealth = 25f;
    [SerializeField] bool isBoss = false;
    [SerializeField] float knockbackRecoverySpeed = 5f; // How quickly enemy recovers from knockback

    private float _currentHealth;
    private Vector2 knockbackVelocity = Vector2.zero;
    private bool isKnockedBack = false;

    public static event System.Action<Enemy> OnEnemyDeath;

    void Start()
    {
        _currentHealth = maxHealth;
    }

    void Update()
    {
        // Apply knockback movement
        if (isKnockedBack)
        {
            // Move based on current knockback velocity
            transform.position += (Vector3)knockbackVelocity * Time.deltaTime;

            // Reduce knockback velocity over time
            knockbackVelocity = Vector2.Lerp(knockbackVelocity, Vector2.zero, knockbackRecoverySpeed * Time.deltaTime);

            // Stop knockback when velocity is very small
            if (knockbackVelocity.magnitude < 0.1f)
            {
                isKnockedBack = false;
                knockbackVelocity = Vector2.zero;
            }
        }
    }

    // Add this method to your Enemy class
    public void ApplyKnockback(Vector2 direction, float force)
    {
        knockbackVelocity = direction.normalized * force;
        isKnockedBack = true;
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