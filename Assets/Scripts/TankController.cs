using UnityEngine;
using UnityEngine.InputSystem;

public class TankController : MonoBehaviour
{
    private float _thrust = 0;
    private float _bodyRotation = 0;
    private float _turretRotation = 0;
    private float _cannonRotation = 0;
    private float _wheelRotation = 0;
    private const float WheelRotationSpeed = 300;
    [SerializeField] private float speed = 1;
    [SerializeField] private float turnSpeed = 30;
    [SerializeField] private float turretSpeed = 40;
    [SerializeField] private float cannonSpeed = 50;
    [SerializeField] private bool isLeftWheel = false;

    [SerializeField] private GameObject bullet;

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

        //transform.Rotate((_wheelRotation + _thrust * 1.1f) * WheelRotationSpeed * Time.deltaTime * Vector3.right);
        transform.Rotate(_wheelRotation * WheelRotationSpeed * Time.deltaTime * Vector3.right);
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
        Debug.Log($"cannon {value}");
        Debug.Log($"rotation {gameObject.transform.rotation.x}");

        if ((gameObject.transform.rotation.x > 0.2 && value > 0) ||
            (gameObject.transform.rotation.x < -0.2 && value < 0))
        {
            _cannonRotation = 0;
        }

        else
        {
            _cannonRotation = value;
        }
    }

    public void TurnWheel(InputAction.CallbackContext ctx)
    {
        var value = ctx.ReadValue<float>();
        Debug.Log($"wheel {value}");

        _wheelRotation = value;
    }

    public void OnShoot(InputAction.CallbackContext ctx)
    {
        Instantiate(bullet);
        bullet.transform.Translate(Vector3.forward);
    }
}