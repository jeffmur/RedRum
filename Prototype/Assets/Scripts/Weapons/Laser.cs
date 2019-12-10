using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public LineRenderer laser;
    private GameObject crosshair;
    private WeaponInventory weaponInventory;
    public bool waitToShow = false;
    private void Start()
    {
        crosshair = GameObject.Find("crossHairs");
        weaponInventory = Casper.Instance.weaponInventory;
        laser.enabled = waitToShow;
        //sniper = gameObject.GetComponentInParent<Weapon>(); // if we wanted to map properly to the sniper offset
    }
    void Shoot()
    {
        if (weaponInventory.GetSelectedWeapon().name == "Sniper(Clone)")
        {
            Debug.Log(weaponInventory.GetSelectedWeapon().name);
            laser.useWorldSpace = true; //wait for the Sniper to be picked up 
            laser.enabled = waitToShow;
            if (transform.parent != null)
            {
                waitToShow = true;
                laser.enabled = waitToShow;
                laser.SetPosition(0, transform.position);
                laser.SetPosition(laser.positionCount - 1, crosshair.transform.position);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
    }
}
