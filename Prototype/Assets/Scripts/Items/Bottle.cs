﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottle : HeldItem
{
    private bool isFilled = false;
    private int playerHealth = -1;

    protected override void Awake()
    {
        base.Awake();
        itemID = 2;
        itemName = "Bottle";
        caption = "Contains a small piece of fairy";

        stats.onHealthChange += checkHealth;
    }

    public override void activateItem()
    {
        if (playerHealth == 0)
        {
            player.GetComponent<PlayerStats>().changeHealth(5);
        }
    }
    public void checkHealth(int hp)
    {
        playerHealth = stats.CurrentHealth;
    }
}