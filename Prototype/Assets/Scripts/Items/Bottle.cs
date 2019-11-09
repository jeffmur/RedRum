﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottle : TriggeredItem
{
    protected override void setItemInfo()
    {
        itemID = 2;
        itemName = "Bottle";
        caption = "Contains a small piece of fairy";

    }

    protected override void setTrigger()
    {
        //stats.onHealthChange += triggerItem;
    }

    protected override void triggerItem()
    {
        
    }
}