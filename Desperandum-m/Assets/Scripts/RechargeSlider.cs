using UnityEngine;
using UnityEngine.UI;

public class RechargeSlider : MonoBehaviour
{
    public Slider slider;
    public FireballAttack fireballattack;

    // Start is called before the first frame update
    private void Start()
    {
        FireballAttack fireballattack = GetComponent<FireballAttack>();
    }

    // Update is called once per frame
    private void Update()
    {
        slider.maxValue = fireballattack.timeBetweenFiring;

        if (fireballattack.canFire)
        {
            slider.value = slider.maxValue;
        }
        else if (!fireballattack.canFire)
        {
            slider.value -= Time.deltaTime * (fireballattack.timeBetweenFiring / 3);
        }
    }
}