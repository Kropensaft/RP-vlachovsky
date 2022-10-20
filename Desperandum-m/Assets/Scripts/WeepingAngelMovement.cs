using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public class WeepingAngelMovement : MonoBehaviour
{
    [SerializeField] WeepingAngelAI Angel;
    [SerializeField] Character player;
    [SerializeField] GameObject flashlight;


    public GameObject Player;
    public float speed;
    public bool flip;
    public bool IsStatic;
    



    // Start is called before the first frame update
    void Start()
    {
        Angel = GetComponent<WeepingAngelAI>();
        player = GetComponent<Character>();
       
    }

    // Update is called once per frame
    void Update()
    {
        IsStatic = true;
        Vector3 scale = transform.localScale;

        if(Player.transform.position.x > transform.position.x && !Angel.isDead && Player.transform.lossyScale.x == 1 && !flashlight.activeInHierarchy)
        {

            Debug.Log("Targeting enemy from left side");
            scale.x = Mathf.Abs(scale.x) * -1 * (flip ? -1 : 1);
            transform.Translate(speed * Time.deltaTime, 0, 0);
            IsStatic = false;

        }
        else if (Player.transform.position.x < transform.position.x && !Angel.isDead && Player.transform.lossyScale.x == -1 && !flashlight.activeInHierarchy)
        {
            Debug.Log("Targeting enemy from right side");
            scale.x = Mathf.Abs(scale.x) * (flip ? -1 : 1);
            transform.Translate(speed * Time.deltaTime * -1, 0 ,0);
            IsStatic = false;


        }
        else
        {
            IsStatic = true;
        }
        
        transform.localScale = scale;
    }
}
