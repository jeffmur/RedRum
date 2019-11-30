using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInventory : MonoBehaviour
{
    public int selectedWeaponIndex = 0;
    public List<Weapon> CurrentInventory;
    public Weapon startingWeapon;

    public delegate void onWeaponUseDelegate(int bulletsRemaining);
    public event onWeaponUseDelegate onWeaponUse;

    public delegate void weaponEventDelegate();
    public event onWeaponUseDelegate onWeaponFired, onWeaponReload;

    // Start is called before the first frame update
    private void Awake()
    {
        InitializeInventory();
    }

    // Update is called once per frame
    private void Update()
    {
        TrackSelectedWeapon();
    }

    private void TrackSelectedWeapon()
    {
        // weapon switch handling
        if (CurrentInventory.Count == 1)
        {
            return;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
        {
            if (selectedWeaponIndex <= 0)
                selectedWeaponIndex = CurrentInventory.Count - 1;
            else
                selectedWeaponIndex--;
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forwards
        {
            if (selectedWeaponIndex >= CurrentInventory.Count - 1)
                selectedWeaponIndex = 0;
            else
                selectedWeaponIndex++;
        }
        SetSelectedWeapon();
    }

    // function to be called when a weapon is picked up
    public void AddWeaponToInventory(Weapon weapon)
    {
        CurrentInventory.Add(weapon);
        Destroy(weapon.GetComponent<ItemSpawnBehavior>());
        weapon.onAmmoChange += TriggerAmmoChange;
        weapon.transform.parent = gameObject.transform;
        weapon.transform.localScale = new Vector3(weapon.equipScale, weapon.equipScale, 1);
        weapon.transform.eulerAngles = gameObject.transform.eulerAngles;
        selectedWeaponIndex = CurrentInventory.Count - 1;
        SetSelectedWeapon();
    }

    private void InitializeInventory()
    {
        if(CurrentInventory == null)
            CurrentInventory = new List<Weapon>();
        AddWeaponToInventory(startingWeapon);
        selectedWeaponIndex = 0;
    }

    // returns selected weapon
    public Weapon GetSelectedWeapon()
    {
        return CurrentInventory[selectedWeaponIndex];
    }

    private void SetSelectedWeapon()
    {

        for (int i = 0; i < CurrentInventory.Count; i++)
        {
            if (i != selectedWeaponIndex)
            { CurrentInventory[i].gameObject.SetActive(false); }
            else { CurrentInventory[i].gameObject.SetActive(true); }
        }
        onWeaponUse?.Invoke(GetSelectedWeapon().bulletsInClip);
    }

    public void TriggerAmmoChange()
    {
        onWeaponUse?.Invoke(GetSelectedWeapon().bulletsInClip);
    }
}
