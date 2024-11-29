using System.Collections;
using UnityEngine;

public class StunController : MonoBehaviour
{
    public float expandSpeed = 1f;
    public float maxSize = 3f; // Maximum size before destroying
    public float expandDuration = 2f; // Duration for the expansion

    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;
    public AudioClip playerHitClip;
    private bool isExpanding = true; // Tracks if the object is still expanding

    void Start()
    {
        // Get the SpriteRenderer component
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Get the BoxCollider2D component if present
        boxCollider = GetComponent<BoxCollider2D>();

        // Start the expansion coroutine
        StartCoroutine(ExpandForDuration());
    }

    void Update()
    {
        if (isExpanding)
        {
            Expand();
        }
    }

    void Expand()
    {
        // Increase the scale of the GameObject over time
        transform.localScale += Vector3.one * expandSpeed * Time.deltaTime;
    }

    IEnumerator ExpandForDuration()
    {
        // Wait for the expand duration
        yield return new WaitForSeconds(expandDuration);

        // Stop expanding and destroy the object
        isExpanding = false;

        // Optional: Destroy the object if it should disappear after expansion stops
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            // Apply damage to the player and play sound
            if (!player.isInvincible)
            {
                player.isStunned = true;
                player.stunDuration = 2.0f;
                player.ChangeHealth(-2);
                player.PlaySound(playerHitClip);
            }
        }
    }
}
