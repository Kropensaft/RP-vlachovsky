using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Arabis : MonoBehaviour
{
    // The boss's health
    public int health = 3;

    private string creditsScene = "CreditsScene";
    public Transform firePos1;
    public Transform firePos2;
    public Transform firePos3;
    public KeySpamDetector KSD;
    public GameObject QTE;
    public GameObject BladeDance;
    public ArabisBladeDance BladeDanceScrpt;
    public BossFightCharacter player;
    public GameObject blade;
    public LightningAttack lightningAttack;
    public ColdAttack coldAttack;
    public Image life;
    public Image life2;
    public Image life3;

    public Vector2 direction = Vector2.right;

    public Rigidbody2D rigid;

    public Transform playerTransform;

    public int BeamSpeed;

    public float maxBallSpawnDistance;

    // Time in seconds after which the fireballs will stop spawning
    public float stopSpawningTime = 5f;

    // Timer for the fireballs
    private float fireballTimer = 0f;

    // The boss's attack damage
    public int attackDamage = 5;

    // The boss's attack rate
    public float attackRate = 1f;

    // The time since the boss last attack
    private float timeSinceLastAttack = 0.0f;

    // The prefab for the projectile to spawn
    public GameObject projectilePrefab;

    //public DialogueSystem dialogueSystem;
    //public Canvas canvas;

    // The starting speed of the projectiles
    public float projectileStartSpeed = 1.0f;

    // The acceleration rate of the projectiles
    public float projectileAcceleration = 0.1f;

    //end of phase check
    public float elapsedTime;

    public float finalPhaseTimer;
    public float PhaseOneDuration;
    public float PhaseTwoDuration;
    public float Phase1a2diff = 15f;
    public float finalPhaseBladeSpawn;
    public int QTEcompleted = 0;
    public bool QTEactive;
    public float finalTime;
    public float lightningSpawnInterval;
    public float time;
    public float _1stPhaseDialogueTime = 0f;
    public float finalPhaseTimeABlades;
    public Animator animator;
    public bool finalPhaseFlag;

    private void Start()
    {
        Canvas canvas = GetComponent<Canvas>();
        KeySpamDetector KSD = GetComponent<KeySpamDetector>();
        ArabisBladeDance BladeDanceScrpt = GetComponent<ArabisBladeDance>();
        LightningAttack lightningAttack = GetComponent<LightningAttack>();
        ColdAttack coldAttack = GetComponent<ColdAttack>();
        Animator animator = GetComponent<Animator>();
        BossFightCharacter player = GetComponent<BossFightCharacter>();
        //DialogueSystem dialogueSystem = GetComponent<DialogueSystem>();

        BladeDance.SetActive(false);
        //dialogueSystem.StartDialogue(0);
    }

    private void Update()
    {
        finalTime += Time.deltaTime;
        timeSinceLastAttack -= Time.deltaTime;
        //if(dialogueSystem.dialogueEnded)
        //{
        //    _1stPhaseDialogueTime = 0f;
        //}

        if (QTEcompleted == 1 && elapsedTime >= PhaseOneDuration + 5 && elapsedTime <= PhaseOneDuration + Phase1a2diff + PhaseTwoDuration)
        {
            // dialogueSystem.StartDialogue(1);

            if (elapsedTime >= PhaseOneDuration + Phase1a2diff)
            {
                PhaseTwo();
            }
        }
        if (QTEcompleted == 2 && !QTEactive)
        {
            if (!finalPhaseFlag)
            {
                BladeDanceScrpt.DestroyBlades();
                BladeDance.SetActive(false);
            }

            finalPhaseTimer += Time.deltaTime;

            if (finalPhaseTimer + 10 < finalPhaseBladeSpawn)
            {
                if (timeSinceLastAttack <= 0.0f)
                {
                    PhaseOne(firePos1.position);
                    PhaseOne(firePos2.position);
                    PhaseOne(firePos3.position);

                    timeSinceLastAttack = attackRate;
                }
                if (time >= lightningSpawnInterval)
                {
                    PhaseThree();

                    time = 0f;
                }
            }
            if (finalPhaseTimer > finalPhaseBladeSpawn)
            {
                finalPhaseFlag = true;
                PhaseTwo();
                if (finalPhaseTimer >= finalPhaseTimeABlades)
                {
                    BladeDanceScrpt.DestroyBlades();
                    BladeDance.SetActive(false);
                    QTE.SetActive(true);
                    QTEactive = true;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        // Decrement the time since the last attack
        timeSinceLastAttack -= Time.deltaTime;
        elapsedTime += Time.deltaTime;

        time += Time.deltaTime;
        // Check if the boss is ready to attack
        if (timeSinceLastAttack <= 0.0f)
        {
            // Attack by spawning a projectile
            if (elapsedTime < PhaseOneDuration && !QTEactive && !player.isDead)
            {
                PhaseOne(playerTransform.position);

                if (time >= lightningSpawnInterval)
                {
                    PhaseThree();

                    time = 0f;
                }
            }

            timeSinceLastAttack = attackRate;
        }

        if (elapsedTime >= PhaseOneDuration && QTEcompleted == 0)
        {
            //1st phase QTE
            QTEactive = true;
            QTE.SetActive(true);
        }

        if (elapsedTime >= PhaseOneDuration + PhaseTwoDuration + Phase1a2diff && QTEcompleted == 1)
        {
            //2nd phase QTE

            QTEactive = true;
            QTE.SetActive(true);

            Debug.Log("second Phase QTE started");
        }
        if (elapsedTime >= PhaseOneDuration + PhaseTwoDuration + Phase1a2diff + finalPhaseTimeABlades && QTEcompleted == 2)
        {
            //2nd phase QTE

            Debug.Log("third Phase QTE started");
        }
    }

    // Spawn a projectile

    // Reduce the boss's health and check for death
    public void TakeDamage1()
    {
        Debug.Log("Arabis Damaged");
        QTEcompleted = 1;
        health = 2;
        KSD.fillBar.value = 0f;
        KSD.barFill = KSD.fillBar.value;

        life.enabled = (false);
    }

    public void TakeDamage2()
    {
        Debug.Log("Arabis Damaged second time");
        QTEcompleted = 2;
        health = 1;
        KSD.fillBar.value = 0f;
        KSD.barFill = KSD.fillBar.value;

        life2.enabled = (false);
    }

    public void TakeDamage3()
    {
        QTEcompleted = 3;
        health = 0;
        KSD.fillBar.value = 0f;
        KSD.barFill = KSD.fillBar.value;

        life3.enabled = (false);
        EndGame();
    }

    public void PhaseOne(Vector3 playerTransform)
    {
        // Check if the time since the last attack is greater than or equal to the attack rate
        if (timeSinceLastAttack >= attackRate)
        {
            // Reset the time since the last attack
            timeSinceLastAttack = 0f;
        }

        if (fireballTimer < stopSpawningTime)
        {
            // Specify the number of projectiles to spawn
            int numProjectiles = 15;

            // Calculate the angle between each projectile
            float angleBetweenProjectiles = 360f / numProjectiles;

            // Initialize the angle of the first projectile to 0
            float angle = 0f;

            // Spawn the projectiles
            for (int i = 0; i < numProjectiles; i++)
            {
                // Convert the angle to a direction vector
                Vector2 direction = Quaternion.Euler(0, 0, angle) * Vector2.up;

                // Instantiate the projectile at the boss's position
                GameObject projectile = Instantiate(projectilePrefab, firePos1.position, Quaternion.identity);
                projectile.transform.position = Vector3.MoveTowards(projectile.transform.position, playerTransform, 5f);

                // Set the projectile's starting speed
                projectile.GetComponent<ArabisProjectile>().speed = projectileStartSpeed;

                // Set the projectile's acceleration rate
                projectile.GetComponent<ArabisProjectile>().acceleration = projectileAcceleration;

                // Set the projectile's direction
                projectile.GetComponent<ArabisProjectile>().direction = direction;

                // Increment the angle of the next projectile
                angle += angleBetweenProjectiles;
            }
        }
    }

    // Spawn projectiles in a sinusoidal pattern at the specified position
    private void PhaseTwo()
    {
        BladeDance.SetActive(true);
    }

    private void PhaseThree()
    {
        lightningAttack.StartAttack();
        lightningAttack.StartAttack();
        lightningAttack.StartAttack();
        coldAttack.StartAttack();
        coldAttack.StartAttack();
        coldAttack.StartAttack();
    }

    // End the game

    private void EndGame()
    {
        animator.SetBool("GameEnded", true);

        Invoke("LoadCredits", 5f);
    }

    private void LoadCredits()
    {
        SceneManager.LoadScene(creditsScene);
    }
}