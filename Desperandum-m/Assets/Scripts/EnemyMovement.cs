using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class EnemyMovement : MonoBehaviour
{

  

    public bool facingRight;
    public AIPath aiPath;


    private void Start()
    {
       

    }
    // Update is called once per frame
    private void Update()
    {
        if(aiPath.desiredVelocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(-1, 1f, 1f);
            facingRight = false;
        }
        else if(aiPath.desiredVelocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(1, 1f, 1f);
            facingRight = true;


        }
    }
}
