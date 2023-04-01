using UnityEngine;
using UnityEngine.UI;

public class LampFuel : MonoBehaviour
{
    [SerializeField] private GameObject Player;

    public Flashlight flashlight;
    [HideInInspector] public Character player;
    public Slider FuelBar;

    private void Start()
    {
        flashlight = Player.GetComponent<Flashlight>();
        Character player = GetComponent<Character>();
    }

    public void SetMaxFuel(float fuel)
    {
        FuelBar.maxValue = fuel;
        FuelBar.value = fuel;
    }

    public void SetFuel(float fuel)
    {
        FuelBar.value = fuel;
    }
}