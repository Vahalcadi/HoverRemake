using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement region")]
    [SerializeField] Rigidbody rb;
    [SerializeField] float speed;
    [SerializeField] float speedMultiplier;
    [SerializeField] float slowMultiplier;
    public float maxVelocity;
    [SerializeField] float jumpForce;
    public Vector2 accelDecel;
    public Vector2 rotation;

    [Header("Collision info")]
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance = 1;
    [SerializeField] protected LayerMask whatIsGround;

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
    [SerializeField] private Transform wallSpawnpoint;
    public int wallUses;
    public bool wallPlaced;
    public float wallDuration;
    public GameObject wallPrefab;

    [Header("Score reference region")]
    public int score;

    [Header("Flag reference region")]
    public int pickedUpFlags;

    [Header("Camera reference region")]
    [SerializeField] private float horizontalCameraSpeed = 10;

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
        PlaceWall();
        Invisibility();
        Rotate();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        //Get value of x and y from input using Input Action component
        accelDecel = inputManager.GetAccelDecel();

       
        if (accelDecel.y != 0)
        {
            float finalSpeed = accelDecel.y * speed;

           if (isSpedUp)
                finalSpeed *= speedMultiplier;
            else if(isSlowedDown)
                finalSpeed *= slowMultiplier;
            
            //Move player
            rb.AddForce(transform.forward * finalSpeed * Time.fixedDeltaTime);
        }

        rb.velocity = new Vector3(ClampVelocityAxis(true), rb.velocity.y, ClampVelocityAxis(false));

        
    }

    private float ClampVelocityAxis(bool isX)
    {
        float currentMaxVelocity = maxVelocity;
        float speed;

        if (isSpedUp)
            maxVelocity *= speedMultiplier;
        else if (isSlowedDown)
            maxVelocity *= slowMultiplier;

        if (isX)
            speed = Mathf.Clamp(rb.velocity.x,-maxVelocity,maxVelocity);
        else
            speed = Mathf.Clamp(rb.velocity.z, -maxVelocity, maxVelocity);

        maxVelocity = currentMaxVelocity;
        return speed;

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

    /*void ClimbStep()
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
    }*/

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

        wallPlaced = true;
        Debug.Log("WALL PLACED");
        
        GameObject wall = Instantiate(wallPrefab, wallSpawnpoint.position, wallSpawnpoint.rotation);
        wallUses--;

        yield return new WaitForSeconds(wallDuration);

        Destroy(wall);
        wallPlaced = false;
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
        transform.Rotate(new Vector3( 0, rotation.x * horizontalCameraSpeed * Time.deltaTime, 0));
        // Assign x value to the vector3 variable "initialRotation"


        /*   // Assign new value of RawOrientation to camera to define an horizontal visual movement
           state.RawOrientation = Quaternion.Euler(0, initialRotation.x, 0);*/

        
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