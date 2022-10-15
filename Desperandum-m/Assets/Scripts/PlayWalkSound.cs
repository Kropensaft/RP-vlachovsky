using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayWalkSound : MonoBehaviour
{

    private AudioSource audioSource;
    private bool IsMoving;

    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetAxis("Vertical") < 0)
        {
             IsMoving = true;
        }
           
        else 
        IsMoving = false;



        if (IsMoving && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
            
           
        if (!IsMoving)
        {
            audioSource.Stop();
        }


        }

}
