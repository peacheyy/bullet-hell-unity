using UnityEngine;

public class WhipPattern : IBulletPattern
{
    private const int BULLETS_IN_LINE = 1;
    private const float BULLET_SPACING = 0.5f;
    private const float ROTATION_SPEED = 180f; // degrees per second
    private const float PATTERN_DURATION = 3f; // how long to whip around

    private float _timer;
    private readonly string _bulletPoolKey;
    private float _currentAngle;

    public bool IsComplete { get; private set; }

    public WhipPattern(string bulletPoolKey)
    {
        _bulletPoolKey = bulletPoolKey;
    }

    public void Execute(Transform spawner, Transform[] firePoints)
    {
        _timer += Time.deltaTime;
        IsComplete = false;

        // Rotate the current angle based on time
        _currentAngle += ROTATION_SPEED * Time.deltaTime;

        // Spawn a new line of bullets every frame
        SpawnBulletLine(spawner);

        // Check if pattern is complete
        if (_timer >= PATTERN_DURATION)
        {
            IsComplete = true;
        }
    }

    private void SpawnBulletLine(Transform spawner)
    {
        // Calculate the line direction based on current angle
        Quaternion lineRotation = Quaternion.Euler(0, 0, _currentAngle);
        Vector3 lineDirection = lineRotation * Vector3.right;

        for (int i = 0; i < BULLETS_IN_LINE; i++)
        {
            // Calculate the position along the line
            float distance = (i + 1) * BULLET_SPACING;
            Vector3 bulletPosition = spawner.position + lineDirection * distance;

            // Get bullet from pool
            Bullet bullet = BulletPoolManager.Instance.GetBullet(
                _bulletPoolKey,
                bulletPosition,
                lineRotation
            );

            bullet.Initialize(2f, 0f, 10f); // Zero speed - bullets stay in place
        }
    }

    public void Reset()
    {
        _timer = 0f;
        _currentAngle = 0f;
        IsComplete = false;
    }
}