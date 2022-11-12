using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
  
    public Rigidbody2D rigid;
    public Transform enemyTransform;
    public Transform groundCheckPos;
    public Collider2D coll;
    public GoblinMovement goblinmovement;
    [HideInInspector] public bool isPathing;
    [SerializeField] private LayerMask platformsLayerMask; // slouží pro grounded check

    public float speed = 10f;
    bool mustTurn;

   
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        goblinmovement = GetComponent<GoblinMovement>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
       if(isPathing)
        {
            mustTurn = !Physics2D.OverlapCircle(groundCheckPos.position, 0.1f, platformsLayerMask);
        }
    }
    void Update()
    {
       

        if (goblinmovement.IsChasing)
        {
            isPathing = false;
        }
        else
            Pathing();
    }

   

    void Pathing()
    {
        if(mustTurn || coll.IsTouchingLayers(platformsLayerMask))
        {
            Flip();
        }
        rigid.velocity = new Vector2(speed * Time.fixedDeltaTime, rigid.velocity.y);

    }

    void Flip()
    {
        isPathing = false;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        speed *= -1;
        isPathing = true;

    }
   
}
