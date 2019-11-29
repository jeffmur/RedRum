using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAndShoot : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject crosshairs;
    private WeaponInventory weaponInventory;
    private GameObject selectedWeapon;
    private Camera mCamera;

    void Start()
    {
        Cursor.visible = false;
        crosshairs = GameObject.Find("crossHairs");
        weaponInventory = GameObject.Find("WeaponInventory").GetComponent<WeaponInventory>();
        selectedWeapon = weaponInventory.GetSelectedWeapon();
        mCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 target = mCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
        crosshairs.transform.position = new Vector3(target.x, target.y, -9f);
        selectedWeapon = weaponInventory.GetSelectedWeapon();
        if (selectedWeapon != null)
        {
            if (Input.GetMouseButton(0))
            {
                FireWeapon(target);
            }
        }
    }

    private void FireWeapon(Vector3 target)
    {
        selectedWeapon = weaponInventory.GetSelectedWeapon();
        Vector3 difference = target - selectedWeapon.transform.position;
        Vector2 direction = difference.normalized; // distance;
        selectedWeapon.GetComponent<Weapon>().FireWeapon(direction);
    }
}
