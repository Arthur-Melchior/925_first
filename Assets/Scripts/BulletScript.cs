using System;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private Rigidbody _rb;

    [SerializeField] private float bulletForce = 50;
    [SerializeField] private GameObject explosion;
    
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
        var fx = Instantiate(explosion, transform.position, transform.rotation);
        Destroy(fx, 1f);
    }
}