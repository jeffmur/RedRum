﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedSyringe : PassiveItem
{
    protected override void setItemInfo()
    {
        itemID = 4;
        itemName = "Speed Syringe";
        caption = "SPEED IS KEY!";
    }

    public override void modifyStats()
    {
        player.GetComponent<PlayerStats>().MoveSpeed += 1f; //speed up
        player.GetComponent<PointAndShoot>().fireRateMultiplier /= 1.5f; //increase the firerate
        //increase the firerate
        GameObject.Find("WeaponInventory")
            .GetComponent<WeaponInventory>()
            .GetSelectedWeapon()
            .GetComponent<Weapon>()
            .FireRate /= 3f; 
    }
}
