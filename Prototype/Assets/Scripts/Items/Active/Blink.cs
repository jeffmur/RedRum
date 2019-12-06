using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : ActivatedItem
{
    protected override void doItemEffect()
    {
        Vector3 blinkPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));
        blinkPosition.z = 0;
        Casper.Instance.transform.localPosition = blinkPosition;
    }

    protected override void endItemEffect()
    {
        return;
    }

    protected override void setActivateItemBehavior()
    {
        return;
    }

    protected override void setItemDurations()
    {
        cooldownDuration = 0;
        effectDuration = 0;
    }

    protected override void setItemInfo()
    {
        itemName = "Blink";
        caption = "It's nothing personal";
    }
}
