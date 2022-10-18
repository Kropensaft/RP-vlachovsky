using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Rendering.Universal;

public class Flashlight : MonoBehaviour
{

    [SerializeField] GameObject FuelBar;
    [SerializeField] GameObject flashlight;
    public bool flashlightLoaded = true;
   
    

    public LampFuel Fuel;
    public Light2D CandleLight;
    public Light2D Lantern;

   
    public void Start()
    {
        FlashlightOff();
        flashlightLoaded = true;
        Fuel = FuelBar.GetComponent<LampFuel>();
    }


   
    public void FlashlightOn()
    {
       
        flashlight.SetActive(true);
        CandleLight.intensity = .7f;
        flashlightLoaded = true;
        
    }

   public void FlashlightOff()
    {
        flashlight.SetActive(false);
        CandleLight.intensity = 1.25f;
        flashlightLoaded = false;   
    }

  

    public void Update()
    {


        if (Input.GetMouseButton(0) && Fuel.fuel > 0.1f)
        {
            flashlightLoaded = false;

            if (!flashlightLoaded)
            {
                FlashlightOn();
                
            }


        }

        else
        {
            FlashlightOff();
        }

        if (Fuel.fuel <= .1f)
            Debug.Log("Fuel is at 0");
           
        
            
    }




}
