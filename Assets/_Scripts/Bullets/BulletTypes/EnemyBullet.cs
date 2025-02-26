using UnityEngine;

public class EnemyBullet : Bullet
{
    [SerializeField] private float bulletLifetime = 10f;
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private float bulletDamage = 10f;

    // OnEnable gets called everytime a GameObject is enabled (in this case, when a bullet is fired)
    protected override void OnEnable() 
    {
        Initialize(bulletLifetime, bulletSpeed, bulletDamage);
        base.OnEnable();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ReturnToPool();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(DamageAmount);
                ReturnToPool();
            }
        }
    }
}


