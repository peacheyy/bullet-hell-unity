using UnityEngine;

public class CirclePattern : IBulletPattern
{
    private const int BULLET_COUNT = 12;
    private const float SPAWN_INTERVAL = 1f;  // Time between circle spawns
    private float _timer;

    public void Execute(Transform spawner, Transform[] firePoints, GameObject bulletPrefab)
    {
        _timer += Time.deltaTime;

        if (_timer >= SPAWN_INTERVAL)
        {
            // Spawn circle of bullets
            for (int i = 0; i < BULLET_COUNT; i++)
            {
                float angle = i * (360f / BULLET_COUNT);
                Quaternion rotation = Quaternion.Euler(0, 0, angle);
                GameObject.Instantiate(bulletPrefab, spawner.position, rotation);
            }
            _timer = 0f;  // Reset timer after spawning
        }
    }

    public void Reset()
    {
        _timer = 0f;
    }
}