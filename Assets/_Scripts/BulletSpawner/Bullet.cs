using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletLife = 1f;
    public float speed = 1f;

    private float timer = 0f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Set velocity based on the bullet's up direction
        rb.velocity = transform.up * speed;
    }

    //checks whether the fired bullet's life exceeds the timer
    void Update()
    {
        if (timer > bulletLife)
        {
            Destroy(gameObject);
            return;
        }
        timer += Time.deltaTime;
    }

    //collision detection when bullet encounters a rigidbody
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}