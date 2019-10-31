using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PassiveItem : Item
{
    public override void process()
    {
        base.process();
        modifyStats();
        player.GetComponent<PlayerStats>().heldItems.Add(this);
         
    }
    public abstract void modifyStats();
}

