using UnityEngine;

public class Player : EntityHover
{
    [Header("Movement region")]
    [SerializeField] Rigidbody rb;
    [SerializeField] float speed;
    [SerializeField] float speedMultiplier;
    [SerializeField] float maxForce;
    [SerializeField] float jumpForce;
    public Vector2 accelDecel;
    public Vector2 rotation;

    [Header("Collision info")]
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance = 1;
    [SerializeField] protected LayerMask whatIsGround;

    [Header("Pickup reference region")]
    public bool isShielded;
    public bool isSpedUp;
    public int invisibilityUses;
    public int jumpUses;
    public bool isSlowedDown;
    public int wallUses;

    private InputManager inputManager;
    private Transform cameraTransform;

    public static Player Instance { get; private set; }


    private void Awake()
    {
        if (Instance != null)
            Destroy(Instance.gameObject);
        else
            Instance = this;
    }

    private void Start()
    {
        inputManager = InputManager.Instance;
        cameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        Jump();
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

        if(isSpedUp)
            targetVelocity *= speed * speedMultiplier;
        else
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

    void Jump()
    {

        if (inputManager.GetJump() && IsGroundDetected() && jumpUses > 0)
        {
            
            rb.AddForce(rb.velocity + Vector3.up * jumpForce, ForceMode.VelocityChange);
            jumpUses--;
            
        }
        
        
    }

    void Rotate()
    {
        rotation = inputManager.GetRotation();
    }

    public bool IsGroundDetected() => Physics.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance, groundCheck.position.z));
    }
}