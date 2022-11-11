using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireballAttack : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 mousePos;
    public GameObject fireball;
    public GameObject flashlight;
    public GameObject player;
    public Character character;
    public Transform fireballTransform;
    public bool canFire;
    public float timer;
    public float timeBetweenFiring;
    public float fireballCost;


    private void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("Player").GetComponent<Camera>();
        Character character = GetComponent<Character>();
      
        
        canFire = true;
    }

    private void Update()
    {
       
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        Vector3 rotation = mousePos - transform.position;

        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        if(player.transform.lossyScale.x == -1)
        {
            rotZ += 180f;
        }  
        else
            rotZ -= 25f; // Fixuje Invertaci svìtla pøi flipu I divnou chybu pøi zmìnì z Freeform na Spotlight

        transform.rotation = Quaternion.Euler(0, 0, rotZ);

        if(!canFire)
        {
            timer += Time.deltaTime;
            if(timer > timeBetweenFiring)
            {
                canFire = true;
                timer = 0;
            }
        }

        if(Input.GetMouseButton(1) && canFire && !flashlight.activeInHierarchy && character.currentFuel >= 0.1f)
        {
            canFire = false;
            character.animator.SetBool("IsAttacking", true);
            character.TakeGoblinDamage(fireballCost);
            Instantiate(fireball, fireballTransform.position, Quaternion.identity);
            
            
        }
        if(Input.GetMouseButtonUp(1))
        {
            character.animator.SetBool("IsAttacking", false);

        }
    }
}


