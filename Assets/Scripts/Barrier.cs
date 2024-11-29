using UnityEngine;

public class WallController2D : MonoBehaviour
{
    public Rigidbody2D wallRigidbody; // Assign the Rigidbody2D of the wall in the Inspector
    public Transform playerTransform; // Assign the player's Transform in the Inspector
    public float minDistanceToPlayer = 1f; // Minimum distance to freeze Y-axis

    private void FixedUpdate()
    {
        // Calculate horizontal distance between player and wall
        float distanceToPlayer = Mathf.Abs(playerTransform.position.x - transform.position.x);

        // If the player is hugging the wall (too close)
        if (distanceToPlayer < minDistanceToPlayer)
        {
            // Freeze the Y-axis
            wallRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else
        {
            // Allow movement on Y-axis but freeze rotation
            wallRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
        }
    }
}
