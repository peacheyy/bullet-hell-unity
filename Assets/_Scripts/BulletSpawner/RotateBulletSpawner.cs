using UnityEngine;

public class RotateBulletSpawner : MonoBehaviour
{

    [SerializeField] Transform bulletSpawner;

    private float timer = 0f;
    private float degrees = 360f;

    void Update()
    {
        //adds seconds between each frame, assigns to timer
        timer += Time.deltaTime;

        //timer between 5f and 6f allows for 1 second to spin the GameObject
        if (timer >= 5f && timer <= 6f)
        {  // Only spin between 5-6 seconds
           // Complete 360 degrees over 1 second
            transform.Rotate(0, 0, degrees * Time.deltaTime);
        }

        // Reset after spin + wait time (6 seconds total)
        if (timer >= 6f)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            timer = 0f;
        }
    }
}
