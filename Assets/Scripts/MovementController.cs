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
            float previousVelocityY = _rb.velocity.y;
            Vector3 moveDirection = transform.TransformDirection(_joystickController.ReturnVectorDirection());
            Vector3 newVelocityXZ = moveDirection * speed;
            _rb.velocity = new Vector3(newVelocityXZ.x, previousVelocityY, newVelocityXZ.z);
        }
    }
}
