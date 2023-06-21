using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f,shootDelay = 0.2f,specialAttackCooldown = 15.0f,angleIncrement,bulletSpeed = 10f,pwrUpDuration=10f;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public int maxHealth,currentHealth,bulletDamageTaken = 1,bulletCount = 8;
    private float lastShootTime = 0,specialAttackTimer = 0.0f,originalFireRate;
    private bool specialAttackReady = true;

    private Rigidbody2D rb;

    private void Awake() {
        PlayerPrefs.SetInt("currValue",0);
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        originalFireRate=shootDelay;
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(horizontal, vertical);
        rb.velocity = movement * speed;

        //Normal shoot method
        if (Input.GetButtonDown("Fire1")||Input.GetButton("Fire1") && Time.time >= lastShootTime + shootDelay)
        { 
            FindObjectOfType<AudioManager>().Play("Shoot");
            Shoot();
            lastShootTime = Time.time;
        }
        //Special shoot and cooldown
        if (Input.GetButtonDown("Fire2") && specialAttackReady)
        {
            
         FindObjectOfType<AudioManager>().Play("SpecialShoot");
            specialAttackReady = false;
            SpecialShoot();
        }

        if (!specialAttackReady)
        {
            specialAttackTimer += Time.deltaTime;
            if (specialAttackTimer >= specialAttackCooldown)
            {
                specialAttackTimer = 0.0f;
                specialAttackReady = true;
            }
        }
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        Debug.Log("Player Died");
        FindObjectOfType<AudioManager>().Play("Death");
        int currValue=PlayerPrefs.GetInt("currValue",0);
        PlayerPrefs.SetInt("currValue",0);
        SceneManager.LoadScene("Main");
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.Euler(0, 0, -90));
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.velocity = new Vector2(bulletSpeed, 0.0f);
    }
    void SpecialShoot()
    {
        angleIncrement = 360f / bulletCount;
        float startAngle = 0f;
        for (int i = 0; i < bulletCount; i++)
        {
            float angle = startAngle + angleIncrement * i;
            float xDirection = Mathf.Cos(angle * Mathf.Deg2Rad);
            float yDirection = Mathf.Sin(angle * Mathf.Deg2Rad);

            GameObject newBullet = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0, 0, -90));
            Rigidbody2D bulletRB = newBullet.GetComponent<Rigidbody2D>();
            bulletRB.velocity = new Vector2(xDirection, yDirection) * bulletSpeed;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "EnemyBullet")
        {
            TakeDamage(bulletDamageTaken);
            Destroy(collision.gameObject);
            int currValue=PlayerPrefs.GetInt("currValue",0);
            PlayerPrefs.SetInt("currValue",currValue- UnityEngine.Random.Range(1,6));
        }
    }
    public void StartReset() {
        {
            StartCoroutine(ResetFireRate());
        }
    }
    IEnumerator ResetFireRate()
    {
        yield return new WaitForSeconds(pwrUpDuration);
        shootDelay = originalFireRate;
    }
}
