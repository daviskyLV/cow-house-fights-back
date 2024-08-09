using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DaylightBarController : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Image fill;
    [SerializeField] private Image border;

    [SerializeField] private TextMeshProUGUI timeText;

    [SerializeField] private GameObject leftIcon;

    [SerializeField] private GameObject rightIcon;

    [SerializeField] private Sprite sun;

    [SerializeField] private Sprite moon;
    [SerializeField] private Gradient fillGradient;
    [SerializeField] private Gradient outlineGradient;

    private const int Hrs12 = 60 * 12;
    private const int Hrs24 = 60 * 24;

    private void Start()
    {
        GameController.OnTimeChanged += SetTime;
    }

    /// <summary>
    /// Set the current time and day. Automatically triggers left/right icon animations on day/night change.
    /// </summary>
    /// <param name="minutes">Clock time in minutes since midnight, eg. 125 = 02:05</param>
    /// <param name="day">Current day</param>
    private void SetTime(int minutes, int day)
    {
        var normalMins = minutes % Hrs24;
        timeText.text = $"Day {day}, {normalMins/60%24:00}:{normalMins%60:00}";
        slider.value = normalMins % Hrs12 / (float)Hrs12;
        
        // Set fill/text colors
        var dayProgress = normalMins / (float)Hrs24;
        fill.color = fillGradient.Evaluate(dayProgress);
        border.color = outlineGradient.Evaluate(dayProgress);
        timeText.color = fillGradient.Evaluate(dayProgress);
        timeText.outlineColor = outlineGradient.Evaluate(dayProgress);
    }
}
