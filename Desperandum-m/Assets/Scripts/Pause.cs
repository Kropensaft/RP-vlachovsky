using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public bool IsPaused;

    private void Start()
    {
        IsPaused = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (IsPaused)
        {
            SceneManager.LoadScene("PauseMenu");
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            IsPaused = !IsPaused;
        }
    }
}