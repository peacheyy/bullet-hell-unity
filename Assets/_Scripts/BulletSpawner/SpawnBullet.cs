using UnityEngine;

public class SpawnBullet : MonoBehaviour
{

    [Header("Bullet Attributes")]
    [SerializeField] GameObject bullet;
    [SerializeField] float bulletLife = 1f;
    [SerializeField] float speed = 1f;

    [SerializeField] Transform firePoint;

    [Header("Spawner Attribute(s)")]
    [SerializeField] float firingRate = 1f;

    private GameObject spawnedBullet;
    private float timer = 0f;

    void Start()
    {
        transform.position = firePoint.position;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= firingRate)
        {
            Fire();
            timer = 0;
        }
    }

    private void Fire()
    {
        if (bullet)
        {
            // Use firePoint's rotation instead of transform.rotation
            spawnedBullet = Instantiate(bullet, firePoint.position, firePoint.rotation);
            Bullet bulletComponent = spawnedBullet.GetComponent<Bullet>();
            bulletComponent.speed = speed;
            bulletComponent.bulletLife = bulletLife;
        }
    }
}
