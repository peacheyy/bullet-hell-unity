using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float moveSpeed = 2f;

    [SerializeField] Player player;

    [Header("Dash Settings")]
    [SerializeField] float dashSpeed = 10f;
    [SerializeField] float dashDuration = 0.2f;
    [SerializeField] float dashCooldown = 1f;

    private bool isDashing;
    private bool canDash = true;

    private Vector2 moveDirection;

    void Start()
    {
        if (player == null)
        {
            player = GetComponent<Player>();
        }
    }

    void Update()
    {
        // Get movement input
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        // Normalized prevents going faster while moving diagonally
        moveDirection = new Vector2(moveX, moveY).normalized;

        // Check for dash input
        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    void FixedUpdate()
    {
        if (isDashing) { return; } // Skip movement and rotation while dashing

        // Apply movement
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);

    }

    private IEnumerator Dash()
    {
        isDashing = true;

        canDash = false;

        if (player != null)
        {
            player.SetInvulnerable(true);
        }

        // Store the original velocity and set the dash velocity
        Vector2 originalVelocity = rb.velocity;
        rb.velocity = moveDirection * dashSpeed;

        // Wait for the dash duration
        yield return new WaitForSeconds(dashDuration);

        // Reset velocity after dash
        rb.velocity = originalVelocity;

        isDashing = false;

        player.SetInvulnerable(false);

        // Wait for the cooldown before allowing another dash
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}