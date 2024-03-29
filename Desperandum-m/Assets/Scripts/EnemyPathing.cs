using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    public Rigidbody2D rigid;
    public Transform enemyTransform;
    public Transform groundCheckPos;
    public Collider2D coll;
    public GoblinMovement goblinmovement;
    [HideInInspector] public bool isPathing;
    [SerializeField] private LayerMask platformsLayerMask; // slou�� pro grounded check

    public float speed = 10f;
    private bool mustTurn;

    // Start is called before the first frame update
    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        goblinmovement = GetComponent<GoblinMovement>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (isPathing)
        {
            mustTurn = !Physics2D.OverlapCircle(groundCheckPos.position, 0.1f, platformsLayerMask);
        }
    }

    private void Update()
    {
        if (goblinmovement.IsChasing)
        {
            isPathing = false;
        }
        else
            Pathing();
    }

    private void Pathing()
    {
        if (mustTurn || coll.IsTouchingLayers(platformsLayerMask))
        {
            Flip();
        }
        rigid.velocity = new Vector2(speed * Time.fixedDeltaTime, rigid.velocity.y);
    }

    private void Flip()
    {
        isPathing = false;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        speed *= -1;
        isPathing = true;
    }
}