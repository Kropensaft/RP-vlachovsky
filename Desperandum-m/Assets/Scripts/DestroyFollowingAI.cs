using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyFollowingAI : MonoBehaviour
{

    public WeepingAngelAI Wangel;

    // Update is called once per frame
    void Update()
    {
        if(Wangel.isDead)
        {
            Invoke("Dstr", 0f);
        }
    }

    void Dstr()
    {
        Destroy(gameObject);
    }
}
