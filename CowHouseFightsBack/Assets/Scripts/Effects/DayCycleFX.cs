using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCycleFX : MonoBehaviour
{
    [SerializeField] private Vector3 middayRotation;
    [SerializeField] private Light sunlight;
    [SerializeField]
    [Tooltip("Intensity gradient based on alpha value")]
    private Gradient intensityGradient;
    
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
        sunlight.intensity = intensityGradient.Evaluate(curMinNorm).a;
        
        // Rotating twice cuz couldnt figure out quaternions
        // First turning it into midnight rotation
        transform.rotation = Quaternion.Euler(
            new Vector3(-1*middayRotation.x,middayRotation.y,middayRotation.z)
            );
        transform.Rotate(Vector3.right * (360 * curMinNorm), Space.World);
    }
}
