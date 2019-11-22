﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasperData
{
    public float FireRate = 1f;
    public int CurrentHealth = 3;
    public int MaxHealth = 3;
    public int CurrentAmmo = 8;
    public int MaxAmmo = 8;
    public float Speed = 5f;
    public Vector3 Scale = new Vector3(6, 6, 1);
    public ActivatedItem CurrentActiveItem;
}
public class PlayerData
{
    public int roomsCleared;
    public float accuracy;
    public int enemiesKilled;
    // any more?
}

public class PlayerStats : MonoBehaviour
{
    public CasperData localCasperData;
    public List<Item> passiveItems;

    public delegate void onHealthChangeDelegate(int value);
    public event onHealthChangeDelegate onHealthChange, onMaxHealthChange;

    public delegate void onHealthCheckDelegate();
    public event onHealthCheckDelegate onDamaged, onHealed;

    public delegate void onAmmoChangeDelegate(int val);
    public event onAmmoChangeDelegate onAmmoChange;

    public delegate void onItemDelegate(Item item);
    public event onItemDelegate onItemPickup;

    public delegate void onActiveItemDelegate(ActivatedItem item);
    public event onActiveItemDelegate onItemUse;

    // Start is called before the first frame update
    void Start()
    {
        localCasperData = GlobalControl.Instance.savedCasperData;
    }

    public void SavePlayer()
    {
        if(HeldItem != null)
            GlobalControl.Instance.saveItem(HeldItem.gameObject);
        GlobalControl.Instance.savedCasperData = localCasperData;
    }

    public float MoveSpeed { get => localCasperData.Speed; set => localCasperData.Speed = value; }
    public int MaxHealth { get => localCasperData.MaxHealth; }
    public int CurrentHealth { get => localCasperData.CurrentHealth; }
    public int MaxAmmo { get => localCasperData.MaxAmmo; }

    public float FireRate { get => localCasperData.FireRate; set => localCasperData.FireRate = value; }
    private ActivatedItem HeldItem { get => localCasperData.CurrentActiveItem; set => localCasperData.CurrentActiveItem = value; }

    public void changeMaxHealth(int value)
    {
        if (value == 0)
        {
            return;
        }

        localCasperData.MaxHealth += value;
        int healthChanged;

        if (value > 0)
        {
            if (localCasperData.MaxHealth > 10)
            {
                localCasperData.MaxHealth = 10;
            }
            changeHealth(value);
        }
        if (value < 0)
        {
            if (localCasperData.MaxHealth < 1)
            {
                localCasperData.MaxHealth = 1;
                changeHealth(-(localCasperData.CurrentHealth - 1));
            }
            else
            {
                int previousMaxHealth = localCasperData.MaxHealth - value;
                int missingHealth = previousMaxHealth - localCasperData.CurrentHealth;
                healthChanged = (missingHealth + value >= 0) ? 0 : value + missingHealth;
                healthChanged = (localCasperData.CurrentHealth - healthChanged < 1) ? 1 : healthChanged;
                changeHealth(-healthChanged);
            }
        }
        onMaxHealthChange?.Invoke(localCasperData.MaxHealth);
    }

    public void changeHealth(int value)
    {
        if (value == 0)
        {
            return;
        }
        localCasperData.CurrentHealth += value;
        if (localCasperData.CurrentHealth > localCasperData.MaxHealth)
        {
            localCasperData.MaxHealth = localCasperData.CurrentHealth;
            onMaxHealthChange?.Invoke(localCasperData.MaxHealth);
        }
        if (localCasperData.CurrentHealth <= 0)
        {
            
            Debug.Log("CASPER DEAD");
            Die();
        }
        onHealthChange?.Invoke(localCasperData.CurrentHealth);
        if (value > 0)
        {
            onHealed?.Invoke();
        }
        else
        {
            onDamaged?.Invoke();
        }
    }

    public void setActivatedItem(ActivatedItem item)
    {
        if (HeldItem != null)
        {
            HeldItem.showItem();
            HeldItem.transform.position = transform.position;
        }
        HeldItem = item;
        item.hideItem();
    }

    public void changeAmmo(int value)
    {
        if(value == 0) { return; }

        localCasperData.CurrentAmmo += value;

        if (localCasperData.CurrentAmmo > localCasperData.MaxAmmo)
            localCasperData.CurrentAmmo = localCasperData.MaxAmmo;

        if (localCasperData.CurrentAmmo < 0)
            localCasperData.CurrentAmmo = 0;
        onAmmoChange?.Invoke(localCasperData.CurrentAmmo);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (collision.gameObject.tag == "Item")
            {              
                pickUpItem(collision.gameObject.GetComponent<Item>());
            }
            if (collision.gameObject.tag == "Heart")
            {
                GameObject.Destroy(collision.gameObject);
                changeHealth(1);
            }
        }
    }
    private void pickUpItem(Item selectedItem)
    {       
        selectedItem.process();
        onItemPickup?.Invoke(selectedItem);
    }
    public void activateItem()
    {
        if (HeldItem != null)
        {
            HeldItem.activateItem();
            onItemUse?.Invoke(HeldItem);
        }
    }

    private void Die()
    {
        Scenes.Load("Alpha", false);
    }
}