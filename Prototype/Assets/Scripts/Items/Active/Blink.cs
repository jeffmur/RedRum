using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : ActivatedItem
{
    public GameObject target;
    public bool targetHitWall;

    protected override void Awake()
    {
        base.Awake();
        CircleCollider2D col = target.AddComponent<CircleCollider2D>();
        col.isTrigger = true;
    }

    protected override void doItemEffect()
    {
        target.transform.position = Casper.Instance.transform.position;
        targetHitWall = false;
        target.SetActive(true);
        Vector3 blinkPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));
        blinkPosition.z = 0;
        CalculateTarget(blinkPosition);
        //targetHitWall = false;
        Casper.Instance.transform.position = CalculateTarget(blinkPosition);
    }

    private Vector3 CalculateTarget(Vector3 targetLocation)
    {
        target.transform.position = casper.transform.position;
        Vector3 targetTrace = (targetLocation - casper.transform.position).normalized;
        while (Vector3.Distance(target.transform.position, targetLocation) > 0.01f)
        {
            if (targetHitWall)
            {
                break;
            }
            target.transform.position += targetTrace * 0.01f;
        }
        return target.transform.position;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        targetHitWall = true;
    }

    protected override void endItemEffect()
    {
        return;
    }

    protected override void setActivateItemBehavior()
    {
        return;
    }

    protected override void setItemDurations()
    {
        cooldownDuration = 0;
        effectDuration = 0;
    }

    protected override void setItemInfo()
    {
        itemName = "Blink";
        caption = "It's nothing personal";
    }
}
