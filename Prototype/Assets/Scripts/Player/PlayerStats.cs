using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private float fireRateModifier;
    private float accuracyPercentage;
    public int currentHealth, maxHealth;
    public int currentAmmo, maxAmmo;
    private float moveSpeed;
    //private float isInvincible;
    private bool isInvincible;
    public List<Item> passiveItems;
    public ActivatedItem currentActiveItem;

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
    void Awake()
    {
        moveSpeed = 5f;

        maxHealth = 3;
        currentHealth = maxHealth;

        maxAmmo = 8;
        currentAmmo = maxAmmo;
    }


    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public int MaxHealth { get => maxHealth; }
    public int CurrentHealth { get => currentHealth; }
    public ActivatedItem CurrentActiveItem { get => currentActiveItem; }

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
            maxHealth = currentHealth;
            onMaxHealthChange?.Invoke(maxHealth);
        }
        if (currentHealth <= 0)
        {
            
            Debug.Log("CASPER DEAD");
            Die();
        }
        onHealthChange?.Invoke(currentHealth);
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
        if (currentActiveItem != null)
        {
            currentActiveItem.gameObject.SetActive(true);
            currentActiveItem.transform.position = transform.position;
        }
        currentActiveItem = item;
        item.gameObject.SetActive(false);
    }

    public void changeAmmo(int value)
    {
        if(value == 0) { return; }

        currentAmmo += value;

        if (currentAmmo > maxAmmo)
            currentAmmo = maxAmmo;

        if (currentAmmo < 0)
            currentAmmo = 0;
        onAmmoChange?.Invoke(currentAmmo);
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
        currentActiveItem.activateItem();
        onItemUse?.Invoke(currentActiveItem);
    }

    private void Die()
    {
        Scenes.Load("Alpha", Scenes.nextDem());
    }
}