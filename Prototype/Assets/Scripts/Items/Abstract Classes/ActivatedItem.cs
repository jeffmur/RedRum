using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActivatedItem : HeldItem
{
    public override void useHeldItem()
    {
        activateItem();
    }
    public abstract void activateItem();
}
