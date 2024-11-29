using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public GameObject doorUp;
    public GameObject doorDown;
    DoorController door;
    public Animator animator;

    bool pressed = false;
    bool boxOn = false;

    // Start is called before the first frame update
    void Start()
    {
        door = doorDown.GetComponent<DoorController>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!pressed && collision.CompareTag("Box"))
        {
            door.SetMoveDown();
            animator.SetBool("isPressed", true);
            pressed = true;
            boxOn = true;
            if (LevelManager.Instance != null)
            {
                LevelManager.Instance.levelFinished = true;

            }
        }

        if (!boxOn && collision.CompareTag("Ruby"))
        {
            door.SetMoveDown();
            animator.SetBool("isPressed", true);
            if (LevelManager.Instance != null)
            {
                LevelManager.Instance.levelFinished = true;

            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (pressed && collision.CompareTag("Box"))
        {
            animator.SetBool("isPressed", false);
            door.SetMoveBack();
            pressed = false;
            boxOn = false;
            if (LevelManager.Instance != null)
            {
                LevelManager.Instance.levelFinished = false;

            }
        }

        if (!boxOn && collision.CompareTag("Ruby"))
        {
            animator.SetBool("isPressed", false);
            door.SetMoveBack();
            if (LevelManager.Instance != null)
            {
                LevelManager.Instance.levelFinished = false;

            }
        }
    }
}
