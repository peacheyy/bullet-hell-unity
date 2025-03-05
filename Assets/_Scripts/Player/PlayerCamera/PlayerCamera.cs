using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float smoothValue = 0.125f;
    [SerializeField] float maxCameraDistance = 5f;
    [SerializeField] float cameraThreshold = 0.8f;

    private Transform _playerTransform;
    private Vector2 mousePosition;

    void Start()
    {
        _playerTransform = player.transform;
    }

    void LateUpdate()
    {
        // Calculate desired position (keep camera's z position)
        Vector3 desiredPosition = new Vector3(
            _playerTransform.position.x,
            _playerTransform.position.y,
            transform.position.z
        );

        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 mouseDirection = (mousePosition - (Vector2)_playerTransform.position).normalized;

        // Get screen center
        Vector2 screenCenter = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);

        // Get mouse position relative to center (as percentage of screen size)
        Vector2 mouseOffset = Input.mousePosition - (Vector3)screenCenter;
        Vector2 distanceFromCenter = new Vector2(
            Mathf.Abs(mouseOffset.x) / (Screen.width * 0.5f),
            Mathf.Abs(mouseOffset.y) / (Screen.height * 0.5f)
        );

        // Check if mouse is beyond threshold in either direction
        if ((distanceFromCenter.x > cameraThreshold || distanceFromCenter.y > cameraThreshold) && !PauseMenu.instance.getGameStatus())
        {
            // Calculate how far past threshold we are (0 to 1 range) thanks AI I don't really understand this other than it slows & smooths the transition at the edge of the screen!!!
            float peekMultiplier = Mathf.Max(
                Mathf.InverseLerp(cameraThreshold, 1f, distanceFromCenter.x),
                Mathf.InverseLerp(cameraThreshold, 1f, distanceFromCenter.y)
            );

            // Apply scaled peek effect
            desiredPosition = new Vector3(
                _playerTransform.position.x + (mouseDirection.x * maxCameraDistance * peekMultiplier),
                _playerTransform.position.y + (mouseDirection.y * maxCameraDistance * peekMultiplier),
                transform.position.z
            );
        }

        // Smoothly move camera to that position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothValue);
        transform.position = smoothedPosition;
    }
}