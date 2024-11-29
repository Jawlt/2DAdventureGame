using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : MonoBehaviour
{
    public GameObject player;
    private Rigidbody2D rigid2D;
    float force = 4f;
    float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        rigid2D = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Ruby");

        Vector3 direction = player.transform.position - transform.position;
        rigid2D.velocity = new Vector2(direction.x, direction.y).normalized * force;

        float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        Vector3 direction = player.transform.position - transform.position;
        rigid2D.velocity = new Vector2(direction.x, direction.y).normalized * force;

        float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);

        if (timer > 10)
        {
            Destroy(gameObject);
        }
    }

    public void Destruct()
    {
        Destroy(gameObject);
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
