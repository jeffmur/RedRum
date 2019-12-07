using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Casper_Sounds : MonoBehaviour
{
    public AudioSource casperSounds;
    public AudioClip casperHit, casperHeal, itemPickup;

    private void Start()
    {
        EventManager.Instance.onDamaged += DamagedSound;
        EventManager.Instance.onHealed += HealedSound;
        EventManager.Instance.onItemPickup += ItemPickupSound;
        EventManager.Instance.onWeaponAdded += WeaponPickupSound;
        EventManager.Instance.onWeaponReloaded += WeaponPickupSound;
    }

    private void DamagedSound()
    {
        casperSounds.clip = casperHit;
        casperSounds.Play();
    }

    private void HealedSound()
    {
        casperSounds.clip = casperHeal;
        casperSounds.Play();
    }

    private void ItemPickupSound()
    {
        casperSounds.clip = itemPickup;
        casperSounds.Play();
    }

    private void WeaponPickupSound()
    {
        casperSounds.clip = Casper.Instance.weaponInventory.GetSelectedWeapon().weaponSound;
        casperSounds.Play();
    }

}
