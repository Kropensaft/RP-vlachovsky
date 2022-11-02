using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{


    private Vector3 mousePos;
    private Camera mainCam;
    public GameObject impactEffect;
    public Rigidbody2D rigid;
    public float force;
  

    // Start is called before the first frame update
    void Start()
    {
        
        rigid = GetComponent<Rigidbody2D>();
        
        mainCam = GameObject.FindGameObjectWithTag("Player").GetComponent<Camera>();
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;

        rigid.velocity = new Vector2(direction.x, direction.y).normalized * force;

    }

    private void Update()
    {
    
    }




    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "GenerateShadowCasters")
        {

            Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(gameObject);

        }
    }
}