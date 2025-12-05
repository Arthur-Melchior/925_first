using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private float rollForce = 1;
    private Rigidbody _rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnRollAnimationStart(int direction)
    {
        _rb.AddForce(transform.right * direction * rollForce, ForceMode.Impulse);
    }

    public void OnRollAnimationEnd(string s)
    {
        _rb.linearVelocity = Vector3.zero;
        if (transform.position.y < 0)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
    }

    public void SlowDown(string s)
    {
        _rb.linearVelocity /= 2;
    }
}