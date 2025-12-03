using System;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private Rigidbody _rb;

    [SerializeField] private float bulletForce = 50;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        if (_rb)
        {
            _rb.AddRelativeForce(Vector3.forward * bulletForce, ForceMode.Impulse);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        _rb.AddRelativeForce(Vector3.back * bulletForce * 1.2f, ForceMode.Impulse);
    }
}