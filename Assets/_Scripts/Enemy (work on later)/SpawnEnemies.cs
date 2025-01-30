using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    [SerializeField] Enemy enemyPrefab;

    private Transform[] spawnPoints;
    private List<Enemy> activeEnemies = new List<Enemy>();

    void Start()
    {
        spawnPoints = GetComponentsInChildren<Transform>();

        // GetComponentsInChildren includes the parent object so we need to skip it
        spawnPoints = transform.Cast<Transform>().Skip(1).ToArray();

        // Subscribe to the OnEnemyDeath event
        Enemy.OnEnemyDeath += HandleEnemyDeath;

        StartCoroutine(SpawnEnemyRoutine());
    }

    void OnDestroy()
    {
        // Unsubscribe from the event to prevent memory leaks
        Enemy.OnEnemyDeath -= HandleEnemyDeath;
    }

    void HandleEnemyDeath(Enemy enemy)
    {
        // Remove the dead enemy from the activeEnemies list
        activeEnemies.Remove(enemy);

        // Spawn a new enemy
        SpawnEnemy();
    }

    void SpawnEnemy()
    {
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Vector3 spawnPosition = spawnPoints[randomIndex].position;

        // Stores the reference to the enemy so you can track and modify the instance
        Enemy newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        activeEnemies.Add(newEnemy);
    }

    //handles enemy spawn using IEnumerator
    IEnumerator SpawnEnemyRoutine()
    {
        //while until false which is never in this implimentation
        while (true)
        {
            if (activeEnemies.Count < 10)
            {
                SpawnEnemy();
            }
            //IEnumerator functionality that lets the loop wait 1s
            yield return new WaitForSeconds(1f);
        }
    }
}