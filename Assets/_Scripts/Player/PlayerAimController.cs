using UnityEngine;

public class PlayerAimController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject weaponObject;
    [SerializeField] GameObject handObject;  // Add hand reference
    [SerializeField] Transform centerPoint;

    [Header("Weapon Attachment")]
    [SerializeField] Transform leftSideAttachPoint;
    [SerializeField] Transform rightSideAttachPoint;

    private Vector2 mousePosition;
    private bool isAimingRight;
    private Transform currentAttachPoint;

    private void Start()
    {
        if (weaponObject == null)
        {
            weaponObject = gameObject;
        }

        currentAttachPoint = rightSideAttachPoint;
    }

    private void FixedUpdate()
    {
        if (centerPoint == null) return;

        // Get mouse position in world space
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Calculate aim direction and angle
        Vector2 aimDirection = mousePosition - (Vector2)centerPoint.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        // slight change from original 90 degrees, 
        // this allows for more accuracy when aiming below the player
        bool newAimingRight = aimAngle > -103 && aimAngle < 103; 

        if (newAimingRight != isAimingRight)
        {
            isAimingRight = newAimingRight;
            currentAttachPoint = isAimingRight ? leftSideAttachPoint : rightSideAttachPoint;

            // Flip weapon and hand by adjusting scale
            Vector3 newScale = new Vector3(isAimingRight ? 1 : -1, 1, 1);

            if (weaponObject != null)
            {
                weaponObject.transform.localScale = newScale; // Fix side flipping
                weaponObject.transform.position = currentAttachPoint.position;
            }

            if (handObject != null)
            {
                handObject.transform.localScale = newScale; // Fix hand flipping
                handObject.transform.position = currentAttachPoint.position;
            }
        }

        // Update weapon and hand positions
        weaponObject.transform.position = currentAttachPoint.position;
        if (handObject != null)
        {
            handObject.transform.position = currentAttachPoint.position;
        }

        // Set rotation to face mouse
        float rotationAngle = isAimingRight ? aimAngle : aimAngle + 180;
        weaponObject.transform.rotation = Quaternion.Euler(0, 0, rotationAngle);
    }

}
