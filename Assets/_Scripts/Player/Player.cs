using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Stats")]
    [SerializeField] private float maxHealth = 100f;

    private float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
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
        return currentHealth;
    }
}