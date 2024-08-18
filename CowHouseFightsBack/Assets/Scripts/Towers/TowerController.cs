using System;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    public static event Action OnTowerDestroyed;
    public static event Action OnTowerPlaced;
    
    [SerializeField] private Tower towerScript;
    [SerializeField] private Collider towerCollider;

    private void Start()
    {
        towerScript.OnTowerDestroyed += TowerDestroyed;
    }

    private void TowerDestroyed()
    {
        OnTowerDestroyed?.Invoke();
    }

    public void Setup(GameObject enemyFolder)
    {
        towerCollider.enabled = true;
        towerScript.Setup(enemyFolder);
        OnTowerPlaced?.Invoke();
    }
}
