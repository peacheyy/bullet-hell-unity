using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float pixelSize = 1f / 32f; // Adjust based on your PPU

    void LateUpdate()
    {
        Vector3 desiredPosition = new Vector3(
            target.position.x,
            target.position.y,
            transform.position.z
        );

        // Snap to the nearest pixel
        desiredPosition.x = Mathf.Round(desiredPosition.x / pixelSize) * pixelSize;
        desiredPosition.y = Mathf.Round(desiredPosition.y / pixelSize) * pixelSize;

        // Smoothly move the camera
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * 10f);
    }
}