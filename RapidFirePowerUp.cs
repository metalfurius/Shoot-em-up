using System.Collections;
using UnityEngine;

public class RapidFirePowerUp : MonoBehaviour
{
    public float duration = 10f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerController playerShooter = other.GetComponent<PlayerController>();
            float originalFireRateValue = playerShooter.shootDelay;
            playerShooter.shootDelay = playerShooter.shootDelay/2;
            playerShooter.StartReset();
        }
        Destroy(gameObject);
    }
}

