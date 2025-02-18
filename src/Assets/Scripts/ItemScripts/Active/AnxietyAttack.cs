﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnxietyAttack : ActivatedItem
{
    private Weapon weapon;

    protected override void doItemEffect()
    {
        float x = Random.Range(-1f, 1f);
        float y = Random.Range(-1f, 1f);
        Vector2 direction = new Vector2(x, y);
        weapon = casper.transform.Find("WeaponInventory").GetComponent<WeaponInventory>().GetSelectedWeapon().GetComponent<Weapon>();
        weapon.Shoot(direction);
    }

    protected override void setActivateItemBehavior()
    {
        return;
    }

    protected override void setItemDurations()
    {
        cooldownDuration = 10;
        effectDuration = 3;
        frameTick = 3;
    }
    protected override void setItemInfo()
    {
        itemName = "Anxiety Attack";
        itemID = 10;
        caption = "AHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHH";
    }

    protected override void endItemEffect()
    {
        return;
    }
}
