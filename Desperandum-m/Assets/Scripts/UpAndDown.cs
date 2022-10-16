using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;
using UnityEngine;

public class UpAndDown : MonoBehaviour
{
    public float amp;
    public float freq;
    Vector3 initPos;
   

    
    

    private void Start()
    {
        initPos = transform.position;
        
        
    }


    private void Update()
    {
        
        transform.position = new Vector3(initPos.x, Mathf.Sin(Time.time *  freq) * amp + initPos.y, -4.78f);
    }
}
