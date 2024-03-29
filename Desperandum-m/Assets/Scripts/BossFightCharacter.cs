﻿using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BossFightCharacter : MonoBehaviour
{
    public HealthBar healthbar;
    public ScreenShake screenshake;
    public LightningAttack lightningattack;
    public Arabis arabis;
    public int arabisDamage;
    private Rigidbody2D rb;
    private Pause pause;
    private Animator animator;
    public float MoveSpeed;
    private Vector2 moveInput;
    public TrailRenderer _trail;

    //Dash ability
    public float dashSpeed = 6f;

    public AudioSource audSource;
    public AudioClip hurtAudio;

    //Elemental variables
    public bool isSlowed;

    public bool isStunned;
    public float slowDuration;
    public float slowAmount;

    public GameObject deathText;

    public Image stunVignette;
    public Image deathVignette;
    public GameObject stunnedText;

    public bool isDead;

    //Dash Slider variables
    public Vector3 lastPos;

    private float activeMoveSpeed;
    public float dashLength = .5f, dashCooldown;
    public bool isDashing;
    private float dashCounter;
    private float DashCooldownCounter;
    public float bladeDamage;
    public float currentHealth;
    public float maxHealth = 100f;

    // Use this for initialization
    private void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Arabis arabis = GetComponent<Arabis>();
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;
        audSource = GetComponent<AudioSource>();
        healthbar.SetMaxHealth(maxHealth);
        pause = GetComponent<Pause>();

        _trail = GetComponent<TrailRenderer>();
        Vector4 color = new Vector4(0, 0, 0, 0);
        stunVignette.color = color;
        stunnedText.SetActive(false);
        activeMoveSpeed = MoveSpeed;
    }

    public static BossFightCharacter Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        lastPos = new Vector3(transform.position.x, transform.position.y, -2);

        //Move using wsad
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");

        moveInput.Normalize();

        rb.velocity = moveInput * activeMoveSpeed;

        if (isStunned)
        {
            activeMoveSpeed = 0f;
            stunVignette.enabled = true;
            stunnedText.SetActive(true);
            stunVignette.color = new Color(0, 0, 0, .5f);
        }
        else
        {
            stunVignette.enabled = false;
            stunnedText.SetActive(false);
        }

        if (isSlowed) { ApplySlow(slowAmount, slowDuration); }

        if (isDashing)
        {
            _trail.emitting = true;
        }
        else { _trail.emitting = false; }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (DashCooldownCounter <= 0 && dashCounter <= 0)
            {
                isDashing = true;
                activeMoveSpeed = dashSpeed;
                dashCounter = dashLength;
            }
        }

        if (dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;

            if (dashCounter <= 0)
            {
                activeMoveSpeed = MoveSpeed;
                DashCooldownCounter = dashCooldown;
            }
        }
        if (DashCooldownCounter > 0)

        {
            isDashing = false;
            DashCooldownCounter -= Time.deltaTime;
        }
        if (isDead)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("E pressed");
                SceneManager.LoadScene("ArabisArena");
            }
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthbar.SetHealth(currentHealth);
        audSource.PlayOneShot(hurtAudio);

        if (currentHealth <= 0)
        {
            isDead = true;
            rb.bodyType = RigidbodyType2D.Static;
            deathVignette.enabled = true;
            deathText.SetActive(true);
            StopAllCoroutines();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Fireball" && !isDashing && !arabis.QTEactive)
        {
            Debug.Log("Hit hp-");
            TakeDamage(arabisDamage);
            Destroy(collision);
            screenshake.TriggerShake();
        }

        if (collision.tag == "LightningBolt" && !isDashing && !arabis.QTEactive)
        {
            ApplyStun(lightningattack.stunDuration);
            Debug.Log("Stunned");
        }

        if (collision.tag == "Snowfall" && !isDashing && !arabis.QTEactive)
        {
            ApplySlow(slowAmount, slowDuration);
            Debug.Log("Slowed");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "blade" && !isDashing && !arabis.QTEactive)
        {
            Debug.Log("Blade hit");
            TakeDamage(bladeDamage);
            screenshake.TriggerShake();
        }
    }

    public void ApplySlow(float slowAmount, float duration)
    {
        if (!isSlowed)
        {
            activeMoveSpeed *= (1 - slowAmount);
            isSlowed = true;
            slowDuration = duration;
            StopCoroutine("RemoveSlow");
            StartCoroutine(RemoveSlow(duration));
        }
    }

    private IEnumerator RemoveSlow(float duration)
    {
        yield return new WaitForSeconds(duration);
        activeMoveSpeed /= (1 - slowAmount);
        isSlowed = false;
    }

    public void ApplyStun(float duration)
    {
        if (!isStunned)
        {
            isStunned = true;

            StartCoroutine(RemoveStun(duration));
        }
        else
        {
            StopCoroutine("RemoveStun");
            StartCoroutine(RemoveStun(duration));
        }
        // Play sound effect or visual effect for stun
    }

    private IEnumerator RemoveStun(float duration)
    {
        yield return new WaitForSeconds(duration);
        isStunned = false;
    }

    public void Death()
    {
    }
}