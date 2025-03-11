using System.Collections;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] string bulletPoolKey = "EnemyBullet";
    [SerializeField] float fireCooldown;

    private bool canFire = true;

    private void Start()
    {
        // Validate that the pool exists
        if (BulletPoolManager.Instance == null)
        {
            Debug.LogError("No BulletPoolManager found in scene!");
            enabled = false;
            return;
        }
    }

    private void Update()
    {
        if (canFire)
        {
            canFire = false;
            StartCoroutine(Fire());
        }
    }

    private IEnumerator Fire()
    {
        // Charging up shot
        // Wait for the cooldown before allowing another fire 
        yield return new WaitForSeconds(fireCooldown);

        // instead of creating a new bullet, this grabs from the bullets in the pool queue
        Bullet bullet = BulletPoolManager.Instance.GetBullet(
            bulletPoolKey,
            firePoint.position,
            firePoint.rotation
        );

        if (bullet != null)
        {
            // Initialize bullet properties
            bullet.Initialize(10f, 5f, 10f);
        }

        canFire = true;
    }
}