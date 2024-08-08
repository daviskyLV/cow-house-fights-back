using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float cameraSpeed;
    
    private PlayerControls controls;

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
    
    public void LateUpdate()
    {
        var scrollDirection = controls.Playfield.Movement.ReadValue<Vector2>().x * cameraSpeed;
        transform.position += new Vector3(0, 0, scrollDirection * Time.deltaTime);
    }
}
