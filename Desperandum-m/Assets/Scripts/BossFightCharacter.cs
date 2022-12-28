﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.WSA;

public class BossFightCharacter : MonoBehaviour
    {

    public int arabisDamage = 5;
    private Rigidbody2D rb;
    public float speed;

    //Dash ability 
    public float dashSpeed = 6f;
    public float dashDuration = 0.3f;
    private bool isDashing = false;
    private float dashTimer = 0f;


    public float currentHealth;
    public float maxHealth = 100f;

    // Use this for initialization
    void Start()
        {
            currentHealth = maxHealth;
            rb = GetComponent<Rigidbody2D>();
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
            UnityEngine.Cursor.visible = false;

    }

        // Update is called once per frame
        void Update()
        {
        //Move using wsad
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(horizontalInput, verticalInput);

        rb.AddForce(movement * speed);

        if(Input.GetKeyDown(KeyCode.LeftShift)) 
        {
            if (Mathf.Abs(horizontalInput) > Mathf.Abs(verticalInput))
            {
                // Dash in the horizontal direction
                if (horizontalInput > 0)
                {
                    Dash(new Vector2(1, 0));
                    
                }
                else if (horizontalInput < 0)
                {
                    Dash(new Vector2(-1, 0));
                    

                }
            }
            else
            {
                // Dash in the vertical direction
                if (verticalInput > 0)
                {
                    Dash(new Vector2(0, 1));
                   

                }
                else if (verticalInput < 0)
                {
                    Dash(new Vector2(0, -1));
                    

                }
            }
        }

        if (isDashing)
        {
            dashTimer += Time.deltaTime;

            if (dashTimer >= dashDuration)
            {
                isDashing = false;
                dashTimer = 0f;
            }
        }

    }


    public void Dash(Vector2 direction)
    {
        if (!isDashing)
        {
            rb.AddForce(direction * dashSpeed, ForceMode2D.Impulse);
            isDashing = true;
            
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            
            rb.bodyType = RigidbodyType2D.Static;
            Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Fireball" && !isDashing)
        {
            Debug.Log("Hit hp-");
            TakeDamage(arabisDamage);
            Destroy(collision);

        }
    }

    void Die()
    {

    }
}
