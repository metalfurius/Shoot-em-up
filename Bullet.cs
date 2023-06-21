using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeTime = 2.0f;
    private float timeAlive = 0.0f;

    void Update()
    {
        timeAlive += Time.deltaTime;
        if (timeAlive >= lifeTime)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "EnemyBullet"||collision.gameObject.tag == "PlayerBullet")
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
}
