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
        //sets initial value of the spawned bullet's position using the firepoint
        transform.position = firePoint.position; 
    }

    void Update()
    {
        //handles fire rate by comparing the firing rate variable with a timer
        //uses Time.deltaTime to get seconds between each frame
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
            //Instantiates bullet and sets all required attributes
            spawnedBullet = Instantiate(bullet, firePoint.position, firePoint.rotation);
            Bullet bulletComponent = spawnedBullet.GetComponent<Bullet>();
            bulletComponent.speed = speed;
            bulletComponent.bulletLife = bulletLife;
        }
    }
}
