using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    [SerializeField] Enemy enemyPrefab;
    [SerializeField] int enemyCount = 5;

    private Transform[] spawnPoints;

    void Start()
    {
        spawnPoints = GetComponentsInChildren<Transform>();
        spawnPoints = transform.Cast<Transform>().Skip(1).ToArray();

        LevelManager.Instance.SetInitialEnemyCount(enemyCount);
        StartCoroutine(SpawnEnemyRoutine());
    }

    void SpawnEnemy()
    {
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Vector3 spawnPosition = spawnPoints[randomIndex].position;
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }

    IEnumerator SpawnEnemyRoutine()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(1f);
        }
    }
}