using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("References")]
    public GameObject[] enemyPrefabs;
    public GameObject bossPrefab;
    public Transform playerLocation;
    public Transform bossSpawnPoint;

    [Header("Spawn Distance")]
    public float minSpawnDistance = 5f;
    public float maxSpawnDistance = 15f;

    [Header("Spawning")]
    public int minEnemiesSpawned = 1;
    public int maxEnemiesSpawned = 3;
    public float spawnInterval = 2f; // Initial spawn interval
    public float bossCounter = 0f;
    public float bossSpawnTime = 10f;

    [Header("Enemy Count")]
    public int currentEnemyCount = 0;
    public int startMaxEnemyCount = 5;
    public int maxEnemyCount = 10;

    private float spawnIntervalDecrementRate = 0.1f; // How much to decrease spawn interval every spawn cycle
    private bool bossSpawned = false; // Track if the boss has been spawned

    private void Start()
    {
        InvokeRepeating(nameof(SpawnWave), 0f, spawnInterval);
        playerLocation = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        if (!bossSpawned) // Check if the boss has already been spawned
        {
            bossCounter += Time.deltaTime;

            if (bossCounter > bossSpawnTime)
            {
                Instantiate(bossPrefab, bossSpawnPoint.position, Quaternion.identity); // Instantiate at the boss spawn point
                bossSpawned = true; // Mark the boss as spawned
                bossCounter = 0f; // Reset the boss spawn counter
            }
        }
    }

    private void SpawnWave()
    {
        if (currentEnemyCount < maxEnemyCount)
        {
            int enemiesToSpawn = Random.Range(minEnemiesSpawned, maxEnemiesSpawned + 1);
            for (int i = 0; i < enemiesToSpawn; i++)
            {
                if (currentEnemyCount >= maxEnemyCount) break;
                SpawnEnemy();
            }

            // Gradually decrease the spawn interval over time
            spawnInterval = Mathf.Max(0.5f, spawnInterval - spawnIntervalDecrementRate); // Ensure spawn interval doesn’t go below 0.5s
            CancelInvoke(nameof(SpawnWave)); // Cancel the current repeating call
            InvokeRepeating(nameof(SpawnWave), spawnInterval, spawnInterval); // Re-invoke with the new faster interval
        }
    }

    private void SpawnEnemy()
    {
        Vector3 spawnPosition;
        do
        {
            float randomAngle = Random.Range(0f, 360f);
            float randomDistance = Random.Range(minSpawnDistance, maxSpawnDistance);
            Vector3 offset = new Vector3(Mathf.Cos(randomAngle * Mathf.Deg2Rad), 0, Mathf.Sin(randomAngle * Mathf.Deg2Rad)) * randomDistance;
            spawnPosition = playerLocation.position + offset;
        }
        while (Physics.CheckSphere(spawnPosition, 1f)); // Avoid overlapping

        Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], spawnPosition, Quaternion.identity);
        currentEnemyCount++;
    }
}
