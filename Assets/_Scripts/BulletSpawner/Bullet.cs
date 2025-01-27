using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletLife = 1f;
    public float speed = 1f;
    public float damageAmount = 5f;

    private float timer = 0f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Set velocity based on the bullet's up direction
        rb.velocity = transform.up * speed;
    }

    void Update()
    {
        //checks whether the fired bullet's life exceeds the timer
        if (timer > bulletLife)
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
                player.TakeDamage(damageAmount);  // You need to add a damageAmount field
                Destroy(gameObject);  // Destroy bullet after hitting player
            }
        }
    }
}