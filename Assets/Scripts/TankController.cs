using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class TankController : MonoBehaviour
{
    private static readonly int StartRollingRight = Animator.StringToHash("startRollingRight");
    private static readonly int StartRollingLeft = Animator.StringToHash("startRollingLeft");
    private float _thrust = 0;
    private float _bodyRotation = 0;
    private float _turretRotation = 0;
    private float _cannonRotation = 0;
    private float _cannonTilt = 0;

    [Header("Speeds")] [SerializeField] private float speed = 1;
    [SerializeField] private float turnSpeed = 60;
    [SerializeField] private float turretSpeed = 40;
    [SerializeField] private float cannonSpeed = 200;
    [SerializeField] private bool allowThrustInput = true;

    [Header("Bullet")] [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletSpawnPoint;

    [SerializeField] private Animator animator;
    private Rigidbody _rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (allowThrustInput)
        {
            var velocity = _thrust * speed * transform.forward;
            _rb.AddForce(new Vector3(velocity.x, 0, velocity.z));
            _rb.linearVelocity = new Vector3(velocity.x, _rb.linearVelocity.y, velocity.z);
        }

        _rb.angularVelocity = _bodyRotation * Mathf.Deg2Rad * turnSpeed * transform.up;
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        Debug.Log($"MOVE {ctx.ReadValue<float>()}");
        _thrust = ctx.ReadValue<float>();
    }

    public void OnTurn(InputAction.CallbackContext ctx)
    {
        Debug.Log($"TURN {ctx.ReadValue<float>()}");
        _bodyRotation = ctx.ReadValue<float>();
    }

    public void OnTurnTurret(InputAction.CallbackContext ctx)
    {
        Debug.Log($"turret {ctx.ReadValue<float>()}");
        _turretRotation = ctx.ReadValue<float>();
    }

    public void OnTurnCannon(InputAction.CallbackContext ctx)
    {
        var value = ctx.ReadValue<float>();
        _cannonTilt += _cannonRotation * cannonSpeed * Time.deltaTime;

        if (_cannonTilt > 65 && value > 0 || _cannonTilt < -90 && value < 0)
        {
            _cannonRotation = 0;
        }
        else
        {
            _cannonRotation = value;
        }
    }

    public void OnShoot(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;

        var fx = Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        Destroy(fx, 5f);
    }

    public void OnRollRight(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;

        Debug.Log("roll");
        animator.SetTrigger(StartRollingRight);
    }

    public void OnRollLeft(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;

        Debug.Log("roll");
        animator.SetTrigger(StartRollingLeft);
    }
}