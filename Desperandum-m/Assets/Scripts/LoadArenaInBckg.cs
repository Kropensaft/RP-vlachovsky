using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadArenaInBckg : MonoBehaviour
{
    public bool changeScene;
    public string sceneName;

    private bool isLoading = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Sprite" && !isLoading)
        {
            StartCoroutine(LoadYourAsyncScene());
        }
    }

    private IEnumerator LoadYourAsyncScene()
    {
        isLoading = true;

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;

        while (asyncLoad.progress < 0.9f) // Wait until the scene is fully loaded
        {
            yield return null;
        }

        while (!changeScene) // Wait until the player reaches the final collider
        {
            yield return null;
        }

        asyncLoad.allowSceneActivation = true; // Allow the scene to activate

        isLoading = false;
    }
}