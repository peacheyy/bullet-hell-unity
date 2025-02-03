using UnityEngine;

public class SpiralPattern : IBulletPattern
{
    private readonly float _rotation = 720f;
    private const float BULLET_SPAWN_INTERVAL = 0.1f;
    private const float MAX_TIME = 3f;
    private const float MAX_ROTATION_TIME = 1f;

    private float _timer;
    private float _bulletSpawnTimer;

    public void Execute(Transform spawner, Transform[] firePoints, GameObject bulletPrefab)
    {
        _timer += Time.deltaTime;

        if (_timer >= MAX_TIME && _timer <= MAX_TIME + MAX_ROTATION_TIME)
        {
            spawner.Rotate(0, 0, _rotation * Time.deltaTime);

            _bulletSpawnTimer += Time.deltaTime;
            if (_bulletSpawnTimer >= BULLET_SPAWN_INTERVAL)
            {
                // Spawn from all firePoints
                foreach (Transform firePoint in firePoints)
                {
                    GameObject.Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                }
                _bulletSpawnTimer = 0f;
            }
        }

        if (_timer >= MAX_TIME + MAX_ROTATION_TIME)
        {
            spawner.rotation = Quaternion.Euler(0, 0, 0);
            Reset();
        }
    }

    public void Reset()
    {
        _timer = 0f;
        _bulletSpawnTimer = 0f;
    }
}