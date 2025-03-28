using UnityEngine;

public class CirclePattern : IBulletPattern
{
    private const int BULLET_COUNT = 36;
    private const float BULLET_SPAWN_INTERVAL = 1f;

    private float _timer;
    private readonly string _bulletPoolKey;

    public bool IsComplete { get; private set; }

    public CirclePattern(string bulletPoolKey)
    {
        _bulletPoolKey = bulletPoolKey;
    }

    public void Execute(Transform spawner, Transform[] firePoints)
    {
        _timer += Time.deltaTime;
        IsComplete = false;

        // Check if it's time to fire bullets and we haven't fired yet
        if (_timer >= BULLET_SPAWN_INTERVAL)
        {
            // Fire the bullets in a circle
            for (int i = 0; i < BULLET_COUNT; i++)
            {
                float angle = i * (360f / BULLET_COUNT);
                Quaternion rotation = Quaternion.Euler(0, 0, angle);

                Bullet bullet = BulletPoolManager.Instance.GetBullet(
                    _bulletPoolKey,
                    spawner.position,
                    rotation
                );

                bullet.Initialize(5f, 8f, 10f);
            }
            IsComplete = true;
        }

    }

    public void Reset()
    {
        _timer = 0f;
    }
}