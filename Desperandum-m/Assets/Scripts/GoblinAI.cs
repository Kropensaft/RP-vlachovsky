using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class GoblinAI : MonoBehaviour
{

    public GameObject projectile;
    public Projectile projectileSc;
    public GoblinMovement goblinMovement;
    public Character player;
    public GameObject goblin;
    public Rigidbody2D rigid;
    public Animator animator;
    public float maxHealth = 60f;
    public bool isDead;
    public float currentHealth;

    // Start is called before the first frame update

   
    void Start()
    {
        currentHealth = maxHealth;

        
        rigid = GetComponent<Rigidbody2D>();
        goblinMovement = GetComponent<GoblinMovement>();
        Projectile projectileSc = GetComponent<Projectile>();
        player = GameObject.FindObjectOfType(typeof(Character)) as Character;
        animator = GetComponent<Animator>();

        isDead = false;
    }

    void TakeDamage()
    {
        
       currentHealth -= projectileSc.damage;
        if (currentHealth <= 0f)
        {
            Die();
            isDead = true; 

        }
    }
    // Update is called once per frame
    void Update()
    {
       
             
    }


    void Die()
    {
        rigid.bodyType = RigidbodyType2D.Static;
        animator.SetTrigger("IsDead");
        Destroy(gameObject, 1.2f);
        //skóre je int ty lopato pøestaò to tam furt dávat
        player.score += 10;
    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("projectile"))
        {
            TakeDamage();
        }
    }


  

}
