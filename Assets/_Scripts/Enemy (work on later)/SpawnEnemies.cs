using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    [SerializeField] EnemyController enemyPrefab;

    private Transform[] spawnPoints;
    private List<EnemyController> activeEnemies = new List<EnemyController>();

    void Start()
    {
        spawnPoints = GetComponentsInChildren<Transform>();

        //GetComponentsInChildren includes the parent object so we need to skip it
        spawnPoints = transform.Cast<Transform>().Skip(1).ToArray();

        StartCoroutine(SpawnEnemyRoutine());
    }


    void SpawnEnemy()
    {
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Vector3 spawnPosition = spawnPoints[randomIndex].position;

        //Stores the reference to the enemy so you can track and modify the instance
        EnemyController newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        activeEnemies.Add(newEnemy);
    }

    IEnumerator SpawnEnemyRoutine()
    {
        while (true)
        {
            if (activeEnemies.Count < 10)
            {
                SpawnEnemy();
            }
            yield return new WaitForSeconds(1f);
        }
    }
}
