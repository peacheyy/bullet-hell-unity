using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float moveSpeed = 2f;

    [Header("Dash Settings")]
    [SerializeField] float dashSpeed = 10f;
    [SerializeField] float dashDuration = 0.2f;
    [SerializeField] float dashCooldown = 1f;

    private bool isDashing;
    private bool canDash = true;

    private Vector2 moveDirection;
    private Vector2 mousePosition;

    void Update()
    {
        // Get movement input
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        // Normalized prevents going faster while moving diagonally
        moveDirection = new Vector2(moveX, moveY).normalized;
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

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

        // Rotate player to face the mouse
        Vector2 aimDirection = mousePosition - rb.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = aimAngle;
    }

    private IEnumerator Dash()
    {
        isDashing = true;
        canDash = false;

        // Store the original velocity and set the dash velocity
        Vector2 originalVelocity = rb.velocity;
        rb.velocity = moveDirection * dashSpeed;

        // Wait for the dash duration
        yield return new WaitForSeconds(dashDuration);

        // Reset velocity after dash
        rb.velocity = originalVelocity;

        isDashing = false;

        // Wait for the cooldown before allowing another dash
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}