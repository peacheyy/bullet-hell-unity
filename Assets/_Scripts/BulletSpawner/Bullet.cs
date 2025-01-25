using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float bulletLife = 1f;
    public float rotation = 0f;
    public float speed = 1f;
    public Transform firePoint;

    private float timer = 0f; 

    void Start() {
        transform.position = firePoint.position;
    }

    void Update() {
        if(timer > bulletLife) Destroy(this.gameObject);
        timer += Time.deltaTime;
        transform.position = Movement(timer);
    }

    private Vector2 Movement(float timer) {
        float x = firePoint.position.x + timer * speed * transform.right.x;
        float y = firePoint.position.y + timer * speed * transform.right.y;
        return new Vector2(x, y);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Destroy(gameObject);

        //then check to see if the bullet hits an enemy
    }
}
    
