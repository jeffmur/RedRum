using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TriggeredItem : HeldItem
{
    protected delegate void triggerDelegate();
    protected event triggerDelegate trigger;

    public override void process()
    {
        base.process();
        setTrigger();
    }

    public override void useHeldItem()
    {
        triggerItem();
    }

    protected abstract void triggerItem();
    protected abstract void setTrigger();
}
