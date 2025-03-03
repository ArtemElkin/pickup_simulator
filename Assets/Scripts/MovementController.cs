using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float speed = 1f;
    [SerializeField] private JoystickController _joystickController;
    private Rigidbody _rb;
    

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (_joystickController.isActive)
        {
            _rb.velocity = _joystickController.ReturnVectorDirection() * speed;
        }
    }
}
