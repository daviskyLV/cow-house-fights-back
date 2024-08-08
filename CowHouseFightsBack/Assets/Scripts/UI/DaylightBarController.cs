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

    private int hrs12 = 60 * 12;
    private int hrs24 = 60 * 24;

    /// <summary>
    /// Set the current time and day. Automatically triggers left/right icon animations on day/night change.
    /// </summary>
    /// <param name="minutes">Clock time in minutes since midnight, eg. 125 = 02:05</param>
    /// <param name="day">Current day</param>
    public void SetTime(int minutes, int day)
    {
        var normalMins = minutes % hrs24;
        timeText.text = $"Day {day}, {normalMins/60%24:00}:{normalMins%60:00}";
        slider.value = normalMins % hrs12 / (float)hrs12;
        
        // Set fill/text colors
        var dayProgress = normalMins / (float)hrs24;
        fill.color = fillGradient.Evaluate(dayProgress);
        border.color = outlineGradient.Evaluate(dayProgress);
        timeText.color = fillGradient.Evaluate(dayProgress);
        timeText.outlineColor = outlineGradient.Evaluate(dayProgress);
    }

    private void Update()
    {
        SetTime((int)(Time.time*100), 1);
    }
}
