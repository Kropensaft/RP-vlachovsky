using UnityEngine;
using UnityEngine.UI;

public class ColorGradient: MonoBehaviour
{
    public Slider slider;
    public Gradient colorGradient;
    public Image fillImage;

    private void Start()
    {
        fillImage = slider.fillRect.GetComponent<Image>();
        fillImage.material = new Material(Shader.Find("UI/Unlit/Transparent"));
    }

    private void Update()
    {
        fillImage.color = colorGradient.Evaluate(slider.value);
    }
}