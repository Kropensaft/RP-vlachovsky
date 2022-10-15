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

    private Animator animator;
    private EnemyMovement angel;
    public float posY;
    public float posX;
    public float offsetR;
    
    public float speed = 1.5f;
    protected float CharX;

   
    // Start is called before the first frame update
    void Start()
    {
        
        rigid = transform.GetComponent<Rigidbody2D>();
       // Healthbar.SetHealth(HP, MaxHP);
        angel = Angel.GetComponent<EnemyMovement>();
        HP = MaxHP;
        
       
    }

    void TakeDamage()
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
          
            TakeDamage();
            Debug.Log("Touched inner radius from right");
                            
             }
        else if(posX <= CharX && !angel.facingRight && flashlight.flashlightLoaded) 
        {
            Debug.Log("Touched inner radius from left");
            TakeDamage();

        }
    }

  void Death()
    {

        isDead = true;
        //Player.score += 10;
        Destroy(gameObject, 0f);
    }
}
