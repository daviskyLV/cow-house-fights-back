using System;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    public static event Action OnTowerDestroyed;
    public static event Action OnTowerPlaced;
    
    [SerializeField] private Tower towerScript;
    [SerializeField] private Collider towerCollider;
    [SerializeField] private Placable placable;

    private void Start()
    {
        placable.OnPlacedDown += AwakenTower;
        towerScript.OnTowerDestroyed += TowerDestroyed;
    }

    private void TowerDestroyed()
    {
        OnTowerDestroyed?.Invoke();
    }
    
    private void AwakenTower()
    {
        towerCollider.enabled = true;
        OnTowerPlaced?.Invoke();
    }
}
