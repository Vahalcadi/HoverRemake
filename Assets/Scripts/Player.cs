using UnityEngine;

public class Player : MonoBehaviour
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
        //Get value of x and y from input using Input Action component
        accelDecel = inputManager.GetAccelDecel();

        //Assign default velocity to a vector3 variable
        Vector3 currentVelocity = rb.velocity;

        //Assign registered x and y input to a vector3 variable
        Vector3 targetVelocity = new Vector3(accelDecel.x, 0f, accelDecel.y);

        //Bounding forward movement to camera direction
        targetVelocity = cameraTransform.forward * targetVelocity.z;

        //checking is player has an active speed buff (GreenLight) and assigning velocity accordingly
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

        //Move player
        rb.AddForce(velocityChange);

    }

    void Jump()
    {
        /**
         * 
         * GetJump() function returns true if the button bound to jump is pressed
         * IsGroundDetected() fuction returns true when the raycast line, that starts from player, touches the ground
         * jumpUses is a class variable that increments when the player picks up a jump pickup
         * 
         * 
         * if all the contions are met, vertical movement is finalised and jumpUses is decreased
         * **/

        if (inputManager.GetJump() && IsGroundDetected() && jumpUses > 0)
        {
            
            rb.AddForce(rb.velocity + Vector3.up * jumpForce, ForceMode.VelocityChange);
            jumpUses--;
            
        }
        
        
    }


    /**
     * 
     * Sets a vector2 variable to the value of x and y registered from the Input Agent
     *
     * The vector2 variable "rotation" is used in the CinemachinePovExtention script to 
     * move the camera horizontaly
     * 
     * **/
    void Rotate()
    {
        rotation = inputManager.GetRotation();
    }


    /**
     * 
     * Raycast to check if the player is touching the ground
     * 
     * **/
    public bool IsGroundDetected() => Physics.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);


    /**
     * 
     * Visually represents the IsGroundDetected() raycast
     * 
     * **/
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance, groundCheck.position.z));
    }
}