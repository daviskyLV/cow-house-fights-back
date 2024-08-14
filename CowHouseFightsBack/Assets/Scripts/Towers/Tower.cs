using System;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public event Action OnTowerDestroyed;
    
    [SerializeField] private Rigidbody rb;

    private void OnCollisionEnter(Collision other)
    {
        OnTowerDestroyed?.Invoke();
    }
}
