using UnityEngine;

public class EnemyBullet : Bullet
{
    [SerializeField] private float bulletLifetime = 10f;
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private float bulletDamage = 10f;

    protected override void Start()
    {
        Initialize(bulletLifetime, bulletSpeed, bulletDamage);
        base.Start();
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
                player.TakeDamage(DamageAmount);
                Destroy(gameObject);  // Destroy bullet after hitting player
            }
        }
    }
}
