using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottle : ActiveItem
{
    private bool isFilled = false;

    // Start is called before the first frame update
    void Start()
    {
        itemID = 2;
        name = "Bottle";
        //GameWorld.onItemPickup += activateItem
    }

    public override void activateItem()
    {

    }

}
