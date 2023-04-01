using UnityEngine;

public class Flip : MonoBehaviour
{
    private Rigidbody2D rigid;

    private float inputHorizontal;
    private float inputVertical;

    public bool facingRight = true;

    // Start is called before the first frame update
    private void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");

        if (inputHorizontal > 0 && !facingRight)
        {
            flip();
        }

        if (inputHorizontal < 0 && facingRight)
        {
            flip();
        }
    }

    private void flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        facingRight = !facingRight;
    }
}