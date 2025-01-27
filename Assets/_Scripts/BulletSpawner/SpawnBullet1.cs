using UnityEngine;

public class SpawnBullet1 : MonoBehaviour
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

    void Start()
    {
        //sets initial value of the firePoint with firePoint GameObject
        transform.position = firePoint.position;
    }

    void Update()
    {
        
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