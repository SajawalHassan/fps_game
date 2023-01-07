using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    private float xRotation;
    
    [SerializeField] private float mouseSensitivity = 5f;
    [SerializeField] private Camera cam;

    public void HandleMouseMovement(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;

        // X rotation (Vertical)
        xRotation -= (mouseY * Time.deltaTime) * mouseSensitivity;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0); // Applies x rotation

        // Y rotation (Horizontal)
        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * mouseSensitivity);
    }
}
