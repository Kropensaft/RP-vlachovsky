using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;



public class Character : MonoBehaviour
{
    //Pickups 
    [SerializeField] GameObject beans;
    [SerializeField] GameObject fuelCan;
    public float beanHealth = 25f;
    public float fuelTankCapacity = 100f;
    bool RoomKeyOne;
    bool RoomKeyTwo;
    bool RoomKeyThree;

    //Saving
    public int level;


    //Sliders 
    public LampFuel fuelBar;
    public HealthBar healthBar;

    //fuel
    public float maxFuel = 300f;
    public float currentFuel;


    //health
    public Image lowHealthBorder;
    public float currentHealth;
    public float maxHealth = 100f;
    public bool IsDead;


    //move variables
    [SerializeField] private LayerMask platformsLayerMask; // slouží pro grounded check
    [HideInInspector] public bool isFacingLeft;
    public float speed;
    float horizontal = 0f;
    public float runSpeed = 1.5f;

    //Minimap
    public Image minimap;
    public RawImage miniMap;

    //Collision variables
    private Rigidbody2D rigid;
    private BoxCollider2D boxCollider2d;
    public Collider2D doorCollider;

    //Enemy variables (angel)
    public WeepingAngelAI Angel;
    public GameObject angel;


    //Animation
    public Animator animator;


    //Fall state values
    public bool IsFalling = false;
    public float FallTreshold = -10f;
    public float jumpVelocity = 4f;
    private bool DoubleJump;


    //"Combat" variables
    public Flashlight flashlight;
    public Text ScoreText;
    public int score;


    //Audio
    public AudioSource WalkAudio;
    public AudioSource HW_pickup;

    public SaveSystem data;

    void Start()
    {
        Initialization();
    }


  
    protected virtual void Initialization()
    {
        rigid = transform.GetComponent<Rigidbody2D>();
        boxCollider2d = transform.GetComponent<BoxCollider2D>();
        doorCollider = GetComponent<Collider2D>();
        flashlight = GetComponent<Flashlight>();
        Angel = GetComponent<WeepingAngelAI>();
        animator = GetComponent<Animator>();
        SaveSystem data = GetComponent<SaveSystem>();

        currentHealth = maxHealth;
        currentFuel = maxFuel;

        minimap.enabled = false;
        miniMap.enabled = false;

        Cursor.visible = false;

        WalkAudio.Pause();
       
        isFacingLeft = false;
        
        healthBar.SetMaxHealth(maxHealth);
        fuelBar.SetMaxFuel(maxFuel);
        lowHealthBorder.enabled = false;

        ScoreText.text = "Score: " + score;
        score = 0;

        RoomKeyOne = false;
        RoomKeyTwo = false;
        RoomKeyThree = false;

        SavePlayer();

    }


    public void SavePlayer()
    {
        data.SavePlayerData();
    }

    public void LoadPlayer()
    {
        data.LoadPlayerData();



    }

    void FixedUpdate()
    {


        horizontal = Input.GetAxisRaw("Horizontal") * runSpeed;
        float vertical = Input.GetAxis("Vertical");

        animator.SetFloat("speed", Mathf.Abs(horizontal));
        Vector2 movement = new Vector2(horizontal, vertical);

        rigid.AddForce(movement * speed);
        rigid.velocity = new Vector2(horizontal * speed * Time.deltaTime, rigid.velocity.y);

        
        //check for death at a fixed frame rate
        if (currentHealth <= 0)
        {
            IsDead = true;
            SavePlayer();

        }


    }
    void PauseChoir() { HW_pickup.Pause(); }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Door1" && RoomKeyOne)
        {
            collision.gameObject.SetActive(false);
        }
        if (collision.gameObject.tag == "Door2" && RoomKeyTwo)
        {
            collision.gameObject.SetActive(false);
        }
        if (collision.gameObject.tag == "Door3" && RoomKeyThree)
        {
            collision.gameObject.SetActive(false);
        }

    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "firstSave")
        {
            SavePlayer();
        }

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

        if(collision.gameObject.tag == "EasterEggCave")
        {
            SceneManager.LoadScene("EALoading");
        }
        //Add health if you collide with beans
        if (collision.gameObject.name == "healingBeans")
        {
            Debug.Log("Restored 15 HP");
            healthBar.SetHealth(currentHealth += beanHealth);
            collision.gameObject.SetActive(false);

            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
        }

        if(collision.gameObject.name == "HW")
        {
            Debug.Log("Picked up Holy Water");
            collision.gameObject.SetActive(false);
            HW_pickup.Play();
            Invoke(nameof(PauseChoir), 3f);
            
            
        }

        if (collision.gameObject.name == "mapSprite")
        {
            Debug.Log("Picked up a minimap");
            collision.gameObject.SetActive(false);
            minimap.enabled = true;
            miniMap.enabled = true;

        }

        if(collision.gameObject.tag == "RoomKey1")
        {
            Debug.Log("Picked up first room key");
            collision.gameObject.SetActive(false);
            RoomKeyOne = true;
            SavePlayer();

        }

        if (collision.gameObject.tag == "RoomKey2")
        {
            Debug.Log("Picked up second room key");
            collision.gameObject.SetActive(false);
            RoomKeyTwo = true;
            SavePlayer();


        }

        if (collision.gameObject.tag == "RoomKey3")
        {
            Debug.Log("Picked up third room key");
            collision.gameObject.SetActive(false);
            RoomKeyThree = true;
            SavePlayer();

        }
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

    

    void Update()
    {


        UpdateFuel();
        ScoreText.text = "Score: " + score;
        animator.SetFloat("currentHealth", currentHealth);
       
        
        //Check for health threshold to enable visual signal
        if(currentHealth > (maxHealth / 3))
        {
        lowHealthBorder.enabled = false;

        }
    
        //Check transform read-only values if Character is facing left
        if (transform.lossyScale.x == -1)
        {
            isFacingLeft = true;
        }
        else
        {
            isFacingLeft = false;
        }

        //Check if player is Grounded by collision, player is grounded as long as he collides with platforms layer mask
        bool IsGrounded()
        {
            RaycastHit2D raycastHit2d = Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0f, Vector2.down, .1f, platformsLayerMask);
            // Debug.Log(raycastHit2d.collider);
            animator.SetBool("IsGrounded", true);
            return raycastHit2d.collider != null;
            
        }

        //Jump function

        if(IsGrounded() && !Input.GetKey(KeyCode.Space))
        {
            DoubleJump = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(IsGrounded() || DoubleJump)
            {
                rigid.velocity = new Vector2(rigid.velocity.x, jumpVelocity);

                DoubleJump = !DoubleJump;
            }    
          
        }

        if(Input.GetKeyDown(KeyCode.Space) && rigid.velocity.y > 0f)
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

       
        //Damage debug 
        if(Input.GetMouseButtonDown(2))
        {
            TakeDamage(10);
        }



     
        //Play walk audio when walking
        if(IsFalling)
        {
            WalkAudio.Pause();
        }
        if (Input.GetKeyDown(KeyCode.D) && IsGrounded())

        {
            WalkAudio.UnPause();

        }
        else if (Input.GetKeyDown(KeyCode.A) && IsGrounded())
        {
            WalkAudio.UnPause();

        }

        else if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
        {
            WalkAudio.Pause();


        }
        
    }

    

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
        
        if(IsDead)
        {
            animator.SetTrigger("IsDead");
            rigid.bodyType = RigidbodyType2D.Static;
            Invoke(nameof(InvokeScene), 1.50f);

        }
        if (currentHealth < (maxHealth / 3) && !IsDead)
        {
            lowHealthBorder.enabled = true; 
        }
        else 
            lowHealthBorder.enabled = false;
    }

    public void TakeGoblinDamage(float damage)
    {
        currentFuel -= damage;

        fuelBar.SetFuel(currentFuel);

    }
    void InvokeScene()
    {
        SceneManager.LoadScene("MainMenu");
        Destroy(gameObject);
    }


   
    
}