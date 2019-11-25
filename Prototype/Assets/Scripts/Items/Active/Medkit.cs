using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medkit : ActivatedItem
{
    protected override void setActivateItemBehavior()
    {
        int full = stats.MaxHealth - stats.CurrentHealth;
        stats.changeHealth(full);
    }

    protected override void setItemDurations()
    {
        effectDuration = -1;
        cooldownDuration = -1;
    }

    protected override void doItemEffect()
    {
        return;
    }

    protected override void setItemInfo()
    {
        itemID = 3;
        itemName = "Medkit";
        caption = "Did someone call a doctor?";
    }

    protected override void endItemEffect()
    {
        return;
    }
}
