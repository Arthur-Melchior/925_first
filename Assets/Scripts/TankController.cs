using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class TankController : MonoBehaviour
{
    private static readonly int StartRolling = Animator.StringToHash("startRolling");
    private float _thrust = 0;
    private float _bodyRotation = 0;
    private float _turretRotation = 0;
    private float _cannonRotation = 0;
    private float _cannonTilt = 0;

    [Header("Speeds")] [SerializeField] private float speed = 1;
    [SerializeField] private float turnSpeed = 60;
    [SerializeField] private float turretSpeed = 40;
    [SerializeField] private float cannonSpeed = 200;

    [Header("Bullet")] [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletSpawnPoint;

    [SerializeField] private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(_thrust * speed * Time.deltaTime * Vector3.forward);
        transform.Rotate(_bodyRotation * turnSpeed * Time.deltaTime * Vector3.up);
        transform.Rotate(_turretRotation * turretSpeed * Time.deltaTime * Vector3.up);
        transform.Rotate(_cannonRotation * cannonSpeed * Time.deltaTime * Vector3.right);
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
        Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
    }

    public void OnRoll(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;
        
        Debug.Log("roll");
        animator.SetTrigger(StartRolling);
    }

    private IEnumerable test()
    {
        yield return new WaitForSeconds(1);
    }
}