using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonUI : MonoBehaviour
{
    

    int lastScene;
   
    

    private void Start()
    {
       
        
    }
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
    public void LoadPlayer()
    {
        //SceneManager.LoadScene("FirstLevel");
         SaveSystem saveSystem = new SaveSystem();
         saveSystem.LoadPlayerData();
      

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

   public void ContinueButton()
    {
        
        SceneManager.LoadScene("FirstLevel");
        Debug.Log("Loading game from files");
        SaveSystem data = GetComponent<SaveSystem>();
        data.LoadPlayerData();
        data.LoadLevelScene();

    }
   

    public void ExitPause()
    {
        
        SceneManager.LoadScene("MainMenu");
        Debug.Log("Exiting pause menu ");
    }
}
