using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item
{
    protected static int idNumber = 0;
    protected int itemID;
    protected bool alreadySpawned = false;

    public abstract void process();

    public void triggerSpawn()
    {
        alreadySpawned = true;
    }

    public bool getAlreadySpawned() { return alreadySpawned; }
}
