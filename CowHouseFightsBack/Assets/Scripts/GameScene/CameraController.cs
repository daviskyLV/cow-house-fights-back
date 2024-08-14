using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float cameraSpeed;
    [SerializeField]
    [Tooltip("Uses grass's Y scale and Z position to calculate the limits of camera Z movement")]
    private Transform grass;
    
    private PlayerControls controls;
    private float camMinZ;
    private float camMaxZ;

    private void Awake()
    {
        controls = new PlayerControls();
    }

    private void Start()
    {
        camMinZ = grass.position.z - grass.localScale.y / 2;
        camMaxZ = grass.position.z + grass.localScale.y / 2;
    }

    private void OnEnable()
    {
        var move = controls.Playfield.Movement;
        move.Enable();
    }

    private void OnDisable()
    {
        var move = controls.Playfield.Movement;
        move.Disable();
    }
    
    public void LateUpdate()
    {
        var scrollDirection = controls.Playfield.Movement.ReadValue<Vector2>().x * cameraSpeed;
        var newZ = Math.Clamp(transform.position.z + scrollDirection * Time.deltaTime, camMinZ, camMaxZ);
        transform.position = new Vector3(transform.position.x, transform.position.y, newZ);
    }
}
