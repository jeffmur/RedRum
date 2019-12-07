using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Casper : SceneSingleton<Casper>
{
    public CasperData localCasperData;
    public PlayerData localPlayerData;
    public WeaponInventory weaponInventory;


    public delegate void CasperEventDelegate();

    // Start is called before the first frame update
    void Start()
    {
        localCasperData = GlobalControl.Instance.savedCasperData;
        localPlayerData = GlobalControl.Instance.savedPlayerData;
        EnableFire = true;
    }

    private void Update()
    {
        //selectedWeapon = weaponInventory.GetSelectedWeapon();
        if (Input.GetKeyDown(KeyCode.E) && isHovering)
        {
            ObtainEquipment(itemCollision);
        }
        CheckCasperFlash();
    }

    public void SaveData()
    {
        if(HeldItem != null)
            GlobalControl.Instance.saveItem(HeldItem.gameObject);
        GlobalControl.Instance.savedCasperData = localCasperData;
        GlobalControl.Instance.savedPlayerData = localPlayerData;
    }

    public bool EnableFire;
    public float MoveSpeed { get => localCasperData.Speed; set => localCasperData.Speed = value; }
    public int MaxHealth { get => localCasperData.MaxHealth; private set => localCasperData.MaxHealth = value; }
    public int CurrentHealth { get => localCasperData.CurrentHealth; private set => localCasperData.CurrentHealth = value; }
    public float FireRate { get => localCasperData.FireRate; set => localCasperData.FireRate = value; }
    private ActivatedItem HeldItem { get => localCasperData.CurrentActiveItem; set => localCasperData.CurrentActiveItem = value; }
    public bool IsInvincible { get => localCasperData.isInvincible; set => localCasperData.isInvincible = value; }
    public bool IsEtherial { get => localCasperData.isEtherial; 
        set { 
            localCasperData.isEtherial = value;
            Color col = GetComponent<SpriteRenderer>().color;
            col.a = value ? 0.5f : 1f;
            GetComponent<SpriteRenderer>().color = col;
        } 
    }

    public IEnumerator ToggleInvincibility(float time)
    {
        IsInvincible = true;
        yield return new WaitForSeconds(time);
        IsInvincible = false;
    }
    public void FireEquippedGun(Vector3 target)
    {
        if (EnableFire)
        {
            Weapon selectedWeapon = weaponInventory.GetSelectedWeapon();
            Vector3 difference = target - selectedWeapon.transform.position;
            Vector2 direction = difference.normalized;
            bool isFired = selectedWeapon.FireWeapon(direction);
            localPlayerData.totalShots += isFired ? 1 : 0;
        }
    }

    private IEnumerator Die()
    {
        yield return new WaitForSeconds(2);
        LevelManager.Dead();
    }
}
