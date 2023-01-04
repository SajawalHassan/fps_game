using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerInput.OnFootActions onFoot;
    private PlayerMotor motor;
    private PlayerLook looker;

    private void Awake()
    {
        // Lock cursor inside of screen
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        // Set variables
        motor = GetComponent<PlayerMotor>();
        looker = GetComponent<PlayerLook>();

        // Initialize variables
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;

        // Jump
        onFoot.Jump.performed += ctx => motor.Jump();
    }

    private void FixedUpdate()
    {
        motor.HandleMovement(onFoot.Movement.ReadValue<Vector2>());
        motor.HandleGravity();
    }

    private void LateUpdate()
    {
        looker.HandleMouseMovement(onFoot.Look.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        onFoot.Enable();
    }

    private void OnDisable()
    {
        onFoot.Disable();
    }
}
