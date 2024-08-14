using System;
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
    private bool placementOffField = true;
    private bool canBePlaced = false;

    public static event Action<GameObject> OnTowerPlaced;
    
    private void Awake()
    {
        controls = new PlayerControls();
    }

    private void OnEnable()
    {
        var select = controls.UI.Select;
        select.Enable();
        select.performed += PlaceTower;
    }

    private void OnDisable()
    {
        var select = controls.UI.Select;
        select.Disable();
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
        currentPlacement = tower.GetComponent<Placable>();
        canBePlaced = true;
        currentPlacement.OnPlacementAvailable += PlacementAvailabilityChanged;
        currentPlacement.ShowPlacementHitbox(true);
    }

    private void PlaceTower(InputAction.CallbackContext context)
    {
        // doesnt work
        if (!canBePlaced || placementOffField)
            return;
        
        currentPlacement.transform.parent = towersGO.transform;
        currentPlacement.PlaceDown();
        currentPlacement.ShowPlacementHitbox(false);
        OnTowerPlaced?.Invoke(currentPlacement.gameObject);
        currentPlacement = null;
        CreateNewTower();
    }

    private void PlacementAvailabilityChanged(bool available)
    {
        canBePlaced = available;
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
            placementOffField = false;
            return;
        }

        placementOffField = true;
    }
}
