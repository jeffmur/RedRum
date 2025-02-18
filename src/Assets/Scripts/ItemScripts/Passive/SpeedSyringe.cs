﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedSyringe : PassiveItem
{
    protected override void setItemInfo()
    {
        itemID = 4;
        itemName = "Speed Syringe";
        caption = "Gotta Go Fast";
    }

    public override void modifyStats()
    {
        casper.GetComponent<Casper>().MoveSpeed += 1f; //speed up
        casper.GetComponent<Casper>().FireRate /= 1.2f; //increase the firerate
        //increase the firerate
        GameObject.Find("WeaponInventory")
            .GetComponent<WeaponInventory>()
            .GetSelectedWeapon()
            .GetComponent<Weapon>()
            .FireRate /= 3f;
    }
}
