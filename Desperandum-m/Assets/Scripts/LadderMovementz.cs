using UnityEngine;

public class LadderMovementz : MonoBehaviour
{
    private float vertical;
    private float speed = 8f;
    private bool isLadder;
    public bool isClimbing;

    [SerializeField] private Rigidbody2D rigid;

    private void Update()
    {
        vertical = Input.GetAxis("Vertical");

        if (isLadder && Mathf.Abs(vertical) > 0f)
        {
            isClimbing = true;
        }
    }

    private void FixedUpdate()
    {
        if (isClimbing)
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
        if (collision.gameObject.tag == ("Ladder"))
        {
            Debug.Log("Climbing Ladder");
            isLadder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == ("Ladder"))
        {
            isLadder = false;
            isClimbing = false;
        }
    }
}