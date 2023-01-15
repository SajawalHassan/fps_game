using UnityEngine;
using TMPro;

// Does not inherit from "BaseWeapon" as this class will be used to manage the weapons, not be an actual weapon
public class WeaponSwitcher : MonoBehaviour
{
    public int selectedWeaponIndex = 0;
    public TextMeshProUGUI bulletsLeftLabel;

    private PlayerInput playerInput;
    private PlayerInput.WeaponActions weaponActions;
    
    // Start is called before the first frame update
    private void Start()
    {
        SelectWeapon();
    }

    private void Awake()
    {
        playerInput = new PlayerInput();        
        weaponActions = playerInput.Weapon;

        // Subscribing to each hotkey for our slots
        weaponActions.Slot1.performed += ctx => {selectedWeaponIndex = 0; SelectWeapon();};
        weaponActions.Slot2.performed += ctx => {selectedWeaponIndex = 1; SelectWeapon();};
        weaponActions.Slot3.performed += ctx => {selectedWeaponIndex = 2; SelectWeapon();};
    }

    // Update is called once per frame
    private void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        int prevSelectedWeaponIndex = selectedWeaponIndex;

        // If we are scrolling
        if (scroll != 0)
        {
            selectedWeaponIndex++; // Incrementing to next weapon
            selectedWeaponIndex %= transform.childCount; // Wrapping the index
        }

        if (prevSelectedWeaponIndex != selectedWeaponIndex) SelectWeapon();
    }

    private void SelectWeapon()
    {
        if (selectedWeaponIndex > (transform.childCount - 1)) return;
        int i = 0;
        
        foreach (Transform weapon in transform)
        {
            if (i == selectedWeaponIndex) weapon.gameObject.SetActive(true);
            else weapon.gameObject.SetActive(false);
            i++;
        }
    }
    
    private void OnEnable()
    {
        weaponActions.Enable();
    }
    private void OnDisable()
    {
        weaponActions.Disable();
    }

}
