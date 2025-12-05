using System;
using Unity.VisualScripting;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    [SerializeField] private float explosionForce = 100;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        var rb = other.GetComponent<Rigidbody>();

        if (rb)
        {
            rb.AddExplosionForce(explosionForce, other.transform.up, 10);
        }
    }
}