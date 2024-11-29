using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyController : MonoBehaviour
{
    // Public variables
    public float speed;
    public bool vertical; // Vertical or horizontal movement
    public float changeTime = 3.0f;
    public bool moveInSquare = false; // Option to toggle square rotation movement
    public ParticleSystem smokeEffect;

    // Varaibles related to Stun
    public GameObject Stun;
    public float stunCooldown;
    private bool onCooldown = true;
    private float cooldownTimer;


    // Private variables
    Rigidbody2D rigidbody2d;
    Animator animator;
    float timer;
    int direction = 1; // Direction for vertical/horizontal movement
    int directionIndex = 0; // Direction index for square movement
    bool broken = true;

    // Variables related to Sound
    AudioSource audioSource;
    public AudioClip enemyHit1Clip;
    public AudioClip enemyHit2Clip;
    public AudioClip enemyFixedClip;

    // Directions for square movement
    Vector2[] squareDirections = new Vector2[]
    {
        Vector2.up,    // Move up
        Vector2.right, // Move right
        Vector2.down,  // Move down
        Vector2.left   // Move left
    };

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        timer = changeTime;
        stunCooldown = (float)Random.Range(4, 6);
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called every frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            if (moveInSquare)
            {
                // Square movement: Increment direction index and wrap around
                directionIndex = (directionIndex + 1) % squareDirections.Length;
            }
            else
            {
                // Vertical or horizontal movement: Toggle direction
                direction = -direction;
            }

            timer = changeTime;
        }

        if (onCooldown)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer < 0)
            {
                onCooldown = false;
            }
        }

        if (!onCooldown && broken)
        {
            onCooldown = true;
            cooldownTimer = (float)Random.Range(4, 6);
            DoStunDmg();
        }
    }

    // FixedUpdate has the same call rate as the physics system
    void FixedUpdate()
    {
        if (!broken)
        {
            return;
        }

        Vector2 position = rigidbody2d.position;

        if (moveInSquare)
        {
            // Square movement
            Vector2 directionVector = squareDirections[directionIndex];
            position += directionVector * speed * Time.deltaTime;

            // Update animator for square movement
            animator.SetFloat("Move X", directionVector.x);
            animator.SetFloat("Move Y", directionVector.y);
        }
        else
        {
            // Vertical or horizontal movement
            if (vertical)
            {
                // Move up or down
                position.y += speed * direction * Time.deltaTime;
                animator.SetFloat("Move X", 0);
                animator.SetFloat("Move Y", direction);
            }
            else
            {
                // Move left or right
                position.x += speed * direction * Time.deltaTime;
                animator.SetFloat("Move X", direction);
                animator.SetFloat("Move Y", 0);
            }
        }

        rigidbody2d.MovePosition(position);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();

        if (player != null)
        {
            player.ChangeHealth(-1);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

    public void Fix()
    {
        broken = false;
        GetComponent<Rigidbody2D>().simulated = false;
        animator.SetTrigger("Fixed");
        audioSource.Stop();
        audioSource.PlayOneShot(enemyFixedClip);
        smokeEffect.Stop();

        // Call EnemyDefeated on the LevelManager singleton
        if (LevelManager.Instance != null)
        {
            LevelManager.Instance.EnemyDefeated();
        }
    }
    public void EnemyHit()
    {
        int clipNum = Random.Range(1, 3);
        if (clipNum == 1) { audioSource.PlayOneShot(enemyHit1Clip); }
        if (clipNum == 2) { audioSource.PlayOneShot(enemyHit1Clip); }
    }

    private void DoStunDmg()
    {
        GameObject spikeObject = Instantiate(Stun, rigidbody2d.position, Quaternion.identity);
    }
}
