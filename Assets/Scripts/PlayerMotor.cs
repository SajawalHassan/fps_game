using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private float speed;

    [SerializeField] private float gravity = -9.8f;
    [SerializeField] private float walkSpeed = 7f;
    [SerializeField] private float airSpeed = 2f;
    [SerializeField] private float jumpHeight = 3f;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        speed = walkSpeed;
    }
    
    private void Update()
    {
        isGrounded = controller.isGrounded;
    }
    
    public void HandleMovement(Vector2 input)
    {
        // Calculate move direction
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;

        // To control acceleration depending whether player is on ground or not
        if (!isGrounded && !(moveDirection.z > 0) || !(moveDirection.z > 0) && moveDirection.x != 0) speed = airSpeed;
        else speed = walkSpeed;
        
        // Apply movement
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
    }
    
    public void HandleGravity()
    {
        velocity.y += gravity * Time.deltaTime;
        if (isGrounded && velocity.y < 0) velocity.y = -2f;

        controller.Move(velocity * Time.deltaTime);
    }

    public void Jump()
    {
        if (isGrounded) velocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
    }
}
