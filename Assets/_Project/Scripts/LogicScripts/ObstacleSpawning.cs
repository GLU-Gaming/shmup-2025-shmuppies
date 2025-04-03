using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstacles;
    public int numberOfObstacles = 100;
    public Transform playerSpawnpoint;
    public float spawnRadius = 50f; // Radius around player spawn point

    void Start()
    {
        for (int i = 0; i < numberOfObstacles; i++)
        {
            Vector3 randomSpawnPosition = GetRandomSpawnPosition();
            GameObject obstacleToSpawn = GetRandomObstacle();
            Instantiate(obstacleToSpawn, randomSpawnPosition, Quaternion.identity);
        }
    }

    Vector3 GetRandomSpawnPosition()
    {
        float randomAngle = Random.Range(0f, 360f);
        float randomDistance = Random.Range(10f, spawnRadius); // Avoid spawning too close
        float spawnX = playerSpawnpoint.position.x + Mathf.Cos(randomAngle * Mathf.Deg2Rad) * randomDistance;
        float spawnZ = playerSpawnpoint.position.z + Mathf.Sin(randomAngle * Mathf.Deg2Rad) * randomDistance;

        return new Vector3(spawnX, 0, spawnZ);
    }

    GameObject GetRandomObstacle()
    {
        int randomIndex = Random.Range(0, obstacles.Length);
        return obstacles[randomIndex];
    }
}
