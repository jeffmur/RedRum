using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinach : PassiveItem
{
    public override void modifyStats()
    {
        casperData.localCasperData.damageModifier += 0.75f;
    }
    protected override void setItemInfo()
    {
        itemName = "Spinach";
        itemID = 5;
        caption = "Grows strong bones";
    }
}
