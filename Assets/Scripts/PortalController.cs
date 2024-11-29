using UnityEngine;

public class PortalTeleport : MonoBehaviour
{
    [Header("Target Portal")]
    public GameObject targetPortal; // The portal to teleport to

    [Header("Teleportation Settings")]
    public float teleportCooldown = 1.0f; // Cooldown duration to prevent looping

    private bool canTeleport = true; // Tracks teleport availability

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Ensure the object entering the portal is tagged as "Ruby" and the target portal exists
        if (other.CompareTag("Ruby") && canTeleport && targetPortal != null)
        {
            // Start cooldown to prevent re-teleportation
            StartCoroutine(TeleportCooldown());

            // Teleport the player
            TeleportPlayer(other.gameObject);
        }
    }

    private void TeleportPlayer(GameObject player)
    {
        // Move the player to the target portal's position
        player.transform.position = targetPortal.transform.position;

        // Optionally, match the player's rotation with the target portal
        player.transform.rotation = targetPortal.transform.rotation;

        // Disable teleporting temporarily at the target portal
        PortalTeleport targetPortalScript = targetPortal.GetComponent<PortalTeleport>();
        if (targetPortalScript != null)
        {
            targetPortalScript.DisableTeleport();
        }
    }

    private System.Collections.IEnumerator TeleportCooldown()
    {
        canTeleport = false;
        yield return new WaitForSeconds(teleportCooldown);
        canTeleport = true;
    }

    public void DisableTeleport()
    {
        StartCoroutine(TeleportCooldown());
    }
}
