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
    public float damage;
  

    // Start is called before the first frame update
    void Start()
    {
        
        rigid = GetComponent<Rigidbody2D>();
        
        mainCam = GameObject.FindGameObjectWithTag("Player").GetComponent<Camera>();
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;

        rigid.velocity = new Vector2(direction.x, direction.y).normalized * force;

        damage = .08f;
    }

    private void Update()
    {
    
    }




    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "WeepingAngel" || collision.gameObject.layer == 7)


        {

            Instantiate(impactEffect, transform.position, transform.rotation);

            Destroy(gameObject);

        }
    }
}