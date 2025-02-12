using UnityEngine;

public class PlayerAimController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject weaponObject;
    [SerializeField] Transform centerPoint;

    [Header("Weapon Settings")]
    [SerializeField] float weaponDistance = 1f;
    [SerializeField] float rotationSpeed = 5f;

    private Vector2 mousePosition;
    private Vector2 weaponPosition;

    private void Start()
    {
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

    private void FixedUpdate()
    {
        if (centerPoint == null) return;

        // Get mouse position in world space
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Calculate aim direction and angle
        Vector2 aimDirection = mousePosition - (Vector2)centerPoint.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x);

        // Calculate weapon position around the center point
        float xOffset = Mathf.Cos(aimAngle) * weaponDistance;
        float yOffset = Mathf.Sin(aimAngle) * weaponDistance;
        weaponPosition = (Vector2)centerPoint.position + new Vector2(xOffset, yOffset);

        // Smoothly move weapon to its orbit position
        weaponObject.transform.position = Vector2.Lerp(
            weaponObject.transform.position,
            weaponPosition,
            rotationSpeed * Time.deltaTime
        );

        // Set rotation to face mouse
        float rotationAngle = aimAngle * Mathf.Rad2Deg;
        weaponObject.transform.rotation = Quaternion.Euler(0, 0, rotationAngle);
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