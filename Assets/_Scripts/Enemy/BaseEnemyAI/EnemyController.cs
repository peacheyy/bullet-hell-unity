using UnityEngine;

public class EnemyAIMovement : MonoBehaviour
{
    [SerializeField] Transform target; // Serialized field for Inspector access
    [SerializeField] float moveSpeed = 5f; // Serialized field for Inspector access
    [SerializeField] float rotationSpeed = 5f; // Serialized field for Inspector access

    private Rigidbody2D rb;
    private Vector2 moveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (target)
        {
            // 1. Calculate direction and angle
            Vector2 direction = (target.position - transform.position).normalized;
            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // 2. Smoothly rotate the enemy
            float currentAngle = rb.rotation;
            float newAngle = Mathf.Lerp(currentAngle, targetAngle, rotationSpeed * Time.fixedDeltaTime);
            rb.MoveRotation(newAngle);

            // 3. Move the enemy
            moveDirection = direction;
            Vector2 targetVelocity = moveDirection * moveSpeed;
            rb.velocity = Vector2.Lerp(rb.velocity, targetVelocity, Time.fixedDeltaTime * 5f);
        }
    }
}