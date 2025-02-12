using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] string bulletPoolKey = "PlayerBullet"; // necessary so the bullets fired have a key value
    
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

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }

    public void Fire()
    {
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
    }
}