using UnityEngine;
using UnityEngine.SceneManagement;

public class Bar : MonoBehaviour
{
    public GameObject bar;
    public int time;

    // Start is called before the first frame update
    private void Start()
    {
        AnimateBar();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void AnimateBar()
    {
        LeanTween.scaleX(bar, 1, time).setOnComplete(LoadMenu);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("LoreSlideshow");
    }
}