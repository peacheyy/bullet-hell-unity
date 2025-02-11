using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float rotationSpeed = 5f;

    private Transform target;
    private Rigidbody2D rb;
    private Vector2 moveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Find the player by tag (make sure your player prefab has the "Player" tag)
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            target = player.transform;
        }
        else
        {
            Debug.LogWarning("EnemyController: No player found in scene!");
        }
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