using System;
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

        StartCoroutine(InitSound());
    }

    private IEnumerator InitSound()
    {      
        casperSounds.volume = 0;
        yield return new WaitForSeconds(1);
        casperSounds.volume = 1;
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
