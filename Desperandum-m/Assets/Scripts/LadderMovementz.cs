using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderMovementz : MonoBehaviour
{
    private float vertical;
    private float speed = 8f;
    private bool isLadder;
    private bool isClimbing;

    [SerializeField] private Rigidbody2D rigid;

    
    void Update()
    {
        vertical = Input.GetAxis("Vertical");

        if(isLadder && Mathf.Abs(vertical) > 0f)
        {
            isClimbing = true;
        }
    }
     void FixedUpdate()
    {
        if(isClimbing)
        {
            rigid.gravityScale = 0f;
            rigid.velocity = new Vector2(rigid.velocity.x, vertical * speed);
        }
        else
        {
            rigid.gravityScale = 1f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Ladder"))
        {
            isLadder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Ladder"))
        {
            isLadder = false;
            isClimbing = false;
        }
    }
}
