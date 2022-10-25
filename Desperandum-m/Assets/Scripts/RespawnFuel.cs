using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class RespawnFuel : MonoBehaviour
{

    [SerializeField] GameObject FuelCan;
    Collider2D coll;

    public float respawnTime = 5f;
    private void Start()
    {
        coll = GetComponent<Collider2D>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Sprite")
        {
            Debug.Log("Invoking");
            Invoke(nameof(SpawnCanister), respawnTime);
        }

    }


    void SpawnCanister()
    {
        Debug.Log("Spawned Canister");
        Vector2 pos = new Vector2(transform.localPosition.x, transform.localPosition.y);
        Instantiate(FuelCan, pos, Quaternion.identity);
        coll.isTrigger = true;
        
    }
}
