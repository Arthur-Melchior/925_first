using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private Transform trackingTarget;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float smoothSpeed = 5f;

    [SerializeField] private Camera cam1;
    [SerializeField] private Camera cam2;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam1.enabled = true;
        cam2.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void LateUpdate()
    {
        var desiredPosition = trackingTarget.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
    }

    public void ChangeCamera(InputAction.CallbackContext ctx)
    {
        Debug.Log("oskour");
        cam1.enabled = !cam1.enabled;
        cam2.enabled = !cam2.enabled;
    }
}
