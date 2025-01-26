using UnityEngine;

public class RotateBulletSpawner : MonoBehaviour
{

    [SerializeField] Transform bulletSpawner;

    private float rotationSpeed = 360f;
    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 5f)
        {
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        }
        if (timer >= 6f)
        { // Reset after 1 second of rotation
            timer = 0f;
        }
    }
}
