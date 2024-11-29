using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    // Public variables
    public bool multiShotPowerUp;
    public bool rapidFirePowerUp;

    public AudioClip collectedClip;

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController controller = other.GetComponent<PlayerController>();

        if (controller != null)
        {
            if (multiShotPowerUp)
            {
                controller.multiShotUpgrade(true);
                controller.PlaySound(collectedClip);
            }

            if (rapidFirePowerUp)
            {
                controller.rapidFireUpgrade(true);
                controller.PlaySound(collectedClip);
            }

            Destroy(gameObject);
        }
    }
}
