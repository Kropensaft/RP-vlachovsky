﻿using UnityEngine;
using UnityEngine.UI;

public class DashCooldownSlider : MonoBehaviour
{
    public Slider slider;
    public BossFightCharacter player;
    private float MaxValue;

    private void Start()
    {
        BossFightCharacter player = GetComponent<BossFightCharacter>();
        MaxValue = slider.maxValue;
    }

    private void Update()
    {
        if (player.isDashing)
        {
            slider.value = 0;
            slider.value += Time.deltaTime * 4;
            if (slider.value > MaxValue) { slider.value = MaxValue; }
        }
        else
        {
            slider.value = MaxValue;
        }
    }
}