using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadArenaInBckg : MonoBehaviour
{
    public bool changeScene;
    public string sceneName;

    // Add this variable to keep track of whether the coroutine is already running
    private bool isLoading = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Sprite" && !isLoading) // Only start the coroutine if it's not already running
        {
            StartCoroutine(LoadYourAsyncScene());
        }
    }

    IEnumerator LoadYourAsyncScene()
    {
        // Set isLoading to true to prevent the coroutine from being called again
        isLoading = true;

        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false; // Set allowSceneActivation to false to prevent the scene from loading immediately

        // Wait until the asynchronous scene fully loads
        while (asyncLoad.progress < 0.9f) // Check if progress is less than 0.9f to ensure that the scene is fully loaded
        {
            yield return null;
        }

        if (changeScene) // Check if the player has met certain conditions
        {
            asyncLoad.allowSceneActivation = true; // Set allowSceneActivation to true to load the scene
        }

        // Set isLoading to false when the coroutine is finished
        isLoading = false;
    }
}
