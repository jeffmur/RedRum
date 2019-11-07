﻿using UnityEngine;

public abstract class HeldItem : Item
{
    protected float cooldownTimer = -1;
    private float effectDuration = -1;

    public float CooldownTimer { get => cooldownTimer; set => cooldownTimer = value; }
    public float EffectDuration { get => effectDuration; set => effectDuration = value; }

    public abstract void activateItem();

    protected override void Awake()
    {
        base.Awake();
    }
    //protected abstract void processActivatedItem();


    public override void process()
    {
        base.process();
        //player.
        //TODO: set player's active item to this one
        stats.setActiveItem(this);
    }
}