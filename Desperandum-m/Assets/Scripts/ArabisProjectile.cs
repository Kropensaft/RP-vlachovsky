using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArabisProjectile : MonoBehaviour
{
    // The speed of the projectile
    public float speed = 1.0f;

    // The acceleration rate of the projectile
    public float acceleration = 0.1f;

    public Vector2 direction = Vector2.right;

    // Update is called once per frame
    void Update()
    {
        // Move the projectile in the specified direction
        transform.position += (Vector3)direction * speed * Time.deltaTime;

        // Increment the speed by the acceleration rate
        speed += acceleration * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Boundary") ||collision.CompareTag("Arabis"))
        {
            Destroy(gameObject);
        }
    }
    

}