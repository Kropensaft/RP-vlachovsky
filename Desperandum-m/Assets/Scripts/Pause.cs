using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Pause : MonoBehaviour
{
    

    public bool IsPaused;
    


    void Start()
    {
        IsPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(IsPaused)
        {
            SceneManager.LoadScene("PauseMenu");
            
        }
        

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            IsPaused = !IsPaused;
        }
    }

    
}
