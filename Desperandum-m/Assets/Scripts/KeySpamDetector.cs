using UnityEngine;
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
    public float currentTime = 0f;
    private float keyHoldDuration;
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
            if (arabis.QTEcompleted == 0 && barFill >= threshold)
            {
                arabis.QTEactive = false;
                arabis.TakeDamage1();
                Debug.Log("QTE Completed");
            }

            if (arabis.QTEcompleted == 1 && barFill >= threshold)
            {
                arabis.QTEactive = false;
                arabis.TakeDamage2();
                Debug.Log("Second QTE completed");
            }
            if (arabis.QTEcompleted == 2 && barFill >= threshold)
            {
                arabis.QTEactive = false;
                arabis.TakeDamage3();
                Debug.Log("Final QTE completed");
            }
            // deactivate QTE UI
            if (arabis.QTEcompleted == 1 || arabis.QTEcompleted == 2 || arabis.QTEcompleted == 3 && !arabis.QTEactive)
            {
                Debug.Log("QTE disabled");
                vignette.color = new Color(0, 0, 0, 1);
                currentTime = 0f;
                this.gameObject.SetActive(false);
            }
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
                keyHoldDuration = 0f;
                barFill -= decreaseSpeed * Time.deltaTime;
                if (barFill < 0)
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