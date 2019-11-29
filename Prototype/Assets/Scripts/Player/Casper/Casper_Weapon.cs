using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Casper
{
    public delegate void onAmmoChangeDelegate(int val);
    public event onAmmoChangeDelegate onAmmoChange;

    public WeaponInventory weaponInventory;
    public Weapon selectedWeapon;

    private void Update()
    {
        selectedWeapon = weaponInventory.GetSelectedWeapon().GetComponent<Weapon>();
    }

    public void changeAmmo(int value)
    {
        if (value == 0) { return; }

        if (value == -1)
            localCasperData.CurrentAmmo += value;
        else
            localCasperData.CurrentAmmo = value;

        if (localCasperData.CurrentAmmo > localCasperData.MaxAmmo)
            localCasperData.CurrentAmmo = localCasperData.MaxAmmo;

        if (localCasperData.CurrentAmmo < 0)
            localCasperData.CurrentAmmo = 0;

        onAmmoChange?.Invoke(localCasperData.CurrentAmmo);
    }

    public void FireEquippedGun(Vector3 target)
    {
        selectedWeapon = weaponInventory.GetSelectedWeapon().GetComponent<Weapon>();
        Vector3 difference = target - selectedWeapon.transform.position;
        Vector2 direction = difference.normalized; // distance;
        selectedWeapon.FireWeapon(direction);
    }
}
