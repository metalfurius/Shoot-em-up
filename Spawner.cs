using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public int[] enemyWeights;
    public Vector2[] spawnPoints; 
    public float spawnInterval; 

    private float timeSinceLastSpawn;
    public float initialInterval;
    public float minimumInterval;
    public float timeToReachMinimumInterval;

    void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;
        
        // Decreasing the spawn interval over time
        spawnInterval = Mathf.Lerp(initialInterval, minimumInterval, Time.time / timeToReachMinimumInterval);

        // If the time since the last enemy was spawned is greater than the spawn interval, and there are less than the max number of enemies, spawn a new enemy
        if (timeSinceLastSpawn >= spawnInterval)
        {
            timeSinceLastSpawn = 0;
            // Choose a random spawn point
            int randomIndex = Random.Range(0, spawnPoints.Length);
            Vector2 spawnPoint = spawnPoints[randomIndex];

            // Choose a random enemy prefab based on its weight
            int totalWeight = 0;
            foreach (int weight in enemyWeights)
            {
                totalWeight += weight;
            }
            int randomValue = Random.Range(0, totalWeight);
            int prefabIndex = 0;
            int currentWeight = enemyWeights[0];
            while (randomValue > currentWeight)
            {
                prefabIndex++;
                currentWeight += enemyWeights[prefabIndex];
            }
            GameObject enemyPrefab = enemyPrefabs[prefabIndex];

            // Spawn a new enemy at the chosen spawn point
            Instantiate(enemyPrefab, spawnPoint, Quaternion.Euler(0, 0, -90));
        }
    }

}
