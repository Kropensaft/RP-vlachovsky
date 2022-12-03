using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tutorialMovement : MonoBehaviour
{

    public Animator animator;
    public Rigidbody2D rigid;
    public BoxCollider2D boxCollider2d;
    //move variables
    [SerializeField] private LayerMask platformsLayerMask; // slouží pro grounded check
    [HideInInspector] public bool isFacingLeft;
    public float speed;
    float horizontal = 0f;
    public float runSpeed = 1.5f;

    //Sliders 
    public LampFuel fuelBar;
    public HealthBar healthBar;

    //fuel
    public float maxFuel = 300f;
    public float currentFuel;
    public float fuelTankCapacity = 100f;


    //health
    public Image lowHealthBorder;
    public float currentHealth;
    public float maxHealth = 100f;
    public bool IsDead;


    //Fall state values
    public bool IsFalling = false;
    public float FallTreshold = -10f;
    public float jumpVelocity = 4f;
    private bool DoubleJump;

    //"Combat" variables
    public Flashlight flashlight;
    public Text ScoreText;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        boxCollider2d = GetComponent<BoxCollider2D>();
        flashlight = GetComponent<Flashlight>();

        currentHealth = maxHealth;
        currentFuel = maxFuel;
        

        healthBar.SetMaxHealth(maxHealth);
        fuelBar.SetMaxFuel(maxFuel);
    }


    void UpdateFuel()
    {
        if (flashlight.flashlightLoaded)
        {

            currentFuel -= 0.1f;
            fuelBar.SetFuel(currentFuel);

        }

        else if (!flashlight.flashlightLoaded)
        {
            fuelBar.SetFuel(currentFuel);
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateFuel();

        bool IsGrounded()
        {
            RaycastHit2D raycastHit2d = Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0f, Vector2.down, .1f, platformsLayerMask);
            // Debug.Log(raycastHit2d.collider);
            animator.SetBool("IsGrounded", true);
            return raycastHit2d.collider != null;

        }

        //Jump function

        if (IsGrounded() && !Input.GetKey(KeyCode.Space))
        {
            DoubleJump = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (IsGrounded() || DoubleJump)
            {
                rigid.velocity = new Vector2(rigid.velocity.x, jumpVelocity);

                DoubleJump = !DoubleJump;
            }

        }

        if (Input.GetKeyDown(KeyCode.Space) && rigid.velocity.y > 0f)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, rigid.velocity.y * 1.2f);
        }

        //Check for Fall State
        if (rigid.velocity.y < 0 && !IsGrounded())
        {

            IsFalling = true;
            Debug.Log("Character is falling");
            animator.SetBool("IsFalling", true);


        }
        else
        {

            animator.SetBool("IsFalling", false);
            IsFalling = false;

        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {


        //Add fuel if you collide with fuel tank
        if (collision.gameObject.name == "fuelTank")
        {
            Debug.Log("Restored 75 fuel");
            fuelBar.SetFuel(currentFuel += fuelTankCapacity);
            collision.gameObject.SetActive(false);

            if (currentFuel > maxFuel)
            {
                currentFuel = maxFuel;
            }

        }



   
    }



    void FixedUpdate()
    {


        horizontal = Input.GetAxisRaw("Horizontal") * runSpeed;
        float vertical = Input.GetAxis("Vertical");

        animator.SetFloat("speed", Mathf.Abs(horizontal));
        Vector2 movement = new Vector2(horizontal, vertical);

        rigid.AddForce(movement * speed);
        rigid.velocity = new Vector2(horizontal * speed * Time.deltaTime, rigid.velocity.y);


        

    }
}
