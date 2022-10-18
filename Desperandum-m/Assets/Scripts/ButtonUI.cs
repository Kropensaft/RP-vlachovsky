using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ButtonUI : MonoBehaviour
{

    

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

   
}
