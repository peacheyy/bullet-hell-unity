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

            if (enemy != null)
            {
                enemy.TakeDamage(DamageAmount);

                // Apply knockback using our custom method
                float knockbackForce = Speed * knockbackMultiplier;
                enemy.ApplyKnockback(transform.up, knockbackForce);

                ReturnToPool();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ReturnToPool(); 
    }
}