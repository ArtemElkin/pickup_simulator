using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float speed = 1f;
    [SerializeField] private JoystickController _joystickController;
    [SerializeField] private CameraController _cameraController;
    private Rigidbody _rb;
    

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (_joystickController.isActive)
        {
            Vector3 moveDirection = transform.TransformDirection(_joystickController.ReturnVectorDirection());
            _rb.velocity = moveDirection * speed;
        }
    }
}
