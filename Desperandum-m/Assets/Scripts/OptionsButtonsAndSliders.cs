using UnityEngine;
using UnityEngine.UI;

public class OptionsButtonsAndSliders : MonoBehaviour
{
    public Slider volumeSlider; // Reference to the Slider that will be used to control the volume

    void Start()
    {
        // Set the initial value of the Slider to the current volume level
        volumeSlider.value = AudioListener.volume;
    }

    public void SetVolume()
    {
        // Set the volume based on the current value of the Slider
        AudioListener.volume = volumeSlider.value;
    }
}