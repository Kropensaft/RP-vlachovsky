using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LampFuel : MonoBehaviour
{
    [SerializeField] GameObject Player;
    
    public Flashlight flashlight;
    [HideInInspector] public Character player;
    public Slider FuelBar;
   

    void Start()
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
