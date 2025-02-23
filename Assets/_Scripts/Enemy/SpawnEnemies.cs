using UnityEngine;
using System.Collections;

// Controls enemy spawning within a defined rectangular area, with support for
// multiple enemy types, weighted random spawning, and safety checks.
public class SpawnEnemies : MonoBehaviour
{
    // Represents an enemy type that can be spawned, with an associated spawn weight
    // for controlling how frequently it appears relative to other enemy types.
    [System.Serializable]
    public class SpawnableEnemy
    {
        public Enemy enemyPrefab;
        [Range(0f, 1f)]
        public float spawnWeight = 1f;  // Higher weight = more likely to spawn
    }

    [Header("Spawn Area Settings")]
    [SerializeField] Vector2 areaSize = new Vector2(10f, 10f);  // Width and height of spawn area
    [SerializeField] bool visualizeSpawnArea = true;            // Toggle gizmo visibility
    [SerializeField] Color gizmoColor = new Color(1f, 0f, 0f, 0.2f);  // Color for spawn area visualization

    [Header("Spawn Settings")]
    [SerializeField] SpawnableEnemy[] spawnableEnemies;        // Array of possible enemies to spawn
    [SerializeField] int maxEnemies = 10;                      // Total number of enemies to spawn
    [SerializeField] float spawnInterval = 1f;                 // Time between spawns
    [SerializeField] float minDistanceFromPlayer = 5f;         // Minimum spawn distance from player

    private Transform playerTransform;
    private bool isSpawning;        // Controls spawn coroutine

    private void Start()
    {
        // Locate the player in the scene
        playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (playerTransform == null)
        {
            Debug.LogError("SpawnArea: Player not found! Make sure player has 'Player' tag.");
            enabled = false;
            return;
        }


        // Tell LevelManager how many enemies to expect
        LevelManager.Instance.SetInitialEnemyCount(maxEnemies);

        // Begin spawning enemies
        StartCoroutine(SpawnRoutine());
    }

    private float CalculateTotalWeight()
    {
        float totalWeight = 0f;
        for (int i = 0; i < spawnableEnemies.Length; i++)
        {
            totalWeight += spawnableEnemies[i].spawnWeight;
        }
        return totalWeight;
    }

    private Enemy GetRandomEnemy()
    {
        float totalWeight = CalculateTotalWeight();
        float randomValue = Random.Range(0f, totalWeight);

        float currentTotal = 0f; // Tracks running total for ranges

        for (int i = 0; i < spawnableEnemies.Length; i++)
        {
            currentTotal += spawnableEnemies[i].spawnWeight; // Add current enemy's weight
            if (randomValue < currentTotal) // Check if random value falls in this range
            {
                return spawnableEnemies[i].enemyPrefab;
            }
        }

        return spawnableEnemies[0].enemyPrefab; // Safety fallback
    }

    // Generates a random position within the spawn area that's far enough from the player.
    // Uses multiple attempts to find a valid position.
    private Vector3 GetRandomSpawnPosition()
    {
        int maxAttempts = 30;  // Prevent infinite loops
        int attempts = 0;

        while (attempts < maxAttempts)
        {
            // Generate random position within spawn area bounds
            Vector3 randomPos = transform.position + new Vector3(
                Random.Range(-areaSize.x / 2f, areaSize.x / 2f),
                Random.Range(-areaSize.y / 2f, areaSize.y / 2f),
                0f
            );

            // Verify position is far enough from player
            if (Vector3.Distance(randomPos, playerTransform.position) >= minDistanceFromPlayer)
            {
                // Note: You could add additional checks here (e.g., raycast for obstacles)
                return randomPos;
            }

            attempts++;
        }

        Debug.LogWarning("Could not find valid spawn position after " + maxAttempts + " attempts");
        return transform.position;  // Fallback to center if no valid position found
    }

    // Coroutine that handles the spawning of enemies over time.
    // Continues until maxEnemies is reached or stopped manually.
    private IEnumerator SpawnRoutine()
    {
        isSpawning = true;

        while (isSpawning && maxEnemies > 0)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval);
            maxEnemies--;
        }
    }

    // Spawns a single enemy at a random position within the spawn area.
    private void SpawnEnemy()
    {
        Enemy enemyPrefab = GetRandomEnemy();
        Vector3 spawnPosition = GetRandomSpawnPosition();
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }

    // Stops the spawning process. Can be called from other scripts
    // to end spawning early.
    public void StopSpawning()
    {
        isSpawning = false;
    }

    // Draws the spawn area and minimum player distance in the Unity editor.
    private void OnDrawGizmos()
    {
        if (!visualizeSpawnArea) return;

        // Draw spawn area
        Gizmos.color = gizmoColor;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawCube(Vector3.zero, new Vector3(areaSize.x, areaSize.y, 0.1f));

        // Draw minimum spawn distance from player if available
        if (playerTransform != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(playerTransform.position, minDistanceFromPlayer);
        }
    }
}