using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInventory : MonoBehaviour
{
    public int selectedWeapon = 0;
    private GameObject selectedWeaponObject = null; 

    // Start is called before the first frame update
    void Start()
    {
        SelectWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        // weapon switch handling
        int previousWeapon = selectedWeapon;
        if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
        {
            if (selectedWeapon <= 0)
                selectedWeapon = transform.childCount - 1;
            else
                selectedWeapon--;
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forwards
        {
            if (selectedWeapon >= transform.childCount - 1)
                selectedWeapon = 0;
            else
                selectedWeapon++;
        }

        if (selectedWeapon != previousWeapon)
        {
            SelectWeapon();
        }
    }

    // function to be called when a weapon is picked up
    public void AddWeaponToInventory(GameObject weapon)
    {
        transform.parent = weapon.transform;
        SelectWeapon();
    }

    // returns selected weapon
    public GameObject GetSelectedWeapon()
    {
        return selectedWeaponObject;
    }

    private void SelectWeapon()
    {
        int i = 0;
        if (transform.childCount > 0) { 
            foreach (Transform weapon in transform)
            {
                if (i == selectedWeapon)
                {
                    weapon.gameObject.SetActive(true);
                    selectedWeaponObject = weapon.gameObject;
                }
                else
                    weapon.gameObject.SetActive(false);
                i++;
            }
        }
    }
}
