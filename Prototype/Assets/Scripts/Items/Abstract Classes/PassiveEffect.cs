using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PassiveEffect : Item
{
    protected GameObject itemEffectManager;

    protected override void Awake()
    {
        base.Awake();
        itemEffectManager = GameObject.Find("ItemEffects");
        this.enabled = false;
    }
    public override void process()
    {
        base.process();
        AddEffect();
        Destroy(gameObject);
        Destroy(itemEffectManager.GetComponent<BoxCollider2D>());
    }

    //itemEffectManager.AddComponent<CHILDCLASS>();
    //itemEffectManager.GetComponent<CHILDCLASS>().enabled = true;
    protected abstract void AddEffect();
    protected abstract void Update();
}
