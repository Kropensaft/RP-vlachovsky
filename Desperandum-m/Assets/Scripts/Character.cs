using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;



public class Character : MonoBehaviour
{

    [SerializeField] GameObject beans;

    [SerializeField] GameObject fuelCan;

    public float speed;
    public float maxHealth = 100;
    public float currentHealth;
    public LampFuel FuelBar;

    public HealthBar healthBar;


    [SerializeField] private LayerMask platformsLayerMask;
    private Rigidbody2D rigid;
    private BoxCollider2D boxCollider2d;
    private Animator animator;


    //Facing Direction

    public Flip flip;


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
    public int score;
    public Flashlight flashlight;
    public Text ScoreText;

    float beanHealth;
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
        flip = GetComponent<Flip>();

        Flashlight flashlight = GetComponent<Flashlight>();

        healthBar.SetMaxHealth(maxHealth);
        score = 0;
        beanHealth = 15f;
        ScoreText.text = "Score: "+ score;

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
        ScoreText.text = "Score: " + score;

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

        if(currentHealth <= 0)
        {
            Die();
        }

    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
    }
    
    void Die()
    {
        
            animator.Play("Base Layer.Death", 0, 1f);
            Destroy(gameObject, 2f);
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {



        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (collision.gameObject.name == "fuelTank")
        {
            if (FuelBar.fuel > FuelBar.MaxFuel)
            {
                FuelBar.fuel = FuelBar.MaxFuel;
            }



            //If the GameObject's name matches the one you suggest, output this message in the console
            Debug.Log("Restored 25% of fuel");
            FuelBar.FuelBar.value += 75f;
            FuelBar.AddFuel();
            Destroy(fuelCan);
      

        }

        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        else if (collision.gameObject.tag == "Fuel")
        {
            if (FuelBar.fuel > FuelBar.MaxFuel)
            {
                FuelBar.fuel = FuelBar.MaxFuel;
            }



            //If the GameObject's name matches the one you suggest, output this message in the console
            Debug.Log("Restored 25% of fuel");
            FuelBar.FuelBar.value += 75f;
            FuelBar.AddFuel();
            Destroy(fuelCan);
            



        }



        else if (collision.gameObject.name == "healingBeans")
        {
            if (currentHealth > maxHealth)
            {
               currentHealth = maxHealth;
            }



            //If the GameObject's name matches the one you suggest, output this message in the console
            Debug.Log("Restored 15 HP");
            healthBar.SetHealth(currentHealth + beanHealth);
            Destroy(beans);


        }

        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        else if (collision.gameObject.tag == "Heal")
        {

            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }



            //If the GameObject's name matches the one you suggest, output this message in the console
            Debug.Log("Restored 15 HP");
            healthBar.SetHealth(currentHealth + beanHealth);
            Destroy(beans);




        }

    }


}