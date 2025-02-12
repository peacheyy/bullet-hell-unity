using UnityEngine;

public class EnemyAimController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject weaponObject;
    [SerializeField] Transform centerPoint;

    [Header("Weapon Settings")]
    [SerializeField] float weaponDistance = 1f;    // Distance from enemy center
    [SerializeField] float rotationSpeed = 5f;

    private Transform playerTransform;
    private Vector2 targetPosition;

    private void Start()
    {
        // Find the player
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Cannot find player with tag 'Player'!");
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
        if (centerPoint == null || playerTransform == null) return;

        // Calculate direction to player
        Vector2 directionToPlayer = (Vector2)playerTransform.position - (Vector2)centerPoint.position;
        float angleToPlayer = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x);

        // Calculate weapon position around the enemy
        float xOffset = Mathf.Cos(angleToPlayer) * weaponDistance;
        float yOffset = Mathf.Sin(angleToPlayer) * weaponDistance;

        // Set the weapon position
        targetPosition = (Vector2)centerPoint.position + new Vector2(xOffset, yOffset);
            // Position movement
            weaponObject.transform.position = Vector2.Lerp(
                weaponObject.transform.position,
                targetPosition,
                rotationSpeed * Time.deltaTime
            );

            // Rotation to face player
            float currentRotation = weaponObject.transform.rotation.eulerAngles.z;
            float targetAngle = (angleToPlayer * Mathf.Rad2Deg) - 90f; // for some odd reason the firepoint is 90 degrees off so the enemy will fire in the wrong direction
            float smoothedAngle = Mathf.LerpAngle(currentRotation, targetAngle, rotationSpeed * Time.deltaTime);
            weaponObject.transform.rotation = Quaternion.Euler(0, 0, smoothedAngle);
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