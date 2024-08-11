using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private Slider hpSlider;
    [SerializeField] private Gradient fillHpColor;
    [SerializeField] private Image hpFill;
    
    private Camera playersCamera;
    private float health;
    
    // Start is called before the first frame update
    void Start()
    {
        health = Random.Range(0.0f, maxHealth);
        hpSlider.value = health/maxHealth;
        Debug.Log($"Max health: {maxHealth}, Health: {health}, Normalized: {health/maxHealth}");
        hpFill.color = fillHpColor.Evaluate(health/maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
