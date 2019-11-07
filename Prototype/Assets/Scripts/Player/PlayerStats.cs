using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private float fireRate;
    private float accuracy;
    public int currentHealth, maxHealth;
    private float moveSpeed;
    private float cooldownrate;
    //private float isInvincible;
    private bool isInvincible;
    public List<Item> heldItems;
    public HeldItem currentActiveItem;

    public delegate void onHealthChangeDelegate(int value);
    public event onHealthChangeDelegate onHealthChange, onMaxHealthChange;

    public delegate void onItemDelegate(Item item);
    public event onItemDelegate onItemPickup;

    public delegate void onActiveItemDelegate(HeldItem item);
    public event onActiveItemDelegate onItemActivate;

    // Start is called before the first frame update
    void Awake()
    {
        moveSpeed = 5f;
        maxHealth = 3;
        currentHealth = maxHealth;
    }


    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public float Cooldownrate { get => cooldownrate; set => cooldownrate = value; }
    public int MaxHealth { get => maxHealth; }
    public int CurrentHealth { get => currentHealth; }
    public HeldItem CurrentActiveItem { get => currentActiveItem; }

    public void changeMaxHealth(int value)
    {
        if (value == 0)
        {
            return;
        }

        maxHealth += value;
        int healthChanged;

        if (value > 0)
        {
            if (maxHealth > 10)
            {
                maxHealth = 10;
            }
            changeHealth(value);
        }
        if (value < 0)
        {
            if (maxHealth < 1)
            {
                maxHealth = 1;
                changeHealth(-(currentHealth - 1));
            }
            else
            {
                int previousMaxHealth = maxHealth - value;
                int missingHealth = previousMaxHealth - currentHealth;
                healthChanged = (missingHealth + value >= 0) ? 0 : value + missingHealth;
                healthChanged = (currentHealth - healthChanged < 1) ? 1 : healthChanged;
                changeHealth(-healthChanged);
            }
        }
        onMaxHealthChange?.Invoke(maxHealth);
    }

    public void changeHealth(int value)
    {
        if (value == 0)
        {
            return;
        }

        currentHealth += value;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
        onHealthChange?.Invoke(currentHealth);
    }

    public void setActiveItem(HeldItem item)
    {
        if (currentActiveItem != null)
        {
            currentActiveItem.gameObject.SetActive(true);
            currentActiveItem.transform.position = transform.position;
        }
        currentActiveItem = item;
        item.gameObject.SetActive(false);
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
    private void activateActiveItem()
    {
        currentActiveItem.activateItem();
        onItemActivate?.Invoke(currentActiveItem);
    }
}