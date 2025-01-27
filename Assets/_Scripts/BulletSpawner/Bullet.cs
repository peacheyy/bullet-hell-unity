using UnityEngine;

public class Bullet : MonoBehaviour
{
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

    private float timer = 0f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Set velocity based on the bullet's up direction
        rb.velocity = transform.up * Speed;
    }

    void Update()
    {
        //checks whether the fired bullet's life exceeds the timer
        if (timer > BulletLife)
        {
            Destroy(gameObject);
            return;
        }
        timer += Time.deltaTime;
    }

    //collision detection when bullet encounters a rigidbody
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(DamageAmount);  // You need to add a damageAmount field
                Destroy(gameObject);  // Destroy bullet after hitting player
            }
        }
    }
}