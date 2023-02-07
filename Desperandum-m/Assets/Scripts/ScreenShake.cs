using System.Collections;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    private Vector3 originalCamPos;
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;

    void OnEnable()
    {
        originalCamPos = Camera.main.transform.position;
    }

    void Update()
    {
        if (shakeAmount > 0)
        {
            Camera.main.transform.position = originalCamPos + Random.insideUnitSphere * shakeAmount;

            shakeAmount -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shakeAmount = 0f;
            Camera.main.transform.position = originalCamPos;
        }
    }

    public void TriggerShake()
    {
        shakeAmount = 0.7f;
        decreaseFactor = 1.0f;
    }
}
