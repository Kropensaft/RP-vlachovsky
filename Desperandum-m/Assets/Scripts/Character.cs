using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



public class Character : MonoBehaviour
{


    public float speed;
    public int maxHealth = 100;
    public int currentHealth;
    public int maxFuel = 20;
    public int currentFuel;

    public LampFuel FuelBar;

    public HealthBar healthBar;


    [SerializeField] private LayerMask platformsLayerMask;
    private Rigidbody2D rigid;
    private BoxCollider2D boxCollider2d;
    private Animator animator;

  
    //Facing Direction
  
    public Flip flip;

    [SerializeField] GameObject flashlight;

    [HideInInspector]
    public bool isFacingLeft;

    //Fall state values
    public bool IsFalling = false;
    public float FallTreshold = -10f;
    public float jumpVelocity = 4f;


    //Attack func.
    public bool IsAttacking = false;
    float DamagePoint;
    public Transform attackPoint;
    public float score;
   



    // Start is called before the first frame update
    void Start()
    {
        Initialization();
    }




    protected virtual void Initialization()
    {
        rigid = transform.GetComponent<Rigidbody2D>();
        boxCollider2d = transform.GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        currentFuel = maxFuel;
        flip = GetComponent<Flip>();



        healthBar.SetMaxHealth(maxHealth);
        score = 0f;
        FuelBar.MaxFuel = maxFuel;


    }






    float horizontal = 0f;
    public float runSpeed = 1.5f;




    void FixedUpdate()
    {
        

            horizontal = Input.GetAxisRaw("Horizontal") * runSpeed;
            float vertical = Input.GetAxis("Vertical");

            animator.SetFloat("Speed", Mathf.Abs(horizontal));
            Vector2 movement = new Vector2(horizontal, vertical);

            rigid.AddForce(movement * speed);



        





        rigid.velocity = new Vector2(horizontal * speed * Time.deltaTime, rigid.velocity.y);



    }

    // Update is called once per frame

    void Update()
    {

            //Collision Check

            bool IsGrounded()
        {
            RaycastHit2D raycastHit2d = Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0f, Vector2.down, .1f, platformsLayerMask);
           // Debug.Log(raycastHit2d.collider);
            return raycastHit2d.collider != null;
        }

        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            rigid.velocity = Vector2.up * jumpVelocity;
            animator.Play("Base Layer.Jump", 0, 1f);
        }

        //Check for Fall State
        if (rigid.velocity.y < 0 && !IsGrounded())
        {
            IsFalling = true;
            Debug.Log("Character is falling");
            animator.Play("Base Layer.Fall", 0, 0.25f);

        }
        else
        {
            IsFalling = false;

        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
    }


   
       
    
}

