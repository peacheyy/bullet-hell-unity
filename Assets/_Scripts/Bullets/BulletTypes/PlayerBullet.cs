using UnityEngine;
using System.Collections;

public class PlayerBullet : Bullet
{
    [SerializeField] private float bulletLifetime = 10f;
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private float bulletDamage = 10f;
    [SerializeField] private float knockbackMultiplier = 2f;

    [SerializeField] Sprite bulletImpact;
    [SerializeField] private float impactDisplayTime = 0.1f; // How long to show impact sprite

    private SpriteRenderer sr;
    private bool isImpacting = false;


    // OnEnable gets called everytime a GameObject is enabled (in this case, when a bullet is fired)
    protected override void OnEnable()
    {
        Initialize(bulletLifetime, bulletSpeed, bulletDamage);

        // Get sprite renderer reference once
        if (sr == null)
        {
            sr = GetComponent<SpriteRenderer>();
        }

        // Reset impact state
        isImpacting = false;

        base.OnEnable();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !isImpacting)
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();

            if (enemy != null)
            {
                // Mark as impacting to prevent multiple triggers
                isImpacting = true;

                // Stop the bullet movement using the parent class rb
                if (rb != null)
                {
                    rb.linearVelocity = Vector2.zero;
                }

                // Change sprite
                if (sr == null)
                {
                    sr = GetComponent<SpriteRenderer>();
                }
                sr.sprite = bulletImpact;

                // Apply damage
                enemy.TakeDamage(DamageAmount);

                // Apply knockback
                float knockbackForce = Speed * knockbackMultiplier;
                enemy.ApplyKnockback(transform.up, knockbackForce);

                // Start impact effect coroutine
                StartCoroutine(ImpactEffect());
            }
        }
    }

    private IEnumerator ImpactEffect()
    {
        // Wait for the impact display time
        yield return new WaitForSeconds(impactDisplayTime);

        // Return to pool after showing impact sprite
        ReturnToPool();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isImpacting)
        {
            // For non-enemy collisions, just return to pool
            ReturnToPool();
        }
    }
}