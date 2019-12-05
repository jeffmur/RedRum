using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTester : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EventManager.Instance.onWeaponAdded += printonWeaponAdded;
        EventManager.Instance.onWeaponFired += printonWeaponFired;
        EventManager.Instance.onWeaponReloaded += printonWeaponReloaded;
        EventManager.Instance.onHealed += printonHealed;
        EventManager.Instance.onDamaged += printonDamaged;
        EventManager.Instance.onItemUse += printonItemUse;
        EventManager.Instance.onItemPickup += printonItemPickup;
    }

    void printonWeaponAdded()
    {
        //print("WEAPON ADDED");
    }
    void printonWeaponFired()
    {
        //print("WEAPON FIRED");
    }
    void printonWeaponReloaded()
    {
        //print("WEAPON RELOADED");
    }
    void printonHealed()
    {
        //print("CASPER HEALED");
    }
    void printonDamaged()
    {
       // print("CASPER HAS BOOBOO");
    }
    void printonItemUse()
    {
        print("ITEM USED");
    }
    void printonItemPickup()
    {
        print("ITEM PICKED UP");
    }
}
