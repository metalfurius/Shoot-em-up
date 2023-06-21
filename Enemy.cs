using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health; 
    public int bulletDamage;       // The amount of damage the enemy takes from each player bullet
    public int collisionDamageTaken;   
    public int collisionDamageGiven;
    public float speed;

    private Transform player;  
    private Rigidbody2D rb;
    public float hoverDistance;
    public float maxHoverDistance;
    public Vector2 oscillationAmplitude;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        float distance = Vector2.Distance(transform.position, player.position);
        if (distance > hoverDistance){
                rb.velocity = direction * speed;
            }
        else{
                rb.velocity = new Vector2(0, Mathf.Sin(Time.time * speed)) * oscillationAmplitude;
            }
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle+90, Vector3.forward);
    }



    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.GetComponent<PlayerController>().TakeDamage(collisionDamageGiven);
            health -= collisionDamageTaken;
            if (health <= 0)
            {
                Destroy(gameObject);
                FindObjectOfType<AudioManager>().Play("DeathEnemy");
                int currValue=PlayerPrefs.GetInt("currValue",0);
                PlayerPrefs.SetInt("currValue",currValue-Random.Range(1,7));
            }
        }
        
        if (collision.gameObject.tag == "PlayerBullet")
        {
            health -= bulletDamage;

            if (health <= 0)
            {
                Destroy(gameObject);
                FindObjectOfType<AudioManager>().Play("DeathEnemy");
                int currValue=PlayerPrefs.GetInt("currValue",0);
                PlayerPrefs.SetInt("currValue",currValue+Random.Range(5,15));
            }

            Destroy(collision.gameObject);
        }
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

}