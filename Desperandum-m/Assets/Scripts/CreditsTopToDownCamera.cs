using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DentedPixel;
using UnityEngine.SceneManagement;

public class CreditsTopToDownCamera : MonoBehaviour
{

    public Transform cTransform;
    private float foo;
    public new Camera camera;

    private Vector3 startPos;
    public Vector3 endPos;
    public int time;


    private void Start()
    {
        foo = cTransform.position.y;
        startPos = cTransform.position;

    }


    // Update is called once per frame
    void Update()
    {

        foo -= (time * Time.deltaTime);

        camera.transform.position = new Vector3(cTransform.position.x, foo, cTransform.position.z);



        if (cTransform.position.y <= endPos.y)
        {
            camera.transform.position = startPos;
        }



    }




}
