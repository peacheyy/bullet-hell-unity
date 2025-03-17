using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float timer = 0f;
    protected Rigidbody2D rb;
    private string poolKey;

    // Properties with private setters for better encapsulation
    public float BulletLife { get; private set; } = 1f;
    public float Speed { get; private set; } = 1f;
    public float DamageAmount { get; private set; } = 5f;

    public void InitializePool(string key)
    {
        poolKey = key;
    }

    // Initialization method to set up the bullet's properties
    public void Initialize(float lifetime, float speed, float damage)
    {
        BulletLife = lifetime;
        Speed = speed;
        DamageAmount = damage;
        timer = 0f;
    }

    protected virtual void Awake()
    {
        // Get the Rigidbody2D component in Awake
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError($"No Rigidbody2D found on bullet {gameObject.name}!");
        }
    }

    protected virtual void OnEnable()
    {
        if (rb != null)
        {
            InitMovement();
        }
        else
        {
            Debug.LogError($"Cannot initialize movement: Rigidbody2D is null on bullet {gameObject.name}!");
        }
    }

    protected virtual void Update()
    {
        HandleLifetime();
    }

    // Set velocity based on the bullet's up direction
    protected virtual void InitMovement()
    {
        rb.linearVelocity = transform.up * Speed;
    }

    // Checks whether the fired bullet's life exceeds the timer
    protected virtual void HandleLifetime()
    {
        if (timer > BulletLife)
        {
            ReturnToPool();
            return;
        }
        timer += Time.deltaTime;
    }

    protected virtual void ReturnToPool()
    {
        if (string.IsNullOrEmpty(poolKey))
        {
            Debug.LogError($"Bullet {gameObject.name} has no pool key!");
            Destroy(gameObject);
            return;
        }

        if (BulletPoolManager.Instance != null)
        {
            BulletPoolManager.Instance.ReturnBullet(poolKey, this);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}