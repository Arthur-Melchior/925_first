using System;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private Transform trackingTarget;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float smoothSpeed = 5f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
}
