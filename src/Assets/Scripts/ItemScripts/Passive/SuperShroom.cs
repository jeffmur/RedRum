﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperShroom : PassiveItem
{
    protected override void setItemInfo()
    {
        itemID = 4;
        itemName = "Super Shroom";
        caption = "The Classic";
    }

    public override void modifyStats()
    {
        casper.transform.localScale *= 1.2f;
        casper.GetComponent<Casper>().changeMaxHealth(2);
    }
}
