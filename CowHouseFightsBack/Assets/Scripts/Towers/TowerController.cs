using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    [SerializeField]
    private GameObject tower;
    [SerializeField] private GameObject placementHB;

    /// <summary>
    /// Whenever a tower's placement availability changes. Boolean for can/can't be placed
    /// </summary>
    public event Action<bool> OnPlacementAvailable;
    // Start is called before the first frame update
    void Start()
    {
        placementHB.GetComponent<PlacementHitboxController>()
            .OnPlacementAvailable += NotifyPlacementAvailability;
    }

    private void NotifyPlacementAvailability(bool available)
    {
        OnPlacementAvailable?.Invoke(available);
    }
    
    /// <summary>
    /// Whether to display placement hitbox
    /// </summary>
    /// <param name="visible">True if it should be visible</param>
    public void ShowPlacementHitbox(bool visible)
    {
        placementHB.SetActive(visible);
    }
}