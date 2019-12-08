using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperRifle : Weapon
{
    public LineRenderer laser;
    private GameObject crosshair;
    
    private void Start()
    {
        crosshair = GameObject.Find("crossHairs");
    }
    void Shoot()
    {
        float dist = Vector3.Distance(crosshair.transform.position, transform.position);
        laser.SetPosition(1, new Vector3(dist, 0, 0));
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
    }
}
