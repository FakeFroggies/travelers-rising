using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider HealthBarSlider;
    public void UpdateHealthBar(int maxHealth, int currentHealth)
    {
        HealthBarSlider.maxValue = maxHealth;
        HealthBarSlider.minValue = 0;
        HealthBarSlider.value = currentHealth;
    }
}
