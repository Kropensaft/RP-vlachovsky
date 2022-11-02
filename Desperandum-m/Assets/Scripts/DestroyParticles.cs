using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticles : MonoBehaviour
{
    private void Update()
    {
        Destroy(gameObject, .7f);
    }
}
