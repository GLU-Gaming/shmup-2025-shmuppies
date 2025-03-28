using System.Collections;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstacles; // Ensure this array is assigned in the Inspector
    public int numberOfObstacles = 100;

    void Start()
    {

        for (int i = 0; i < numberOfObstacles; i++)
        {
            Vector3 randomSpawnPosition = new Vector3(Random.Range(-500, 500), 0, Random.Range(-500, 500));
            GameObject obstacleToSpawn = GetRandomObstacle();
            Instantiate(obstacleToSpawn, randomSpawnPosition, Quaternion.identity);
        }
    }

    GameObject GetRandomObstacle()
    {
        int randomIndex = Random.Range(0, obstacles.Length);
        return obstacles[randomIndex]; // Corrected to return a random obstacle from the array
    }
}
