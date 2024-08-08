using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShopController : MonoBehaviour
{
    [SerializeField] private GameObject towersGO;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject chickenTowerPrefab;
    
    private PlayerControls controls;
    private Placable currentPlacement;

    private void Awake()
    {
        controls = new PlayerControls();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        CreateNewTower();
    }

    // Update is called once per frame
    void Update()
    {
        PositionPlacement();
    }

    private void CreateNewTower()
    {
        var tower = Instantiate(chickenTowerPrefab);
        currentPlacement = tower.GetComponent<TowerController>();
        currentPlacement.ShowPlacementHitbox(true);
    }

    private void PositionPlacement()
    {
        if (!currentPlacement)
            return;
        
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        int layerMask = 1 << 7; // Tower placement collider layer
        
        if (Physics.Raycast(ray, out hit, 1000, layerMask))
        {
            currentPlacement.transform.position = hit.point;
            return;
        }
    }
}
