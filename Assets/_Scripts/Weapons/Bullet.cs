using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision) {
        Destroy(gameObject);

        //then check to see if the bullet hits an enemy
    }
}
