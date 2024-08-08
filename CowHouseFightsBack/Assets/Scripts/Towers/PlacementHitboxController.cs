using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementHitboxController : MonoBehaviour
{
    [SerializeField] private Material noCollisionColor;

    [SerializeField] private Material collisionColor;

    private List<Transform> hitboxes;
    /// <summary>
    /// Whenever a tower's placement availability changes. Boolean for can/can't be placed
    /// </summary>
    public event Action<bool> OnPlacementAvailable;
    // Start is called before the first frame update
    void Start()
    {
        hitboxes = new List<Transform>();
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

    private void OnCollisionEnter(Collision other)
    {
        OnPlacementAvailable?.Invoke(false);
        ChangeColliderMaterial(collisionColor);
    }

    private void OnCollisionExit(Collision other)
    {
        OnPlacementAvailable?.Invoke(true);
        ChangeColliderMaterial(noCollisionColor);
    }
}
