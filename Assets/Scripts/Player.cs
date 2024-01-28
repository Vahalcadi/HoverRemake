using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement region")]
    [SerializeField] Rigidbody rb;
    [SerializeField] float speed;
    [SerializeField] float speedMultiplier;
    [SerializeField] float slowMultiplier;
    [SerializeField] float maxForce;
    [SerializeField] float jumpForce;
    public Vector2 accelDecel;
    public Vector2 rotation;

    [Header("Collision info")]
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance = 1;
    [SerializeField] protected LayerMask whatIsGround;

    [Header("Climb Stairs info")]
    [SerializeField] private GameObject stepRaycastUp;
    [SerializeField] private GameObject stepRaycastLow;
    [SerializeField] private float stepHeight;
    [SerializeField] private float stepSnap;

    [Header("Shield reference region")]
    public bool isShielded;

    [Header("Speed up reference region")]
    public bool isSpedUp;

    [Header("Invisibility reference region")]
    public int invisibilityUses;
    public bool isInvisible;
    public float invisibilityDuration;

    [Header("Jump reference region")]
    public int jumpUses;

    [Header("Slow down reference region")]
    public bool isSlowedDown;

    [Header("Wall reference region")]
    public int wallUses;
    public float wallDuration;
    public GameObject wallPrefab;

    [Header("Score reference region")]
    public int score;

    [Header("Flag reference region")]
    public int pickedUpFlags;

    private InputManager inputManager;
    private Transform cameraTransform;

    public static Player Instance { get; private set; }


    private void Awake()
    {
        if (Instance != null)
            Destroy(Instance.gameObject);
        else
            Instance = this;

        stepRaycastUp.transform.position = new Vector3(stepRaycastUp.transform.position.x, stepHeight, stepRaycastUp.transform.position.z);
    }

    private void Start()
    {
        inputManager = InputManager.Instance;
        cameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        Jump();
        PlaceWall();
        Invisibility();
    }

    private void FixedUpdate()
    {
        Move();
        Rotate();
        ClimbStep();

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
        if (isSpedUp)
            targetVelocity *= speed * speedMultiplier;
        else if (isSlowedDown)
            targetVelocity *= speed * slowMultiplier;
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
            Debug.Log("JUMPED");
            rb.AddForce(rb.velocity + Vector3.up * jumpForce, ForceMode.VelocityChange);
            jumpUses--;

        }
    }

    void ClimbStep()
    {
        RaycastHit hitLow;

        if (Physics.Raycast(stepRaycastLow.transform.position, transform.TransformDirection(Vector3.forward), out hitLow, 0.1f))
        {
            RaycastHit hitUp;
            if (!Physics.Raycast(stepRaycastUp.transform.position, transform.TransformDirection(Vector3.forward), out hitUp, 0.2f))
            {
                rb.position -= new Vector3(0, -stepSnap, 0);
            }
        }
    }

    void PlaceWall()
    {
        if (inputManager.GetPlaceWall() && wallUses > 0)
        {
            StartCoroutine(TimeWall());
        }
    }

    IEnumerator TimeWall()
    {

        /**
        * 
        * Places a wall behind the player
        * 
        * Quaternion wallRotation is used to adjust wall rotation when the game object is instantiated
        * Vector3 spawnPosition saves the position to instantiate the wall
        * 
        * **/

        Debug.Log("WALL PLACED");
        Quaternion wallRotation = cameraTransform.rotation;
        wallRotation *= Quaternion.AngleAxis(90f, Vector3.up);
        Vector3 spawnPosition = new Vector3(cameraTransform.position.x, cameraTransform.position.y, cameraTransform.position.z - 1);
        GameObject wall = Instantiate(wallPrefab, spawnPosition, wallRotation);
        wallUses--;

        yield return new WaitForSeconds(wallDuration);

        Destroy(wall);

    }

    void Invisibility()
    {
        if (inputManager.GetInvisibility() && invisibilityUses > 0 && isInvisible == false)
        {
            StartCoroutine(TimeInvisible());
        }
    }

    IEnumerator TimeInvisible()
    {
        /**
         * 
         * Sets isInvisible class variable to true, then reduces the number of uses
         * The excecution of the coroutine is then suspended for some seconds before completing
         * 
         * While the coroutine is suspended, the EnemyBumper can't locate the player
         * 
         * **/

        Debug.Log("INVISIBILITY USED");
        isInvisible = true;
        invisibilityUses--;
        yield return new WaitForSeconds(invisibilityDuration);
        Debug.Log("INVISIBILITY ENDED");
        isInvisible = false;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyFlag"))
        {
            other.gameObject.SetActive(false);
            pickedUpFlags++;
            Debug.Log($"Flag picked up by player\nScore {score}");
        }
    }
}