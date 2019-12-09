using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public LineRenderer laser;
    private GameObject crosshair;
    private bool waitToShow = false;
    private void Start()
    {
        crosshair = GameObject.Find("crossHairs");
        laser.useWorldSpace = true;
    }
    void Shoot()
    {
        laser.enabled = waitToShow;
        if (transform.parent != null)
        {
            waitToShow = true;
            laser.enabled = waitToShow;
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
