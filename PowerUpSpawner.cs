using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    public GameObject powerUpPrefab;
    public float spawnInterval = 15f;
    public float randomRange = 5f;

    private float lastSpawnTime;

    private void Start()
    {
        lastSpawnTime = Time.time;
    }

    private void Update()
    {
        if (Time.time - lastSpawnTime >= spawnInterval)
        {
            lastSpawnTime = Time.time;
            Vector2 spawnPos = transform.position;
            spawnPos.x += Random.Range(-randomRange, randomRange);
            spawnPos.y += Random.Range(-randomRange, randomRange);
            Instantiate(powerUpPrefab, spawnPos, Quaternion.identity);
        }
    }
}
