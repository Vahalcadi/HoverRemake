using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float speed;
    [SerializeField] float maxForce;
    [SerializeField] float rotationSpeed;
    public Vector2 accelDecel;
    public Vector2 rotation;

    private InputManager inputManager;
    private Transform cameraTransform;

    private void Start()
    {
        inputManager = InputManager.Instance;
        cameraTransform = Camera.main.transform;
    }

    private void FixedUpdate()
    {
        Move();
        Rotate();
    }

    void Move()
    {
        //Find target velocity
        accelDecel = inputManager.GetAccelDecel();
        Vector3 currentVelocity = rb.velocity;
        Vector3 targetVelocity = new Vector3(accelDecel.x, 0f, accelDecel.y);
        targetVelocity = cameraTransform.forward * targetVelocity.z;
        targetVelocity *= speed;
        //Align direction
        targetVelocity = transform.TransformDirection(targetVelocity);
        //Calculate forces
        Vector3 velocityChange = (targetVelocity - currentVelocity);
        //Limit force
        Vector3.ClampMagnitude(velocityChange, maxForce);
        rb.AddForce(velocityChange);

        //controller.Move(velocityChange);
    }

    void Rotate()
    {
        rotation = inputManager.GetRotation();
    }
}