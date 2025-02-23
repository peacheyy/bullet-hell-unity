using UnityEngine;

public class SpiralPattern : IBulletPattern
{
    private readonly float _rotation = 720f;
    private const float BULLET_SPAWN_INTERVAL = 0.1f;
    private const float MAX_TIME = 1f;
    private const float MAX_ROTATION_TIME = 1f;

    private float _timer;
    private float _bulletSpawnTimer;
    private readonly string _bulletPoolKey;

    public bool IsComplete { get; private set; }

    public SpiralPattern(string bulletPoolKey)
    {
        _bulletPoolKey = bulletPoolKey;
    }

    public void Execute(Transform spawner, Transform[] firePoints)
    {
        _timer += Time.deltaTime;

        IsComplete = false;

        if (_timer >= MAX_TIME && _timer <= MAX_TIME + MAX_ROTATION_TIME)
        {

            spawner.Rotate(0, 0, _rotation * Time.deltaTime);

            _bulletSpawnTimer += Time.deltaTime;
            if (_bulletSpawnTimer >= BULLET_SPAWN_INTERVAL)
            {
                foreach (Transform firePoint in firePoints)
                {
                    // instead of creating a new bullet, this grabs from the bullets in the pool queue
                    Bullet bullet = BulletPoolManager.Instance.GetBullet(
                        _bulletPoolKey,
                        firePoint.position,
                        firePoint.rotation
                    );
                    
                    bullet.Initialize(5f, 8f, 10f);
                }
                _bulletSpawnTimer = 0f;
            }
        }

        if (_timer >= MAX_TIME + MAX_ROTATION_TIME)
        {
            spawner.rotation = Quaternion.Euler(0, 0, 0);
            IsComplete = true;
        }
    }

    public void Reset()
    {   
        _timer = 0f;
        _bulletSpawnTimer = 0f;
    }
}