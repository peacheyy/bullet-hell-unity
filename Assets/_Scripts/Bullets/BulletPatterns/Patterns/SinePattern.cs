using UnityEngine;

public class SinePattern : IBulletPattern
{
    private const int BULLET_COUNT = 24;
    private const float BULLET_SPAWN_INTERVAL = 1f;
    
    private float _timer;
    private readonly string _bulletPoolKey;

    public bool IsComplete { get; private set; }

    public SinePattern(string bulletPoolKey)
    {
        _bulletPoolKey = bulletPoolKey;
    }

    public void Execute(Transform spawner, Transform[] firepoints)
    {
        _timer += Time.deltaTime;

        IsComplete = false;

        if (_timer >= BULLET_SPAWN_INTERVAL)
        {
            for(int i = 1; i <= BULLET_COUNT; i++)
            {
                float angle = i * (360/BULLET_COUNT);

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