using UnityEngine;

public class SpawnBullet2 : MonoBehaviour
{

    [Header("Bullet Attributes")]
    [SerializeField] GameObject bullet;
    [SerializeField] Transform firePoint;
    [SerializeField] float bulletLife = 1f;
    [SerializeField] float speed = 1f;
    [SerializeField] float damageAmount = 5f;
    [SerializeField] float firingRate = 1f;
    private GameObject spawnedBullet;

    //having a fire timer and a rotation timer makes the bullet spawner wait to fire until it's rotating
    private float fireTimer = 0f;
    private float rotationTimer = 0f;
    private float maxRotationTime = 3f;
    private float rotationDuration = 1f;

    void Start()
    {
        //sets initial value of the spawned bullet's position using the firepoint
        transform.position = firePoint.position;
    }

    void Update()
    {
        //handles fire rate by comparing the firing rate variable with a timer
        //uses Time.deltaTime to get seconds between each frame

        rotationTimer += Time.deltaTime;

        if (rotationTimer >= maxRotationTime && rotationTimer <= maxRotationTime + rotationDuration)
        {
            fireTimer += Time.deltaTime;
            if (fireTimer >= firingRate)
            {
                Fire();
                fireTimer = 0f;
            }
        }

        //resets the rotation timer
        if (rotationTimer >= maxRotationTime + rotationDuration) {
            rotationTimer = 0f;
        }
    }

    private void Fire()
    {
        if (bullet)
        {
            //Instantiates bullet and sets all required attributes
            spawnedBullet = Instantiate(bullet, firePoint.position, firePoint.rotation);
            Bullet bulletComponent = spawnedBullet.GetComponent<Bullet>();
            bulletComponent.Speed = speed;
            bulletComponent.BulletLife = bulletLife;
            bulletComponent.DamageAmount = damageAmount;
        }
    }
}