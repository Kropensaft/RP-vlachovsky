using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FlashlightMove : MonoBehaviour
{


    //rotation
    private Camera mainCam;
    private Vector3 mousePos;



    // Start is called before the first frame update
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("Player").GetComponent<Camera>();

    }

    // Update is called once per frame
    void Update()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mousePos - transform.position;

        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        rotZ = rotZ - 90f; // Fixuje Invertaci sv�tla p�i flipu I divnou chybu p�i zm�n� z Freeform na Spotlight
        transform.rotation = Quaternion.Euler(0, 0, rotZ);
    }
}
