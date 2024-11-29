using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public bool unlockedUp;
    public bool unlockedDown;
    public bool moveable;
    float timer = 0;
    float moveTime = 5f;

    bool isUp;
    bool isDown;

    Rigidbody2D rigid2D;

    void Start()
    {
        rigid2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = rigid2D.position;

        if (unlockedUp && timer < moveTime && moveable)
        {
            position.y = position.y + Time.deltaTime * 3f;
            timer += Time.deltaTime;
        }

        if (unlockedDown && timer < moveTime && moveable)
        {
            position.y = position.y - Time.deltaTime * 3f;
            timer += Time.deltaTime;
        }

        if (timer > moveTime)
        {
            timer = 0;
            moveable = false;
        }

        rigid2D.MovePosition(position);
    }

    public void Reverse()
    {
        if (unlockedUp)
        {
            unlockedUp = false;
            unlockedDown = true;
            return;
        }

        if (unlockedDown)
        {
            unlockedDown = false;
            unlockedUp = true;
        }
    }

    public void SetMoveUp()
    {
        if (timer > 0)
        {
            timer = moveTime - timer;
            unlockedUp = true;
            unlockedDown = false;
            moveable = true;
        }
        else
        {
            unlockedUp = true;
            unlockedDown = false;
            moveable = true;
            timer = 0.01f;
        }
    }

    public void SetMoveDown()
    {
        if (timer > 0)
        {
            timer = moveTime - timer;
            unlockedUp = false;
            unlockedDown = true;
            moveable = true;
        }
        else
        {
            unlockedUp = false;
            unlockedDown = true;
            moveable = true;
            timer = 0.01f;
        }
    }

    public void SetMoveBack()
    {
        if (timer > 0)
        {
            timer = moveTime - timer + 0.002f;
            Reverse();
        }
        else
        {
            moveable = true;
            Reverse();
        }
    }

}
