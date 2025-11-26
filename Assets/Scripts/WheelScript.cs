using UnityEngine;
using UnityEngine.InputSystem;

public class WheelScript : MonoBehaviour
{
    private float _turnDirection;
    private float _thrustDirection;
    [SerializeField] private float wheelRotation = 300;
    [SerializeField] private bool isLeftWheel;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        var rotationValue = _thrustDirection != 0
            ? _turnDirection / 2f + _thrustDirection
            : _turnDirection;

        transform.Rotate(rotationValue * wheelRotation * Time.deltaTime * Vector3.right);
    }

    public void GoForward(InputAction.CallbackContext ctx)
    {
        Debug.Log($"wheel thrust : {ctx.ReadValue<float>()}");
        _thrustDirection = isLeftWheel ? ctx.ReadValue<float>() : -ctx.ReadValue<float>();
    }

    public void Turn(InputAction.CallbackContext ctx)
    {
        Debug.Log($"wheel rotation : {ctx.ReadValue<float>()}");
        _turnDirection = ctx.ReadValue<float>();
    }
}