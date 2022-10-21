using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ButtonUI : MonoBehaviour
{
    

    int lastScene;

   

    private void Update()
    {
        lastScene = SceneManager.GetActiveScene().buildIndex - 1;
    }

    [SerializeField] private string loadingScene = "LoadingScene";

    public void NewGameButton()
    {
        SceneManager.LoadScene(loadingScene);
      
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game Exiting");
        
    }
    public void LoadOptions()
    {
        Debug.Log("Loading options menu");
        SceneManager.LoadScene("OptionsMenu");
    }
    public void ExitOptions()
    {
        Debug.Log("Exiting options menu");
        SceneManager.LoadScene(lastScene);
       
    }
   
}
