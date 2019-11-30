using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Casper : SceneSingleton<Casper>
{
    public CasperData localCasperData;
    public PlayerData localPlayerData;
    public List<Item> passiveItems;


    protected override void Awake()
    {
        base.Awake();
        selectedWeapon = weaponInventory.GetSelectedWeapon();
        
        //MaxAmmo = selectedWeapon.GetComponent<Weapon>().ClipSize;
        //changeAmmo(MaxAmmo);
    }

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
    public int MaxHealth { get => localCasperData.MaxHealth; private set => localCasperData.MaxHealth = value; }
    public int CurrentHealth { get => localCasperData.CurrentHealth; private set => localCasperData.CurrentHealth = value; }
    public int MaxAmmo { get => localCasperData.MaxAmmo; set => localCasperData.MaxAmmo = value; }
    public int CurrentAmmo { get => localCasperData.CurrentAmmo; set => localCasperData.CurrentAmmo = value; }
    public float FireRate { get => localCasperData.FireRate; set => localCasperData.FireRate = value; }
    private ActivatedItem HeldItem { get => localCasperData.CurrentActiveItem; set => localCasperData.CurrentActiveItem = value; }
    public bool[] WeaponInventory { get => localCasperData.WeaponInventory; set => localCasperData.WeaponInventory = value; }
    public bool IsInvincible { get => localCasperData.isInvincible; set => localCasperData.isInvincible = value; }
    public bool IsEtherial { get => localCasperData.isEtherial; 
        set { 
            localCasperData.isEtherial = value;
            Color col = GetComponent<SpriteRenderer>().color;
            col.a = value ? 0.25f : 1f;
            GetComponent<SpriteRenderer>().color = col;
        } 
    }

    public IEnumerator ToggleInvincibility(float time)
    {
        IsInvincible = true;
        yield return new WaitForSeconds(time);
        IsInvincible = false;
    }

    private IEnumerator Die()
    {
        yield return new WaitForSeconds(2);
        LevelManager.Dead();
    }
}
