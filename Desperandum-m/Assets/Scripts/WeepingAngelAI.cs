using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class WeepingAngelAI : MonoBehaviour
{

    

    private Rigidbody2D rigid;

    public float HP;
    public float MaxHP = 30;
    //public EnemyHealthBar Healthbar;
    public bool isDead = false;
    
    

    //Get the coordinations of the cones outer border 

    [SerializeField] GameObject Player;
    [SerializeField] GameObject Angel;


    //Flashlight
    public Flashlight flashlight;
    public Character player;
    public Animator animator;
    private EnemyMovement angel;
    public float posY;
    public float posX;
    public float offsetR;
    public float WeepingAngelDamage = 5f;
    public float speed = 1.5f;
    protected float CharX;
    
   
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigid = transform.GetComponent<Rigidbody2D>();
        player = GameObject.FindObjectOfType(typeof(Character)) as Character;
       // Healthbar.SetHealth(HP, MaxHP);
        angel = Angel.GetComponent<EnemyMovement>();
        HP = MaxHP;
        
       
    }

    void takeDamage()
    {
        HP -= .08f;
        //Healthbar.SetHealth(HP, MaxHP);

        if (HP <= 0)
        {
            
            Death();
        }

    }
    
    // Update is called once per frame
    void Update()
    {
        posY = transform.position.y;
        posX = transform.position.x - 4.99773f;
        offsetR = posX - 1.8f;
        CharX = Player.transform.position.x;

        Flashlight flashlight = Player.GetComponent<Flashlight>();
        //Healthbar.SetHealth(HP, MaxHP);
        
        
        checkForRadius();

        if(isDead == true)
        {
            Debug.Log("Killed an Enemy");
        }
      
    }

   

    void checkForRadius()
    {
         if (posX >= offsetR && angel.facingRight && flashlight.flashlightLoaded)
             {
          
            takeDamage();
            Debug.Log("Touched inner radius from right");
                            
             }
        else if(posX <= CharX && !angel.facingRight && flashlight.flashlightLoaded) 
        {
            Debug.Log("Touched inner radius from left");
            takeDamage();

        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Sprite")
        {
            animator.Play("Base Layer.Attack1", 0, 3f);
            player.TakeDamage(WeepingAngelDamage);
        }
    }


  void Death()
    {
        
        isDead = true;
        player.score += 10;
        Destroy(gameObject, 0f);
    }
}
