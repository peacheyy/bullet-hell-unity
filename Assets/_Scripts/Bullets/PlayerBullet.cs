using UnityEngine;

public class PlayerBullet : Bullet
{
    protected override void Start()
    {
        Speed = 5f;
        BulletLife = 10f;
        DamageAmount = 10f;
        base.Start();
    }
    //collision detection when bullet encounters a rigidbody
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            Rigidbody2D enemyRb = collision.gameObject.GetComponent<Rigidbody2D>();

            if (enemy != null)
            {
                // Apply damage
                enemy.TakeDamage(DamageAmount);

                // Apply knockback if they have a rigidbody
                if (enemyRb != null)
                {
                    float knockbackForce = Speed * 2f; // Adjust this multiplier as needed
                    enemyRb.AddForce(transform.up * knockbackForce, ForceMode2D.Impulse);
                }

                Destroy(gameObject);
            }
        }
    }
}
