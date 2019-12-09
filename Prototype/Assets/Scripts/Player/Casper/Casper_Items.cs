using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Casper
{
    public GameObject HeldItemContainer;

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
        item.transform.parent = HeldItemContainer.transform;
    }

    private void pickUpItem(Item selectedItem)
    {
        if(selectedItem == null) { return; }
        selectedItem.process();
        Destroy(selectedItem.GetComponent<ItemSpawnBehavior>());
        updateCache(selectedItem);
  
        onItemPickup?.Invoke(selectedItem);
        ItemPickupEvent?.Invoke();
    }

    private void DropItem(ActivatedItem item)
    {
        HeldItem.showItem();
        HeldItem.tag = "Item";
        HeldItem.transform.position = transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        HeldItem.transform.parent = returnToRoom(); // Back in Room
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
        RoomRegister rr = obj.GetComponent<RoomRegister>();
        if (rr == null)
        {
            rr = obj.gameObject.AddComponent<RoomRegister>();
            // has not been pickedup
            rr.RoomIndex = currentRoomIndex;
            rr.OriginalScale = obj.transform.localScale * transform.localScale.x;
        }
        // Already has been spawned || being dropped
        else
        {
            rr.RoomIndex = currentRoomIndex;
            //if(rr.OriginalScale != Vector3.zero)
            //    obj.transform.localScale = rr.OriginalScale;
        }
        // Hide/Show
        obj.tag = "Item";
    }

    private Transform returnToRoom()
    {
        // Find all rooms?
        var myRooms = FindObjectsOfType<RoomStats>();
        // Iterate through each component <RoomStats>
        for(int i = 0; i < myRooms.Length; i++)
        {
            if (myRooms[i].isInRoom(transform.position))
                return myRooms[i].transform;
        }
        return null;
        // return transform when true
    }
}
