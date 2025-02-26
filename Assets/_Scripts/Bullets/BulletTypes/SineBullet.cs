using UnityEngine;

public class SineBullet : Bullet
{
    [SerializeField] private float bulletLifetime = 10f;
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private float bulletDamage = 10f;

    [SerializeField] private float waveAmplitude = 2f;
    [SerializeField] private float waveFrequency = 3f;

    private Vector2 _startPosition;
    private Vector2 _direction;
    private float _travelTime;

    protected override void OnEnable()
    {
        Initialize(bulletLifetime, bulletSpeed, bulletDamage);
        base.OnEnable();

        _startPosition = transform.position;
        _direction = transform.up;
        _travelTime = 0f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ReturnToPool();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(DamageAmount);
                ReturnToPool();
            }
        }
    }

    protected override void Update()
    {
        base.HandleLifetime(); 
        
        // Update travel time
        _travelTime += Time.deltaTime;

        // Calculate forward position
        float distanceTraveled = Speed * _travelTime;
        Vector2 forwardPos = _startPosition + _direction * distanceTraveled;

        // Calculate perpendicular movement based on sine wave
        Vector2 waveDirection = new Vector2(-_direction.y, _direction.x); // Perpendicular to movement
        float waveOffset = Mathf.Sin(_travelTime * waveFrequency) * waveAmplitude;

        // Set the position directly
        transform.position = forwardPos + waveDirection * waveOffset;
    }

    // Override this to prevent normal velocity setting
    protected override void InitMovement()
    {
        rb.velocity = Vector2.zero;
    }
}