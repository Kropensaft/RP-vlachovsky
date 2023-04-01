using UnityEngine;

public class WeepingAngelAI : MonoBehaviour
{
    public float HP;
    public float MaxHP = 30;
    public bool isDead = false;
    public bool canBeAttacked = false;

    //Get the coordinations of the cones outer border

    [SerializeField] public GameObject Player;

    private WeepingAngelMovement angelMovement;

    //Flashlight
    public Flashlight flashlight;

    public Character player;
    public Animator animator;
    private Rigidbody2D rigid;
    public float posY;
    public float posX;
    public float offsetR;
    public float WeepingAngelDamage = 5f;
    public float speed = 1.5f;
    protected float CharX;

    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        player = GameObject.FindObjectOfType(typeof(Character)) as Character;
        angelMovement = GetComponent<WeepingAngelMovement>();
        HP = MaxHP;
    }

    private void takeDamage()
    {
        HP -= .08f;

        if (HP <= 0)
        {
            Death();
        }
    }

    private void takeFireballDamage(float damage)
    {
        HP -= damage;

        if (HP <= 0)
        {
            Death();
        }
    }

    // Update is called once per frame
    private void Update()
    {
        posY = transform.position.y;
        posX = transform.position.x - 4.99773f;
        offsetR = posX - 1.8f;
        CharX = Player.transform.position.x;

        flashlight = Player.GetComponent<Flashlight>();
        //Healthbar.SetHealth(HP, MaxHP);

        checkForRadius();

        if (isDead == true)
        {
            Debug.Log("Killed an Enemy");
        }
    }

    private void checkForRadius()
    {
        if (posX >= offsetR && flashlight.flashlightLoaded && !isDead)
        {
            if (canBeAttacked)
            {
                takeDamage();
            }

            Debug.Log("Touched inner radius from right");
        }
        else if (posX <= CharX && flashlight.flashlightLoaded && !isDead && !angelMovement.IsStatic && canBeAttacked)
        {
            if (canBeAttacked)
            {
                takeDamage();
            }

            Debug.Log("Touched inner radius from left");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "projectile")
        {
            takeFireballDamage(10);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Sprite")
        {
            if (!isDead)
            {
                player.TakeDamage(Time.deltaTime * WeepingAngelDamage);
            }
            Debug.Log("Attacking Player");
            animator.SetBool("IsAttacking", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        animator.SetBool("IsAttacking", false);
    }

    private void Death()
    {
        rigid.bodyType = RigidbodyType2D.Static;
        isDead = true;
        animator.SetTrigger("IsDead");
        Destroy(gameObject, 2f);
        player.score += 30;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Sprite")
        {
            canBeAttacked = true;
        }
    }
}