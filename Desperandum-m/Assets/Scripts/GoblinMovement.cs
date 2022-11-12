using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinMovement : MonoBehaviour
{
    //Move Variables
    public float speed;

    //Player variables
    public GameObject Player;
    public Character player;

    //Animation
    public Animator animator;

    //goblin vars
    [SerializeField] Transform goblinTransform;


    public BoxCollider2D coll;
    private Rigidbody2D rigid;
    public GoblinAI goblin;
    public bool flip;
    public float jumpTimer = 0;
    public bool IsChasing = false;
    public bool IsAlive;
     float GoblinDamage;


    private void Start()
    {
        animator = GetComponent<Animator>();
        Character player = GetComponent<Character>();
        rigid = transform.GetComponent<Rigidbody2D>();
        goblin = GetComponent<GoblinAI>();

        IsAlive = true;
        speed = 3f;
        GoblinDamage = 10f;
        jumpTimer = 3f;
    }



    private void Update()
    {
        jumpTimer -= Time.deltaTime;

        if(jumpTimer < 0) jumpTimer = 0;

        if(jumpTimer == 0)
        {
            Jump();
            jumpTimer += 4;

        }
    }

    void Jump()
    {
        rigid.AddForce(Vector2.up * 50f);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.gameObject == Player && IsAlive)
        {
            IsChasing = true;
        }



    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        IsChasing = false;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject == Player && IsAlive)
        {
            if (IsAlive)
            {
                player.TakeGoblinDamage(GoblinDamage * Time.deltaTime);

            }
            Debug.Log("Attacking Player");
            animator.SetBool("IsAttacking", true);

        }

    }
    

}