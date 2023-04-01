using System.Collections;
using UnityEngine;

public class HomingBlade : MonoBehaviour
{
    public float homingDelay = 2.0f;
    public float homingSpeed = 4.0f;

    public bool homingEnabled = false;
    private Transform playerTransform;

    public float velocityIncrement = 0.5f;

    private int numCollisions = 0;
    public bool isRunning = false;
    private Rigidbody2D rigid;
    public LayerMask collisionMask;

    private void Start()
    {
        playerTransform = GetPlayerTransform();
        rigid = GetComponent<Rigidbody2D>();
        KeySpamDetector KSD = GetComponent<KeySpamDetector>();

        StartHomingCoroutine();
    }

    private Transform GetPlayerTransform()
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

    private IEnumerator EnableHoming()
    {
        yield return new WaitForSeconds(homingDelay);
        homingEnabled = true;
        print("Started Homing");
        isRunning = true;
    }

    private void Update()
    {
        if (homingEnabled)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, homingSpeed * Time.deltaTime);
        }
    }

    public void StartHomingCoroutine()
    {
        if (!isRunning)
            StartCoroutine(EnableHoming());
        else
        {
            StopCoroutine(EnableHoming());
            StartCoroutine(EnableHoming());
        }
    }

    private void FixedUpdate()
    {
        // Get the velocity of the circle
        Vector2 velocity = rigid.velocity;

        // Cast a ray in the direction of the velocity
        RaycastHit2D hit = Physics2D.Raycast(transform.position, velocity, velocity.magnitude * Time.fixedDeltaTime, collisionMask);

        // If the ray hits a wall, reflect the velocity and add velocity increment
        if (hit.collider != null)
        {
            // Get the normal of the collision
            Vector2 normal = hit.normal;

            // Calculate the angle of reflection
            Vector2 reflection = Vector2.Reflect(velocity, normal);

            // Apply the angle of reflection to the Rigidbody 2D
            rigid.velocity = reflection.normalized * (velocity.magnitude + velocityIncrement);

            // Increment the number of collisions
            numCollisions++;
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