using UnityEngine;

// Does not inherit from "BaseWeapon" as this class will be used to handle the weapons, not be an actual weapon
public class WeaponSway : MonoBehaviour
{
    [SerializeField] private float smooth;
    [SerializeField] private float swayMultiplier;

    private void Update()
    {
        // Get mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * swayMultiplier;
        float mouseY = Input.GetAxisRaw("Mouse Y") * swayMultiplier;

        // Calculate target rotation
        Quaternion targetRotationX = Quaternion.AngleAxis(-mouseY, Vector3.right);
        Quaternion targetRotationY = Quaternion.AngleAxis(mouseX, Vector3.up);

        Quaternion targetRotation = targetRotationX * targetRotationY;

        // Apply rotation
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smooth * Time.deltaTime);
    }
}
