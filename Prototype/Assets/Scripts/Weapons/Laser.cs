using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public LineRenderer laser;
    private GameObject crosshair;
    
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
            // THIS IS FUCKING STUPID
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
