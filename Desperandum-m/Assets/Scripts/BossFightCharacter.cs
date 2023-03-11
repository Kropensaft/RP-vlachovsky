using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;


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

    //Elemental variables
    public bool isSlowed;
    public bool isStunned;
    public float slowAmount;


    private float activeMoveSpeed;
    public float dashLength = .5f, dashCooldown = 1f;
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

        healthbar.SetMaxHealth(maxHealth);
        pause= GetComponent<Pause>();
       
        _trail = GetComponent<TrailRenderer>();
       
        activeMoveSpeed = MoveSpeed;
    }

    // Update is called once per frame
    private void Update()
    {
        //Move using wsad
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");

        moveInput.Normalize();

        rb.velocity = moveInput * activeMoveSpeed;

        if(isStunned) { activeMoveSpeed = 0f; }

        if(isDashing)
        {
           _trail.emitting = true;
        }
        else {_trail.emitting = false; }

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
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthbar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            rb.bodyType = RigidbodyType2D.Static;
            Die();
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

        if(collision.tag == "LightningBolt" && !isDashing && !arabis.QTEactive)
        {
            ApplyStun(lightningattack.stunDuration);
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
    
    private void Die()
    {
    }
    public void ApplySlow(float slowAmount, float duration)
    {
        if (!isSlowed)
        {
            isSlowed = true;
            MoveSpeed *= slowAmount;
            StartCoroutine(RemoveSlow(duration));
        }
        else
        {
            StopCoroutine("RemoveSlow");
            StartCoroutine(RemoveSlow(duration));
        }
    }

    private IEnumerator RemoveSlow(float duration)
    {
        yield return new WaitForSeconds(duration);
        MoveSpeed /= slowAmount;
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
}