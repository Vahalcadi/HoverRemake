using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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



    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }
}
