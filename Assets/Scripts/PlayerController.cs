using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class Example : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float speed;
    [SerializeField] float maxForce;
    [SerializeField] float rotationSpeed;
    Vector2 accelDecel;
    Vector2 rotation;

    public void GetAccelDecel(InputAction.CallbackContext context)
    {
        accelDecel = context.ReadValue<Vector2>();
    }

    public void GetRotation(InputAction.CallbackContext context)
    {
        rotation = context.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Move();
        Rotate();
    }

    void Move()
    {
        //Find target velocity
        Vector3 currentVelocity = rb.velocity;
        Vector3 targetVelocity = new Vector3(accelDecel.x, 0f, accelDecel.y);
        targetVelocity *= speed;
        //Align direction
        targetVelocity = transform.TransformDirection(targetVelocity);
        //Calculate forces
        Vector3 velocityChange = (targetVelocity - currentVelocity);
        //Limit force
        Vector3.ClampMagnitude(velocityChange, maxForce);
        rb.AddForce(velocityChange);
    }

    void Rotate()
    {
        rotation *= rotationSpeed * Time.deltaTime;
        transform.Rotate(0f, rotation.x, 0f);
    }
}