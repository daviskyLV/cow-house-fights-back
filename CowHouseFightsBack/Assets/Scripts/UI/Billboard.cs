using UnityEngine;

public class Billboard : MonoBehaviour
{
    [SerializeField] private Transform playersCamera;

    // Update is called once per frame
    void LateUpdate()
    {
        if (!playersCamera)
            return;
        
        transform.LookAt(transform.position + playersCamera.forward);
    }
    
    public void ChangePlayersCamera(Transform cam)
    {
        playersCamera = cam;
    }
    
    public void ChangePlayersCamera(Camera cam)
    {
        playersCamera = cam.transform;
    }
}
