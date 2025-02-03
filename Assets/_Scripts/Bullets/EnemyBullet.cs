using UnityEngine;

public class EnemyBullet : Bullet
{
    protected override void Start()
    {
        Speed = 2f;
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
