using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAndShoot : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 target;
    private GameObject crosshairs;
    private WeaponInventory weaponInventory;
    private GameObject selectedWeapon;
    private Object bulletPrefab;
    private Camera mCamera;
    private float lastAttackTime;
    public float firerate;
    public float bulletSpeed = 5.0f; 

    void Start()
    {
        firerate = 0.5f;
        Cursor.visible = false;
        crosshairs = GameObject.Find("crossHairs");
        weaponInventory = GameObject.Find("WeaponInventory").GetComponent<WeaponInventory>();
        selectedWeapon = weaponInventory.GetSelectedWeapon();
        Debug.Assert(selectedWeapon);
        bulletPrefab = Resources.Load("Textures/Prefabs/Hero/Bullet");
        mCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {

        target = mCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
        crosshairs.transform.position = new Vector3(target.x, target.y, -9f);
        selectedWeapon = weaponInventory.GetSelectedWeapon();
        if (selectedWeapon != null)
        {
            Vector3 difference = target - selectedWeapon.transform.position;
            float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

            if (Input.GetMouseButton(0))
            {
                selectedWeapon = weaponInventory.GetSelectedWeapon();
                float distance = difference.magnitude;
                Vector2 direction = difference / distance;
                direction.Normalize();
                selectedWeapon.GetComponent<Weapon>().FireWeapon(direction, rotationZ);
                
            }
        }
            if (Time.time > (lastAttackTime + firerate))
            {
                float distance = difference.magnitude;
                Vector2 direction = difference / distance;
                direction.Normalize();
                Shooting(direction, rotationZ);
                lastAttackTime = Time.time;
            }
        }

    }

    void Shooting(Vector2 direction, float rotationZ)
    {
        GameObject bullet = Instantiate(bulletPrefab) as GameObject;
        Vector2 startPos = player.transform.position;
        bullet.transform.position = new Vector2(startPos.x + 0.5f, startPos.y - 0.2f);
        bullet.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
        bullet.GetComponent<Rigidbody2D>().AddForce(direction * bulletSpeed, ForceMode2D.Impulse);
    }
}
