using UnityEngine;

public class PlayerBullet : Bullet
{
    [SerializeField] private float bulletLifetime = 10f;
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private float bulletDamage = 10f;
    [SerializeField] private float knockbackMultiplier = 2f;

    // OnEnable gets called everytime a GameObject is enabled (in this case, when a bullet is fired)
    protected override void OnEnable()  
    {
        Initialize(bulletLifetime, bulletSpeed, bulletDamage);
        base.OnEnable();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            Rigidbody2D enemyRb = collision.gameObject.GetComponent<Rigidbody2D>();

            if (enemy != null)
            {
                enemy.TakeDamage(DamageAmount);

                if (enemyRb != null)
                {
                    float knockbackForce = Speed * knockbackMultiplier;
                    enemyRb.AddForce(transform.up * knockbackForce, ForceMode2D.Impulse);
                }

                ReturnToPool(); 
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ReturnToPool(); 
    }
}