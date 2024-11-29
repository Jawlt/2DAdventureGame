using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    // Variables related to player character movement
    public InputAction MoveAction;
    Rigidbody2D rigidbody2d;
    Vector2 move;
    public float speed = 5.0f;
    bool isSpeedBoostActive = false;
    public bool isStunned = false;
    public float stunDuration = 1.5f;

    // Varaibles related to Dashing
    public float dashSpeedMultipler = 4f;
    public float dashDistance = 2f;
    public float dashCooldown = 0.1f;
    private bool canDash = true;

    // Variables related to the health system
    public int maxHealth = 5;
    public int health { get { return currentHealth; } }
    int currentHealth;
    bool canHealOnce = true;

    // Variables related to temporary invincibility
    public float timeInvincible = 2.0f;
    public bool isInvincible;
    float damageCooldown;

    // Variables related to Animation
    Animator animator;
    Vector2 moveDirection = new Vector2(1, 0);

    // Variables related to Projectile
    public GameObject projectilePrefab;
    Vector2 lookDirection;
    public bool rapidFire = false;
    public bool multiShot = false;
    public float fireCooldown = 0.2f; // Time between consecutive projectiles
    private float fireTimer = 0f; // Timer to track the cooldown

    // Variables related to Sound
    AudioSource audioSource;
    public AudioSource playerWalkClip;
    [SerializeField] AudioClip throwsProjectileClip;
    [SerializeField] AudioClip playerTakesDmgClip;

    // Variable related to Golden Key
    public bool hasKeys = false;
    private int keyCount = 0;

    //Varaibles related to Pause and Game Status
    public bool isPaused = false;

    private void Awake()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        MoveAction.Enable();
        rigidbody2d = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = true;
        }

        if (isStunned)
        {
            stunDuration -= Time.deltaTime;
            speed = 0.0f;
            animator.SetFloat("Look X", 0);
            animator.SetFloat("Look Y", 0);
            animator.SetFloat("Speed", 0);
            playerWalkClip.enabled = false;
            if (stunDuration < 0)
            {
                speed = 5.0f;
                isStunned = false;
            }
        }

        if (!isPaused && !isStunned)
        {
            move = MoveAction.ReadValue<Vector2>();

            if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
            {
                moveDirection.Set(move.x, move.y);
                moveDirection.Normalize();
            }

            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                playerWalkClip.enabled = true;
            }
            else
            {
                playerWalkClip.enabled = false;
            }

            animator.SetFloat("Look X", moveDirection.x);
            animator.SetFloat("Look Y", moveDirection.y);
            animator.SetFloat("Speed", move.magnitude);

            if (isInvincible)
            {
                damageCooldown -= Time.deltaTime;
                if (damageCooldown < 0)
                {
                    isInvincible = false;
                }
            }

            isDead();

            fireTimer += Time.deltaTime;
            if (rapidFire && Input.GetMouseButton(1) && fireTimer >= fireCooldown)
            {
                Launch();
                fireTimer = 0f;
            }

            if (Input.GetMouseButtonDown(1))
            {
                Launch();
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                FindFriend();
            }

            if (Input.GetKeyDown(KeyCode.Space) && canDash)
            {
                isInvincible = true;
                damageCooldown += 0.2f;
                StartCoroutine(Dash());
            }

            //Heals 10 hp (Mainly for the boss fight, but full hp for any other level)
            if (Input.GetKeyDown(KeyCode.H) && canHealOnce)
            {
                StartCoroutine(HealForDuration(5f));
            }
        }
    }

    // FixedUpdate has the same call rate as the physics system 
    void FixedUpdate()
    {
        Vector2 position = (Vector2)rigidbody2d.position + move * speed * Time.deltaTime;
        rigidbody2d.MovePosition(position);
    }

    public IEnumerator Dash()
    {
        canDash = false;
        float originalSpeed = speed; // Store the original speed
        speed *= dashSpeedMultipler; // Apply the dash speed multiplier

        yield return new WaitForSeconds(dashCooldown); // Wait for the duration of the boost

        speed = originalSpeed; // Restore the original speed
        canDash = true;
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible)
            {
                return;
            }
            isInvincible = true;
            damageCooldown = timeInvincible;
            animator.SetTrigger("Hit");
            PlaySound(playerTakesDmgClip);
            speed = 5.0f;
        }
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UIHandler.Instance.SetHealthValue(currentHealth / (float)maxHealth);

        isDead();
    }

    public void isDead()
    {
        if (currentHealth <= 0)
        {
            isPaused = true;
            speed = 0;
            animator.SetFloat("Look X", 0);
            animator.SetFloat("Look Y", 0);
            animator.SetFloat("Speed", 0);
            Time.timeScale = 0;
            AudioListener.pause = true;
            menuController.Instance.RestartMenu();
        }
    }

    public void MaxHealth()
    {
        currentHealth = maxHealth;
        UIHandler.Instance.SetHealthValue(currentHealth / (float)maxHealth);
    }

    void Launch()
    {
        // Get the mouse position
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Calculate the direction from the player to the mouse
        lookDirection = mousePos - transform.position;

        if (multiShot)
        {
            // Offset positions for multishot
            Vector2 leftOffset = new Vector2(-0.5f, 0f); // Slightly to the left
            Vector2 rightOffset = new Vector2(0.5f, 0f); // Slightly to the right

            // Create the left projectile
            GameObject leftProjectile = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f + leftOffset, Quaternion.identity);
            Projectile leftProj = leftProjectile.GetComponent<Projectile>();
            leftProj.Launch(lookDirection, 200);

            // Create the right projectile
            GameObject rightProjectile = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f + rightOffset, Quaternion.identity);
            Projectile rightProj = rightProjectile.GetComponent<Projectile>();
            rightProj.Launch(lookDirection, 200);

            // Trigger animation and sound
            animator.SetTrigger("Launch");
            PlaySound(throwsProjectileClip);
        }
        else
        {
            // Single shot
            GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
            Projectile projectile = projectileObject.GetComponent<Projectile>();
            projectile.Launch(lookDirection, 200);
            animator.SetTrigger("Launch");
            PlaySound(throwsProjectileClip);
        }

    }

    void FindFriend()
    {
        RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, moveDirection, 1.5f, LayerMask.GetMask("NPC"));
        if (hit.collider != null)
        {
            NonPlayerCharacter character = hit.collider.GetComponent<NonPlayerCharacter>();
            if (character != null)
            {
                UIHandler.Instance.NPCDisplayDialogue();
            }
        }
    }

    // Simple Speed Boost implementation
    public void ActivateSpeedBoost(float duration, float multiplier)
    {
        if (!isSpeedBoostActive) // Prevent multiple boosts
        {
            StartCoroutine(SpeedBoostCoroutine(duration, multiplier));
        }
    }

    private IEnumerator SpeedBoostCoroutine(float duration, float multiplier)
    {
        isSpeedBoostActive = true;
        float originalSpeed = speed; // Store the original speed
        speed *= multiplier; // Apply the speed multiplier

        yield return new WaitForSeconds(duration); // Wait for the duration of the boost

        speed = originalSpeed; // Restore the original speed
        isSpeedBoostActive = false; // Reset the boost flag
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    public void GotKey()
    {
        keyCount++;
        if (keyCount == 2)
        {
            hasKeys = true;

            // Call EnemyDefeated on the LevelManager singleton
            if (LevelManager.Instance != null)
            {
                LevelManager.Instance.LevelCompleted();
            }

            if (UIHandler.Instance != null)
            {
                UIHandler.Instance.SetGoldenKeyVisible(true);
            }
        }

        if (CanvasManager.Instance != null)
        {
            CanvasManager.Instance.UpdateGoldenKeysText(keyCount);
        }
    }

    public bool getHasKeys()
    {
        return hasKeys;
    }

    public void multiShotUpgrade(bool upgradeNow)
    {
        multiShot = true;
    }

    public void rapidFireUpgrade(bool upgradeNow)
    {
        rapidFire = true;
    }
    private System.Collections.IEnumerator HealForDuration(float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            Heal(); // Call the Heal function every second
            yield return new WaitForSeconds(1f); // Wait for 1 second
            elapsedTime += 1f;
        }
        canHealOnce = false;
    }

    public void Heal()
    {
        ChangeHealth(2);
    }
}
