using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medkit : ActivatedItem
{
    public override void activateItem()
    {
        int full = stats.maxHealth - stats.currentHealth;
        stats.changeHealth(full);
    }

    protected override void setItemInfo()
    {
        itemID = 3;
        itemName = "Medkit";
        caption = "I NEED MEDIC";
    }
}
