using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LampFuel : MonoBehaviour
{

    [SerializeField] GameObject Player;
    public Flashlight flashlight;
    public Slider FuelBar;
    public float fuel;
    public float MaxFuel = 20f;

    void Start()
    {
        Flashlight flashlight = Player.GetComponent<Flashlight>();
        fuel = MaxFuel;
        FuelBar.maxValue = MaxFuel;
        FuelBar.value = fuel;
    }


    void Update()
    {
        if(flashlight.flashlightLoaded)
        {
            fuel -= 0.1f;
            FuelBar.value = fuel; 
        }
    }
}
