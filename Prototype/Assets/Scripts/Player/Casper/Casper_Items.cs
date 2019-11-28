using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Casper : MonoBehaviour
{

    public delegate void onItemDelegate(Item item);
    public event onItemDelegate onItemPickup;

    public delegate void onActiveItemDelegate(ActivatedItem item);
    public event onActiveItemDelegate onItemUse;

    public void setActivatedItem(ActivatedItem item)
    {
        if (HeldItem != null)
        {
            HeldItem.showItem();
            HeldItem.tag = "Item";
            HeldItem.transform.position = transform.position;
        }
        HeldItem = item;
        HeldItem.tag = "PickedUp";
        item.hideItem();
    }

    private void pickUpItem(Item selectedItem)
    {
        selectedItem.process();
        onItemPickup?.Invoke(selectedItem);
    }

    public void activateItem()
    {
        if (HeldItem != null && !HeldItem.isOnCooldown)
        {
            HeldItem.activateItem();
            onItemUse?.Invoke(HeldItem);
        }
    }
}
