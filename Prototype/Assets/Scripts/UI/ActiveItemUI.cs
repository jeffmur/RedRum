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
        if (item is ActivatedItem)
        {
            SpriteRenderer rend = item.GetComponent<SpriteRenderer>();
            itemImage.sprite = rend.sprite;
            itemImage.color = Color.white;
        }
    }

    public void updateItemOnActivate(ActivatedItem item)
    {
        if (item.isOneTimeUse())
        {
            itemImage.sprite = null;
        }
    }
}