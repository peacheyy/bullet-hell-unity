using UnityEngine;

public class CirclePattern : IBulletPattern
{
    private const int BULLET_COUNT = 12;
    private const float SPAWN_INTERVAL = 1f;
    private float _timer;
    private readonly string _bulletPoolKey;

    public CirclePattern(string bulletPoolKey)
    {
        _bulletPoolKey = bulletPoolKey;
    }

    public void Execute(Transform spawner, Transform[] firePoints)
    {
        _timer += Time.deltaTime;

        if (_timer >= SPAWN_INTERVAL)
        {
            for (int i = 0; i < BULLET_COUNT; i++)
            {
                float angle = i * (360f / BULLET_COUNT);
                Quaternion rotation = Quaternion.Euler(0, 0, angle);
                
                // instead of creating a new bullet, this grabs from the bullets in the pool queue
                Bullet bullet = BulletPoolManager.Instance.GetBullet(
                    _bulletPoolKey,
                    spawner.position,
                    rotation
                );
                
                bullet.Initialize(5f, 8f, 10f);
            }
            _timer = 0f;
        }
    }

    public void Reset()
    {
        _timer = 0f;
    }
}
