﻿using UnityEngine;

public abstract class HeldItem : Item
{
    protected IUseItem heldItemType;

    public override void process()
    {
        base.process();
        stats.setHeldItem(this);
    }

    public abstract void useHeldItem();
}