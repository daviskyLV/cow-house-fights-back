using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class DayCycleFX : MonoBehaviour
{
    [SerializeField] private Vector3 middayRotation;
    [SerializeField] private Light sunlight;
    [SerializeField] private AnimationCurve intensityGradient;
    [SerializeField]
    [Tooltip("Sun angle (value is 0-360 for degrees)")]
    private AnimationCurve sunAngleGradient;
    
    private const int Hrs12 = 60 * 12;
    private const int Hrs24 = 60 * 24;
    
    // Start is called before the first frame update
    void Start()
    {
        GameController.OnTimeChanged += TimeChange;
    }

    private void TimeChange(int currentMinute, int currentDay)
    {
        currentMinute %= Hrs24;
        var curMinNorm = currentMinute / (float)Hrs24;
        sunlight.intensity = intensityGradient.Evaluate(curMinNorm);
        //sunlight.intensity = intensityGradientOLD.Evaluate(curMinNorm).a;
        
        // Rotating twice cuz couldnt figure out quaternions
        // First turning it into midnight rotation
        var angle = sunAngleGradient.Evaluate(curMinNorm);
        transform.rotation = Quaternion.Euler(
            new Vector3(-1*middayRotation.x,middayRotation.y,middayRotation.z)
            );
        transform.Rotate(Vector3.right * angle, Space.World);
    }
}
