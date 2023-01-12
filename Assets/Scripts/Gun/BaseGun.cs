using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BaseGun : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerInput.OnFootActions onFootActions;
    private PlayerInput.WeaponActions weaponActions;
    private Camera cam;
    private Ray gunRay;
    private TextMeshProUGUI bulletsLeftLabel;

    private bool isReloading;
    private float timeSinceLastShot;
    private int bulletsLeft, totalBulletsLeft;

    [SerializeField] private BaseGunInfo gunInfo;

    private void Awake()
    {
        playerInput = new PlayerInput();
        
        // Setting variables
        cam = transform.parent.parent.GetComponent<Camera>();
        bulletsLeftLabel = transform.parent.GetComponent<GunSwitcher>().bulletsLeftLabel;
        
        // Initialize variables
        onFootActions = playerInput.OnFoot;
        weaponActions = playerInput.Weapon;
        bulletsLeft = gunInfo.bullets;
        totalBulletsLeft = gunInfo.totalBullets;

        // Reload
        weaponActions.Reload.performed += ctx => StartCoroutine(Reload());
    }

    private void Update()
    {
        Debug.DrawRay(gunRay.origin, gunRay.direction);

        bulletsLeftLabel.text = bulletsLeft.ToString() + " / " + totalBulletsLeft.ToString();
        timeSinceLastShot += Time.deltaTime;

        if (Input.GetMouseButton(0)) Fire();
    }

    private void Fire()
    {
        if (bulletsLeft == 0) StartCoroutine(Reload());
        if (isReloading || timeSinceLastShot < gunInfo.intervalBetweenShots || totalBulletsLeft == 0 && bulletsLeft == 0) return;
        
        bulletsLeft--;
        timeSinceLastShot = 0;
        
        // Create a ray from the camera going in the forward direction
        gunRay = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hitInfo;

        // Shoot and check if the raycast hit something
        if (Physics.Raycast(gunRay, out hitInfo, gunInfo.attackRange, gunInfo.targetMask))
        {
            Enemy enemy = hitInfo.collider.GetComponent<Enemy>();
            enemy.TakeDamage(gunInfo.damage);
        }
    }

    private IEnumerator Reload()
    {
        if (bulletsLeft == gunInfo.bullets || totalBulletsLeft == 0 || isReloading) yield break;
        
        isReloading = true;
        
        yield return new WaitForSeconds(gunInfo.reloadTime); // Wait for some time

        bulletsLeft = gunInfo.bullets;
        totalBulletsLeft--;

        isReloading = false;
    }

    private void OnEnable()
    {
        onFootActions.Enable();
        weaponActions.Enable();
    }

    private void OnDisable()
    {
        onFootActions.Disable();
        weaponActions.Disable();
    }
}
