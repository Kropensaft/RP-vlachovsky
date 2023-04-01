using UnityEngine;
using UnityEngine.SceneManagement;

public class EAcameraload : MonoBehaviour
{
    public Transform cTransform;
    private float foo;
    public new Camera camera;

    public GameObject bar;
    public int time;

    private void Start()
    {
        foo = cTransform.position.x;
        AnimateBar();
    }

    // Update is called once per frame
    private void Update()
    {
        foo += (2.7f * Time.deltaTime);

        camera.transform.position = new Vector3(foo, cTransform.position.y, cTransform.position.z);
    }

    public void AnimateBar()
    {
        LeanTween.scaleX(bar, 1, time).setOnComplete(LoadScene);
    }

    private void LoadScene()
    {
        SceneManager.LoadScene("EasterEgg");
    }
}