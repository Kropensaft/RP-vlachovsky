using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;


public class KeySpamDetector : MonoBehaviour
{
    public Slider fillBar;
    public Image KeyIcon;
    public Image vignette;
    public float barFill = 0f;

    private float fillSpeed = 0.5f;
    private float decreaseSpeed = 0.2f;
    private float threshold = 0.5f;
    private float eventDuration = 5f;
    private float currentTime = 0f;
    private float keyHoldDuration;
    public bool QTEcompleted = false;
    public Arabis arabis;

    
    
    private void Start()
    {
        Arabis arabis = GetComponent<Arabis>();
        fillBar.value = 0f;
        fillBar.interactable = false;
        
    }
    private void Update()
    {
       
        currentTime += Time.deltaTime;

        

        if (currentTime >= eventDuration)
        {
            if (barFill > threshold)
            {
                arabis.TakeDamage1();
                Debug.Log("QTE Completed");
                QTEcompleted= true;
                
            }

            else
            {
                currentTime = 0f;
                arabis.elapsedTime = 0f;
            }
            // deactivate QTE UI
            this.gameObject.SetActive(false);
        }
        else
        {
            if (Input.GetKey(KeyCode.F))
            {


                keyHoldDuration += Time.deltaTime;

                if (keyHoldDuration < 0.1f)
                    barFill += fillSpeed * Time.deltaTime;
                
      
               
            }
            else
            {
                    keyHoldDuration= 0f;
                    barFill -= decreaseSpeed * Time.deltaTime;
                if(barFill < 0)
                {
                    barFill = 0;
                }
            }

            fillBar.value = barFill;
            fillBar.value = Mathf.Clamp(fillBar.value + fillSpeed * Time.deltaTime, 0, 1);
            vignette.color = new Color(0, 0, 0, 1 - barFill);
        }
    }

   
}
