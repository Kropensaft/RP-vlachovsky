using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class RespawnHealth : MonoBehaviour
{

    [SerializeField] GameObject healingBeans;
    [SerializeField] Collider2D coll;

    public float HealhtRespawnTime = 5f;
    private void Start()
    {
        coll = GetComponent<Collider2D>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Sprite")
        {
            Debug.Log("Invoking");
            Invoke(nameof(SpawnBeans), HealhtRespawnTime);

        }

    }


    void SpawnBeans()
    {
        Debug.Log("Spawned Beans");
        Vector2 pos = new Vector2(transform.localPosition.x, transform.localPosition.y);
        Instantiate(healingBeans, pos, Quaternion.identity);
        healingBeans.SetActive(true); // Zaruèí aby se klon spawnul aktivovaný 
        coll.isTrigger = true;

    }
}