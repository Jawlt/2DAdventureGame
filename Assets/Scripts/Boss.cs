using UnityEngine;
using UnityEngine.UI; // Required for health bar UI

public class Boss : MonoBehaviour
{
    public static Boss Instance { get; private set; }
    public Transform[] movePoints; // Assign 4 points in the Inspector
    public float moveSpeed = 3f; // Speed of movement between points

    private int currentPointIndex = -1; // The index of the current point
    private int targetPointIndex = -1; // The index of the next target point

    [Header("Attack Settings")]
    public GameObject missilePrefab; // Assign missile prefab in the Inspector
    public GameObject missilePrefab1; // Assign missile prefab in the Inspector
    public GameObject slashPrefab; // Assign slash prefab in the Inspector
    public Transform attackSpawnPoint; // Where the missiles/slashes spawn
    public float attackInterval = 2f, radialInterval = 0.5f; // Time between attacks
    private float attackTimer = 0f, radialTimer = 0f; // Timer to track when to attack

    // Varaibles Related to Health
    HealthSystemForDummies healthSystem;

    private void Awake()
    {
        // Ensure there's only one LevelManager
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (movePoints.Length < 4)
        {
            Debug.LogError("Please assign at least 4 points in the movePoints array.");
            return;
        }

        //Initialize Health System
        healthSystem = GetComponent<HealthSystemForDummies>();
        healthSystem.OnIsAliveChanged.AddListener(BossDefeated);

        // Randomly pick the first target point
        ChooseNextPoint();
    }

    void OnTriggerStay2D(Collider2D other)
    {
        PlayerController controller = other.GetComponent<PlayerController>();

        if (controller != null)
        {
            controller.ChangeHealth(-5);
        }
    }

    void Update()
    {
        if (targetPointIndex >= 0 && movePoints.Length >= 4)
        {
            MoveToTargetPoint();
        }

        // Handle attack timing
        attackTimer += Time.deltaTime;
        radialTimer += Time.deltaTime;
        if (attackTimer >= attackInterval)
        {
            Attack(); // Trigger an attack
            attackTimer = 0f; // Reset the attack timer
        }

        if (radialTimer >= attackInterval)
        {
            RadialMissileAttack(); // Trigger an attack
            radialTimer = 0f; // Reset the attack timer
        }
    }

    void MoveToTargetPoint()
    {
        // Get the target position
        Vector2 targetPosition = movePoints[targetPointIndex].position;

        // Smoothly move towards the target position
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Check if the boss has reached the target position
        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            currentPointIndex = targetPointIndex; // Update the current point
            ChooseNextPoint(); // Choose the next random point
        }
    }

    void ChooseNextPoint()
    {
        int newPointIndex;

        do
        {
            // Randomly choose a new point index, ensuring it is not the same as the current point
            newPointIndex = Random.Range(0, movePoints.Length);
        } while (newPointIndex == currentPointIndex);

        targetPointIndex = newPointIndex; // Set the new target point index
    }

    void Attack()
    {
        int missileCount = 2; // Number of missiles to spawn
        int slashCount = 2;   // Number of slashes to spawn
        float spreadAngle = 30f; // Angle between projectiles (degrees)

        // Spawn multiple missiles
        for (int i = 0; i < missileCount; i++)
        {
            float angle = -spreadAngle / 2 + (spreadAngle / (missileCount - 1)) * i;
            Quaternion rotation = Quaternion.Euler(0, 0, angle);
            Instantiate(missilePrefab, attackSpawnPoint.position, rotation);
        }

        // Spawn multiple slashes
        for (int i = 0; i < slashCount; i++)
        {
            float angle = -spreadAngle / 2 + (spreadAngle / (slashCount - 1)) * i;
            Quaternion rotation = Quaternion.Euler(0, 0, angle);
            Instantiate(slashPrefab, attackSpawnPoint.position, rotation);
        }
    }

    void RadialMissileAttack()
    {
        int numPoints = 8; // Number of directions
        float angleIncrement = 360f / numPoints;

        for (int i = 0; i < numPoints; i++)
        {
            // Calculate the direction based on the current angle
            float angle = i * angleIncrement;
            Vector3 spawnDirection = Quaternion.Euler(0f, 0f, angle) * Vector3.up;

            // Instantiate the missile
            GameObject spawnedObject = Instantiate(missilePrefab1, transform.position, Quaternion.identity);

            // Apply velocity in the calculated direction
            Rigidbody2D missileRigidbody = spawnedObject.GetComponent<Rigidbody2D>();
            if (missileRigidbody != null)
            {
                missileRigidbody.velocity = spawnDirection * 15; // Set velocity directly
            }

            // Rotate the missile to face the movement direction
            float objectRotation = Mathf.Atan2(spawnDirection.y, spawnDirection.x) * Mathf.Rad2Deg;
            spawnedObject.transform.rotation = Quaternion.Euler(0f, 0f, objectRotation - 90f);
        }
    }

    // Method to take damage
    public void TakeDamage(int damage)
    {
        healthSystem.AddToCurrentHealth(damage);
    }
    private void BossDefeated(bool isAlive)
    {
        if (!isAlive)
        {
            if (LevelManager.Instance != null)
            {
                LevelManager.Instance.BossIsDefeated();
            }
            Destroy(gameObject); // Destroy the boss GameObject
        }
    }
}
