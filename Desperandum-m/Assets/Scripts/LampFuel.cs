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
    public float MaxFuel = 300f;
   

    void Start()
    {
        Flashlight flashlight = Player.GetComponent<Flashlight>();
        

        fuel = MaxFuel;
        FuelBar.maxValue = MaxFuel;
        FuelBar.value = fuel;

    }

    public void AddFuel()
    {
        fuel += 75f;
    }
   
    void Update()
    {

        


        if (flashlight.flashlightLoaded)
        {
            
             
             FuelBar.value = fuel;
             fuel -= 0.1f;
            
        }

        else if(!flashlight.flashlightLoaded)
        {
            FuelBar.value = fuel;
        }

  }

   
}
