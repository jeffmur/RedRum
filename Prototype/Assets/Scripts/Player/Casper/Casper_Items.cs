using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Casper
{
    public delegate void onItemDelegate(Item item);
    public event onItemDelegate onItemPickup;

    public delegate void onActiveItemDelegate(ActivatedItem item);
    public event onActiveItemDelegate onItemUse;

    public event CasperEventDelegate ItemUseEvent, ItemPickupEvent;

    public void setActivatedItem(ActivatedItem item)
    {
        if (HeldItem != null)
        {
            HeldItem.showItem();
            HeldItem.tag = "Item";            
            HeldItem.transform.position = transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            HeldItem.transform.parent = null;
        }
        HeldItem = item;
        HeldItem.tag = "PickedUp";
        item.hideItem();
        item.transform.parent = gameObject.transform;
    }

    private void pickUpItem(Item selectedItem)
    {
        selectedItem.process();
        Destroy(selectedItem.GetComponent<ItemSpawnBehavior>());
  
        onItemPickup?.Invoke(selectedItem);
        ItemPickupEvent?.Invoke();
    }

    public void activateItem()
    {
        if (HeldItem != null && !HeldItem.isOnCooldown)
        {
            HeldItem.activateItem();
            onItemUse?.Invoke(HeldItem);
            ItemUseEvent?.Invoke();
        }
    }
}
