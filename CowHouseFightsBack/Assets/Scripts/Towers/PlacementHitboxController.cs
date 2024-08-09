using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementHitboxController : MonoBehaviour
{
    [SerializeField] private Material noCollisionColor;

    [SerializeField] private Material collisionColor;

    private List<Transform> hitboxes = new();
    [SerializeField]
    private bool placedDown = false;
    /// <summary>
    /// Whenever a tower's placement availability changes. Boolean for can/can't be placed
    /// </summary>
    public event Action<bool> OnPlacementAvailable;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            hitboxes.Add(transform.GetChild(i));
        }
    }

    private void ChangeColliderMaterial(Material mat)
    {
        foreach (var t in hitboxes)
        {
            var mr = t.GetComponent<MeshRenderer>();
            mr.material = mat;
        }
    }
    
    /// <summary>
    /// Enable or disable hitboxes
    /// </summary>
    /// <param name="visible">True if visible</param>
    public void ShowHitbox(bool visible = true)
    {
        foreach (var t in hitboxes)
        {
            t.GetComponent<MeshRenderer>().enabled = visible;
        }
    }

    /// <summary>
    /// Changes whether hitbox is placed down or not
    /// </summary>
    /// <param name="placedDown">True for yes</param>
    public void ChangePlacementStatus(bool placedDown)
    {
        this.placedDown = placedDown;
    }

    private void OnTriggerEnter(Collider other)
    {
        OnPlacementAvailable?.Invoke(false);
        ChangeColliderMaterial(collisionColor);
        ShowHitbox();
    }

    private void OnTriggerExit(Collider other)
    {
        OnPlacementAvailable?.Invoke(true);
        ChangeColliderMaterial(noCollisionColor);
        if (placedDown)
            ShowHitbox(false);
    }
}
