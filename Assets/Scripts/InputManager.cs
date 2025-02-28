using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;
    private PlayerControls playerControls;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this.gameObject);
        else
            Instance = this;

        playerControls = new PlayerControls();
    }


    public Vector2 GetAccelDecel()
    {
        return playerControls.Player.AccelerationDeceleration.ReadValue<Vector2>();
    }

    public Vector2 GetRotation()
    {
        return playerControls.Player.Rotation.ReadValue<Vector2>();
    }

    public bool GetJump()
    {
        return playerControls.Player.Jump.triggered;
    }

    public bool GetPlaceWall()
    {
        return playerControls.Player.PlaceWall.triggered;
    }

    public bool GetInvisibility()
    {
        return playerControls.Player.Invisibility.triggered;
    }

    public void OnEnable()
    {
        playerControls.Enable();
    }

    public void OnDisable()
    {
        playerControls.Disable();
    }
}
