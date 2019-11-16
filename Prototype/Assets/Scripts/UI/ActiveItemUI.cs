using System;
using UnityEngine;
using UnityEngine.UI;

public class ActiveItemUI : MonoBehaviour
{
    public Image itemImage;
    //public CooldownBar cooldownBar;
    private float cooldownTimer;

    public void displayActiveItem(Item item)
    {
        print(item);
        if (item is HeldItem)
        {
            SpriteRenderer rend = item.GetComponent<SpriteRenderer>();
            itemImage.sprite = rend.sprite;
            itemImage.color = Color.white;
        }
    }

    public void updateItemUI(HeldItem item)
    {
        //if (item is OneTimeUseItem)
        //{
        //    itemImage.sprite = null;
        //}
        //else if (item is CooldownItem)
        //{
        //    cooldownBar.echoCooldownBar(item.CooldownTimer);
        //}
    }
}