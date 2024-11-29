using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    // Public variables
    [Range(0f, 1f)] public float speedBoostChance = 0.5f; // 50% chance to trigger SpeedBoost

    public AudioClip collectedClip;

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController controller = other.GetComponent<PlayerController>();

        if (controller != null)
        {
            if (controller.health < controller.maxHealth)
            {
                controller.ChangeHealth(1);
                controller.PlaySound(collectedClip);
            }

            // Random chance to trigger SpeedBoost
            if (Random.value < speedBoostChance) // Random.value returns a number between 0 and 1
            {
                controller.ActivateSpeedBoost(5f, 1.5f); // Example: boost for 5 seconds with 1.5x speed
            }

            Destroy(gameObject);
        }
    }
}
