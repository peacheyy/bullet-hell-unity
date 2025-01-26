using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBullet : MonoBehaviour {

    [Header("Bullet Attributes")]
    public GameObject bullet;
    public float bulletLife = 1f;
    public float speed = 1f;

    public Transform firePoint;

    [Header("Spawner Attribute(s)")]
    [SerializeField] float firingRate = 1f;

    private GameObject spawnedBullet;
    private float timer = 0f;

    void Start() {
        transform.position = firePoint.position;
    }

    void Update() {
        timer += Time.deltaTime;
        if(timer >= firingRate) {
            Fire();
            timer = 0;
        }
    }

    private void Fire() {
        if(bullet) {
            spawnedBullet = Instantiate(bullet, transform.position, Quaternion.identity);
            spawnedBullet.GetComponent<Bullet>().speed = speed;
            spawnedBullet.GetComponent<Bullet>().bulletLife = bulletLife;
            spawnedBullet.GetComponent<Bullet>().firePoint = transform;
            spawnedBullet.transform.rotation = transform.rotation;
        }
    }
}

