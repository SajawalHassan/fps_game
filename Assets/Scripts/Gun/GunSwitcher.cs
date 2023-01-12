using UnityEngine;
using TMPro;

public class GunSwitcher : MonoBehaviour
{
    public int selectedGunIndex = 0;
    public TextMeshProUGUI bulletsLeftLabel;

    private PlayerInput playerInput;
    private PlayerInput.WeaponActions weaponActions;
    
    // Start is called before the first frame update
    void Start()
    {
        SelectGun();
    }

    private void Awake()
    {
        playerInput = new PlayerInput();        
        weaponActions = playerInput.Weapon;

        // Slot switching
        weaponActions.Slot1.performed += ctx => {selectedGunIndex = 0; SelectGun();};
        weaponActions.Slot2.performed += ctx => {selectedGunIndex = 1; SelectGun();};
        weaponActions.Slot3.performed += ctx => {selectedGunIndex = 2; SelectGun();};
    }

    // Update is called once per frame
    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        int prevSelectedGunIndex = selectedGunIndex;

        // If we are scrolling
        if (scroll != 0)
        {
            selectedGunIndex++; // Incrementing to next weapon
            selectedGunIndex %= transform.childCount; // Wrapping the index
        }

        if (prevSelectedGunIndex != selectedGunIndex) SelectGun();
    }

    private void SelectGun()
    {
        int i = 0;
        
        foreach (Transform gun in transform)
        {
            if (i == selectedGunIndex) gun.gameObject.SetActive(true);
            else gun.gameObject.SetActive(false);
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
