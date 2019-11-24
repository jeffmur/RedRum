using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnxietyAttack : ActivatedItem
{
    private Weapon weapon;
    protected override void setItemInfo()
    {
        itemName = "Anxiety Attack";
        itemID = 10;
        caption = "AAAAAAAAAAAAAAAAAA";
    }
    protected override void doItemEffect()
    {
        float x = Random.Range(-1f, 1f);
        float y = Random.Range(-1f, 1f);
        Vector2 direction = new Vector2(x, y);
        weapon = player.GetComponent<PointAndShoot>().getSelectedWeapon();
        weapon.FireWeapon(direction);
    } 

    protected override void setItemDurations()
    {
        cooldownDuration = 3;
        effectDuration = 3;
        frameTick = 1;
    }
}
