using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Pause : MonoBehaviour
{
    

    public bool IsPaused;
   


    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if(IsPaused)
        {

            Menu();
        }
        else
        {
           
            Time.timeScale = 1f;    
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            IsPaused = true;
        }
    }

    public void Resume()

    {
        SceneManager.LoadScene("FirstLevel");
        IsPaused = false; 
    }

    public void Menu()
    {
        
            Time.timeScale = 0f;
            SceneManager.LoadScene("PauseMenu");
        

        
    }
}
