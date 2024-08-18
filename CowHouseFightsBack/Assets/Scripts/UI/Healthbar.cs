using System;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Slider hpSlider;
    [SerializeField] private Gradient fillHpColor;
    [SerializeField] private Image hpFill;

    /// <summary>
    /// How much health does the entity have (0-1)
    /// </summary>
    /// <param name="health">Health amount between 0 and 1</param>
    public void UpdateHealthValue(float health)
    {
        health = Math.Clamp(health, 0.0f, 1.0f);
        hpSlider.value = health;
        hpFill.color = fillHpColor.Evaluate(health);
    }
}
