using System.Collections;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float minTimeBetweenShots;
    public float maxTimeBetweenShots;
    public Transform bulletSpawnPoint;
    public float bulletSpeed = 5f;
    public Transform player;

    private float timeSinceLastShot;
    private void Awake() {
        player = GameObject.FindWithTag("Player").transform;
    }

    void Start(){
        StartCoroutine(ShootBullets());
    }

    IEnumerator ShootBullets(){
        while (true)
        {
            // Wait for a random amount of time between shots
            float waitTime = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
            yield return new WaitForSeconds(waitTime);
            Vector2 direction = (player.position - transform.position).normalized;
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = direction * bulletSpeed;
        }
    }
}
