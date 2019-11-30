using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInventory : MonoBehaviour
{
    public int selectedWeapon = 0;
    private GameObject selectedWeaponObject = null;
    private bool[] CurrentInventory;

    // Start is called before the first frame update
    void Awake()
    {
        // NUMBER OF WEAPONS = 3
        //StartCoroutine(LateStart());
        StartCoroutine(LateStart());
    }

    IEnumerator LateStart()
    {
        yield return new WaitForSeconds(0.001f);
        CurrentInventory = GlobalControl.Instance.savedCasperData.WeaponInventory;
        setInitial(transform.childCount);
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

        if (selectedWeapon != previousWeapon && CurrentInventory[selectedWeapon])
        {
            SelectWeapon();
        }
    }

    // function to be called when a weapon is picked up
    public void AddWeaponToInventory(GameObject clone)
    {
        // Get char & destroy
        char picked = clone.name[0];
        Destroy(clone);
        setWeapon(true, picked);
        SelectWeapon();
        GlobalControl.Instance.savedCasperData.WeaponInventory = CurrentInventory;
    }

    private void setInitial(int size)
    {
        if(CurrentInventory == null)
            CurrentInventory = new bool[size];

        CurrentInventory[0] = true;
    }
    private void setWeapon(bool active, char first)
    {
        int i = 0;
        foreach (Transform wp in transform)
        {
            if (first == wp.name[0])
            {
                CurrentInventory[i] = active;
                selectedWeapon = i;
            }
                
            i++;
        }
            
    }

    public int numOfWeapons()
    {
        int r = 0;
        for (int i = 0; i < CurrentInventory.Length; i++)
            if (CurrentInventory[i] == true)
                r++;
        return r;
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
                    StartCoroutine(weapon.GetComponent<Weapon>().GetStats());
                    selectedWeaponObject = weapon.gameObject;
                }
                else
                    weapon.gameObject.SetActive(false);
                i++;
            }
        }
    }
}
