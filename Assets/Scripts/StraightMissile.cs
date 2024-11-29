using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraigthMissile : MonoBehaviour
{
    private float timer = 0;

    void Update()
    {
        timer += Time.deltaTime;

        // Destroy the bullet after 4 seconds
        if (timer > 4)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        PlayerController player = other.collider.GetComponent<PlayerController>();
        Projectile projectile = other.collider.GetComponent<Projectile>();

        if (player != null)
        {
            player.ChangeHealth(-5);
            Destroy(gameObject);
        }

        if (projectile != null)
        {
            Destroy(gameObject);
        }
    }
}