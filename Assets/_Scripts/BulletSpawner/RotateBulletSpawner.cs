using UnityEngine;

public class RotateBulletSpawner : MonoBehaviour
{

    [SerializeField] Transform bulletSpawner;

    private float degrees = 360f;
    private float timer = 0f;
    private float maxTime = 3f;
    private float maxRotationTime = 1f;

    void Update()
    {
        //adds seconds between each frame, assigns to timer
        timer += Time.deltaTime;

        //timer between 5f and 6f allows for 1 second to spin the GameObject
        if (timer >= maxTime && timer <= maxTime + maxRotationTime)
        {  // Only spin between 5-6 seconds
           // Complete 360 degrees over 1 second
            transform.Rotate(0, 0, degrees * Time.deltaTime);
        }

        // Reset after spin + wait time (6 seconds total)
        if (timer >= maxTime + maxRotationTime)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            timer = 0f;
        }
    }
}
