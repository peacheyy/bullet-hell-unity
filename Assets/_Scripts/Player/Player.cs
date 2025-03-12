using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Stats")]
    [SerializeField] float _maxHealth = 6f;

    public event Action<float, float> OnHealthChanged; // current, max

    public bool isInvulnerable { get; private set; } = false;

    private float _currentHealth;

    void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if(isInvulnerable) { return; }
        _currentHealth -= damage;

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

    public float GetMaxHealth()
    {
        return _maxHealth;
    }

    public void SetInvulnerable(bool invulnerable)
    {
        isInvulnerable = invulnerable;
    }
}