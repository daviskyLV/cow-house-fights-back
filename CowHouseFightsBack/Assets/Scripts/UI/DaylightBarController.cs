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
    [SerializeField]
    [Range(0, Hrs24-1)]
    private int dayStartMinute;
    [SerializeField]
    [Range(1, Hrs24)]
    private int dayEndMinute;

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

        var dayProgress = 0.0f;
        var normalizedProgress = normalMins / (float)Hrs24;
        if (minutes >= dayStartMinute && minutes <= dayEndMinute)
        {
            // Daytime
            dayProgress = (minutes - dayStartMinute)/(float)(dayEndMinute - dayStartMinute);
        }
        else
        {
            // Night time (first fill up day time end, then day time start)
            var barSize = (float)(dayStartMinute + Hrs24 - dayEndMinute);
            if (minutes > dayEndMinute)
                dayProgress = (minutes - dayEndMinute) / barSize;
            else
                dayProgress = (Hrs24 - dayEndMinute + minutes) / barSize;
        }
        
        slider.value = dayProgress;
        // Set fill/text colors
        fill.color = fillGradient.Evaluate(normalizedProgress);
        border.color = outlineGradient.Evaluate(normalizedProgress);
        timeText.color = fillGradient.Evaluate(normalizedProgress);
        timeText.outlineColor = outlineGradient.Evaluate(normalizedProgress);
    }
}
