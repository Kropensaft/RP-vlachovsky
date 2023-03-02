using System.Collections;
using UnityEngine;

public class HomingBlade : MonoBehaviour
{
    public float homingDelay = 2.0f;
    public float homingSpeed = 5.0f;

    private bool homingEnabled = false;
    private Transform playerTransform;

    private Rigidbody2D rigid;
    

    void Start()
    {
        playerTransform = GetPlayerTransform();
        StartCoroutine(EnableHoming());
        rigid= GetComponent<Rigidbody2D>();
       
    }

    Transform GetPlayerTransform()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            return player.transform;
        }
        else
        {
            Debug.LogError("Player not found");
            return null;
        }
    }

    IEnumerator EnableHoming()
    {
        yield return new WaitForSeconds(homingDelay);
        homingEnabled = true;
    }

    void Update()
    {
        if (homingEnabled)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, homingSpeed * Time.deltaTime);
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Boundary"))
        {
            // Get the normal vector of the collision
            Vector2 normal = collision.contacts[0].normal;

            // Calculate the reflection vector
            Vector2 reflection = Vector2.Reflect(rigid.velocity, normal);

            // Apply a force to the circle in the direction of the reflection vector
            rigid.velocity = reflection;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("blade"))
        {
            // Calculate the direction vector between the two colliding circles
            Vector2 direction = (transform.position - collision.transform.position).normalized;

            // Apply a force to both circles in the direction away from each other
            GetComponent<Rigidbody2D>().AddForce(direction * 2f, ForceMode2D.Impulse);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(-direction * 2f, ForceMode2D.Impulse);
        }
      
    }
}
