using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperRifle : Weapon
{
    public LineRenderer laser;
    private GameObject crosshair;
    private Transform weaponInventory;
    
    private void Start()
    {
        crosshair = GameObject.Find("crossHairs");
        laser.useWorldSpace = true;
        laser.enabled = false;
    }
    void Shoot()
    {
        if (transform.parent != null)
        {
            laser.enabled = true;
            laser.SetPosition(0, transform.position);
            laser.SetPosition(laser.positionCount - 1, crosshair.transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
    }
}
