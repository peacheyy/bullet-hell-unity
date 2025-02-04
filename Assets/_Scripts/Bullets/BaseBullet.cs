using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float timer = 0f;
    private Rigidbody2D rb;

    // Properties with private setters for better encapsulation
    public float BulletLife { get; private set; } = 1f;
    public float Speed { get; private set; } = 1f;
    public float DamageAmount { get; private set; } = 5f;

    // Initialization method to set up the bullet's properties
    public void Initialize(float lifetime, float speed, float damage)
    {
        BulletLife = lifetime;
        Speed = speed;
        DamageAmount = damage;
    }

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        InitMovement();
    }

    protected virtual void Update()
    {
        HandleLifetime();
    }

    // Set velocity based on the bullet's up direction
    protected virtual void InitMovement()
    {
        rb.velocity = transform.up * Speed;
    }

    //checks whether the fired bullet's life exceeds the timer
    protected virtual void HandleLifetime()
    {
        if (timer > BulletLife)
        {
            Destroy(gameObject);
            return;
        }
        timer += Time.deltaTime;
    }
}