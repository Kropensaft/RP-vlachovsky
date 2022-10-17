using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonUI : MonoBehaviour
{

    [SerializeField] private string firstLevel = "FirstLevel";


    public void NewGameButton()
    {
        SceneManager.LoadScene(firstLevel);
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game Exiting");
    }


}
