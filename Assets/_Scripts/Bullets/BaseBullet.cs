using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float timer = 0f;
    private Rigidbody2D rb;

    private float _bulletLife = 1f;
    private float _speed = 1f;
    private float _damageAmount = 5f;

    //getters for bullet traits
    public float BulletLife
    {
        get { return _bulletLife; }
        set { _bulletLife = value; }
    }

    public float Speed
    {
        get { return _speed; }
        set { _speed = value; }
    }

    public float DamageAmount
    {
        get { return _damageAmount; }
        set { _damageAmount = value; }
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