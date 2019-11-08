using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneTimeUseItem : IUseItem
{
    public void useItem(HeldItem item)
    {
        GameObject.Destroy(item.gameObject);
    }
}
