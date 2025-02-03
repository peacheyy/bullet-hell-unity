using UnityEngine;

public class RotateBulletSpawner : MonoBehaviour
{
    [SerializeField] Transform bulletSpawner;
    [SerializeField] SpawnBullet spawner;

    private float degrees = 360f;
    private float timer = 0f;
    private float maxTime = 3f;
    private float maxRotationTime = 1f;

    // Add bullet spawn timing control
    private float bulletSpawnTimer = 0f;
    [SerializeField] private float bulletSpawnInterval = 0.1f;  // Adjust in Inspector

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= maxTime && timer <= maxTime + maxRotationTime)
        {
            transform.Rotate(0, 0, degrees * Time.deltaTime);

            // Control bullet spawning frequency during rotation
            bulletSpawnTimer += Time.deltaTime;
            if (bulletSpawnTimer >= bulletSpawnInterval)
            {
                spawner.getBullet();
                bulletSpawnTimer = 0f;
            }
        }

        if (timer >= maxTime + maxRotationTime)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            timer = 0f;
            bulletSpawnTimer = 0f;  // Reset bullet timer too
        }
    }
}