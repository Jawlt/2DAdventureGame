using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenKey : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D other)
    {
        PlayerController controller = other.GetComponent<PlayerController>();

        if (controller != null)
        {
            controller.GotKey();
            Destroy(gameObject);
        }
    }
}
