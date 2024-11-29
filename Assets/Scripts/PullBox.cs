using UnityEngine;

public class PullBox : MonoBehaviour
{
    public Transform box;
    public float pullSpeed = 2f;
    public float pullDistance = 5f;
    public BoxCollider2D boxCollider;

    private Rigidbody2D boxRigidbody;

    void Start()
    {
        if (box != null && boxCollider != null)
        {
            boxRigidbody = box.GetComponent<Rigidbody2D>();
        }
    }

    void FixedUpdate()
    {
        // Check if the B key is being held down and the box exists
        if (Input.GetKey(KeyCode.B) && box != null && boxCollider != null)
        {
            // Set the collider to trigger to avoid interference
            boxCollider.isTrigger = true;

            // Check the distance between the player and the box
            float distance = Vector2.Distance(transform.position, box.position);

            if (distance <= pullDistance)
            {
                // Pull the box toward the player
                Vector2 pullDirection = (transform.position - box.position).normalized;
                boxRigidbody.velocity = pullDirection * pullSpeed;
            }
            else
            {
                // Stop pulling if the box is too far
                boxRigidbody.velocity = Vector2.zero;
            }
        }
        else
        {
            if (boxCollider != null)
            {
                boxCollider.isTrigger = false;
            }

            if (boxRigidbody != null)
            {
                boxRigidbody.velocity = Vector2.zero;
            }
        }
    }
}
