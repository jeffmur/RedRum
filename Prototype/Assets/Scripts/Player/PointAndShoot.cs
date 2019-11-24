using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAndShoot : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject crosshairs;
    private WeaponInventory weaponInventory;
    private GameObject selectedWeapon;
    private Object bulletPrefab;
    private Camera mCamera;
    public float fireRateMultiplier;

    void Start()
    {
        Cursor.visible = false;
        crosshairs = GameObject.Find("crossHairs");
        weaponInventory = GameObject.Find("WeaponInventory").GetComponent<WeaponInventory>();
        selectedWeapon = weaponInventory.GetSelectedWeapon();
        bulletPrefab = Resources.Load("Textures/Prefabs/Hero/Bullet");
        mCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.visible = false;
        Vector3 target = mCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
        crosshairs.transform.position = new Vector3(target.x, target.y, -9f);
        selectedWeapon = weaponInventory.GetSelectedWeapon();
        if (selectedWeapon != null)
        {
            Vector3 difference = target - selectedWeapon.transform.position;
            if (Input.GetMouseButton(0))
            {
                selectedWeapon = weaponInventory.GetSelectedWeapon();
                float distance = difference.magnitude;
                Vector2 direction = difference / distance;
                direction.Normalize();
                selectedWeapon.GetComponent<Weapon>().ProcessFireWeapon(direction, fireRateMultiplier);

            }
        }
    }
    public Weapon getSelectedWeapon()
    {
        return selectedWeapon.GetComponent<Weapon>();
    }
}
