using UnityEngine;

public class FloatingWeapon2D : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject weaponObject;
    [SerializeField] private Transform centerPoint;

    [Header("Weapon Settings")]
    [SerializeField] private float weaponDistance = 1f;    // Distance from enemy center
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private bool smoothRotation = true;

    private Transform playerTransform;
    private Vector2 targetPosition;

    private void Start()
    {
        // Find the player
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("FloatingWeapon2D: Cannot find player with tag 'Player'!");
            enabled = false;
            return;
        }
        playerTransform = player.transform;

        // Check for weapon object
        if (weaponObject == null)
        {
            weaponObject = gameObject;
        }

        // Set center point if not assigned
        if (centerPoint == null && transform.parent != null)
        {
            centerPoint = transform.parent;
        }
    }

    private void Update()
    {
        if (!enabled || centerPoint == null || playerTransform == null) return;

        // Calculate direction to player
        Vector2 directionToPlayer = (Vector2)playerTransform.position - (Vector2)centerPoint.position;
        float angleToPlayer = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x);

        // Calculate weapon position around the enemy
        float xOffset = Mathf.Cos(angleToPlayer) * weaponDistance;
        float yOffset = Mathf.Sin(angleToPlayer) * weaponDistance;

        // Set the weapon position
        targetPosition = (Vector2)centerPoint.position + new Vector2(xOffset, yOffset);

        // Update position and rotation
        if (smoothRotation)
        {
            // Smooth position movement
            weaponObject.transform.position = Vector2.Lerp(
                weaponObject.transform.position,
                targetPosition,
                rotationSpeed * Time.deltaTime
            );

            // Smooth rotation to face player
            float currentRotation = weaponObject.transform.rotation.eulerAngles.z;
            float targetAngle = angleToPlayer * Mathf.Rad2Deg;
            float smoothedAngle = Mathf.LerpAngle(currentRotation, targetAngle, rotationSpeed * Time.deltaTime);
            weaponObject.transform.rotation = Quaternion.Euler(0, 0, smoothedAngle);
        }
        else
        {
            // Instant position and rotation
            weaponObject.transform.position = targetPosition;
            weaponObject.transform.rotation = Quaternion.Euler(0, 0, angleToPlayer * Mathf.Rad2Deg);
        }
    }

    // Helper method to visualize in the editor
    private void OnDrawGizmosSelected()
    {
        if (centerPoint != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(centerPoint.position, weaponDistance);
        }
    }
}