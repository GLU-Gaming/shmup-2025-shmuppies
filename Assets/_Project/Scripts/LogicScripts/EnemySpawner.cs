using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("References")]
    public GameObject enemyPrefab;
    public Transform playerLocation;

    [Header("Spawn Distance")]
    public float minSpawnDistance = 5f;
    public float maxSpawnDistance = 15f;

    [Header("Spawning")]
    public int minEnemiesSpawned = 1;
    public int maxEnemiesSpawned = 3;
    public float spawnInterval = 2f;

    [Header("Enemy Count")]
    public int currentEnemyCount = 0;
    public int maxEnemyCount = 10;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnWave), 0f, spawnInterval);
        playerLocation = GameObject.FindWithTag("Player").GetComponent<Transform>();
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

        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        currentEnemyCount++;
    }
}
