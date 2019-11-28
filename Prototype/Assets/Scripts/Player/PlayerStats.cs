using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasperData
{
    public float FireRate = 1f;
    public int CurrentHealth = 5;
    public int MaxHealth = 5;
    public int CurrentAmmo = 8;
    public int MaxAmmo = 8;
    public float Speed = 5f;
    public float damageModifier = 1;
    public Vector3 Scale = new Vector3(6, 6, 1);
    public bool isInvincible = false;
    public ActivatedItem CurrentActiveItem;
    public bool[] WeaponInventory;

}
public class PlayerData
{
    public float totalShots = 0;
    public int roomsCleared = -1;
    public float bulletsHit = 0;
    public int itemsPickedUp = 0;
    public int enemiesKilled = 0;
}

public class PlayerStats : MonoBehaviour
{
    public CasperData localCasperData;
    public PlayerData localPlayerData;
    public List<Item> passiveItems;

    private bool isEtherial; //need player collider

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
        localPlayerData = GlobalControl.Instance.savedPlayerData;
    }

    public void SaveData()
    {
        if(HeldItem != null)
            GlobalControl.Instance.saveItem(HeldItem.gameObject);
        GlobalControl.Instance.savedCasperData = localCasperData;
        GlobalControl.Instance.savedPlayerData = localPlayerData;
    }

    public float MoveSpeed { get => localCasperData.Speed; set => localCasperData.Speed = value; }
    public int MaxHealth { get => localCasperData.MaxHealth; }
    public int CurrentHealth { get => localCasperData.CurrentHealth; }
    public int MaxAmmo { get => localCasperData.MaxAmmo; set => localCasperData.MaxAmmo = value; }
    public int CurrentAmmo { get => localCasperData.CurrentAmmo; set => localCasperData.CurrentAmmo = value; }

    public float FireRate { get => localCasperData.FireRate; set => localCasperData.FireRate = value; }
    private ActivatedItem HeldItem { get => localCasperData.CurrentActiveItem; set => localCasperData.CurrentActiveItem = value; }

    public bool[] WeaponInventory { get => localCasperData.WeaponInventory; set => localCasperData.WeaponInventory = value; }
    public bool IsEtherial { get => isEtherial; set { 
            isEtherial = value;
            GetComponent<Collider2D>().isTrigger = value;
        } 
    }

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
            if (localCasperData.MaxHealth > 20)
            {
                localCasperData.MaxHealth = 20;
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

        if (value < 0 && (localCasperData.isInvincible || IsEtherial))
        {
            print("Casper is invincible");
            return;
        }
        localCasperData.CurrentHealth += value;
        if (localCasperData.CurrentHealth > localCasperData.MaxHealth)
        {
            localCasperData.CurrentHealth = localCasperData.MaxHealth;
        }
        if (CurrentHealth == 1 && name == "Casper") { StartCoroutine(SlowMotion.DoSlowMotion(5, 0.05f)); }
        if (localCasperData.CurrentHealth <= 0 && name == "Casper")
        {
            StartCoroutine(SlowMotion.DoSlowMotion(2, 0.05f));
            Invoke("Die", 2f); //dies after 5 seconds
        }
        onHealthChange?.Invoke(localCasperData.CurrentHealth);
        if (value > 0)
        {
            onHealed?.Invoke();
        }
        else
        {
            StartCoroutine(ToggleInvincibility());
            onDamaged?.Invoke();
        }
    }

    public IEnumerator ToggleInvincibility()
    {
        localCasperData.isInvincible = true;
        yield return new WaitForSeconds(1);
        localCasperData.isInvincible = false;
    }

    public void setActivatedItem(ActivatedItem item)
    {
        if (HeldItem != null)
        {
            HeldItem.showItem();
            HeldItem.tag = "Item";
            HeldItem.transform.position = transform.position;
        }
        HeldItem = item;
        HeldItem.tag = "PickedUp";
        item.hideItem();
    }

    public void changeAmmo(int value)
    {
        if(value == 0) { return; }

        if(value == -1)
            localCasperData.CurrentAmmo += value;
        else
            localCasperData.CurrentAmmo = value;

        if (localCasperData.CurrentAmmo > localCasperData.MaxAmmo)
            localCasperData.CurrentAmmo = localCasperData.MaxAmmo;

        if (localCasperData.CurrentAmmo < 0)
            localCasperData.CurrentAmmo = 0;

        onAmmoChange?.Invoke(localCasperData.CurrentAmmo);
    }

    public ItemFloatingText hint;


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            if (collision.gameObject.GetComponent<SpriteRenderer>())
            {
                string message = "Press E to pick up";
                hint.showFloatingText(collision.gameObject.transform.position, message);
            }
        }
        if (collision.gameObject.tag == "Weapon")
        {
            if (collision.gameObject.GetComponent<SpriteRenderer>())
            {
                string message = "Press E to equip";
                hint.showFloatingText(collision.gameObject.transform.position, message);
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (collision.gameObject.tag == "Item")
            {
                if (collision.gameObject.GetComponent<SpriteRenderer>())
                {
                    pickUpItem(collision.gameObject.GetComponent<Item>());
                    hint.gameObject.SetActive(false);
                }
            }
            if (collision.gameObject.tag == "Heart")
            {
                GameObject.Destroy(collision.gameObject);
                changeHealth(1);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Item" || collision.gameObject.tag == "Weapon")
        {
            hint.gameObject.SetActive(false);
        }
    }

    private void pickUpItem(Item selectedItem)
    {
        selectedItem.process();
        onItemPickup?.Invoke(selectedItem);
    }
    public void activateItem()
    {
        if (HeldItem != null && !HeldItem.isOnCooldown)
        {
            HeldItem.activateItem();
            onItemUse?.Invoke(HeldItem);
        }
    }

    private void Die()
    {
        LevelManager.Dead();
    }
}
