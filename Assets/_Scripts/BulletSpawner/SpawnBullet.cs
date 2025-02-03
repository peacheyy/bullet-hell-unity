using UnityEngine;

public class SpawnBullet : MonoBehaviour
{
    [SerializeField] GameObject enemyBulletPrefab;
    [SerializeField] Transform[] firePoints;

    private void SpawnEnemyBullet()
    {
        foreach (Transform firePoint in firePoints)
        {
            Instantiate(enemyBulletPrefab, firePoint.position, firePoint.rotation);
        }
    }

    public void getBullet()
    {
        SpawnEnemyBullet();
    }
}