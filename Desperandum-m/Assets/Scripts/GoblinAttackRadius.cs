using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinAttackRadius : MonoBehaviour
{
    public GoblinMovement gMove;

    // Start is called before the first frame update
    void Start()
    {
       GoblinMovement gMove = GetComponent<GoblinMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Sprite")
        {
            gMove.animator.SetBool("IsAttacking", true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
            gMove.animator.SetBool("IsAttacking", false);

    }
}
