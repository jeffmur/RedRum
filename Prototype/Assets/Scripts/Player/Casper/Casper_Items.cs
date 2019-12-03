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

    public int currentRoomIndex = 0;

    public void setActivatedItem(ActivatedItem item)
    {
        if (HeldItem != null)
        {
            DropItem(item);
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

    private void DropItem(ActivatedItem item)
    {
        HeldItem.showItem();
        HeldItem.tag = "Item";
        HeldItem.transform.position = transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        HeldItem.transform.parent = item.transform.parent; // Back in Room
        HeldItem.transform.rotation = Quaternion.Euler(0, 0, 0);
        updateCache(HeldItem);
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

    private void updateCache(Item obj)
    {
        // TODO: allow to getcurrentRoomIndex
        if (obj.GetComponent<RoomRegister>() == null)
            obj.gameObject.AddComponent<RoomRegister>().RoomIndex = currentRoomIndex;
        else
            obj.GetComponent<RoomRegister>().RoomIndex = currentRoomIndex;
        // Hide/Show
        obj.tag = "Item";
    }
}
