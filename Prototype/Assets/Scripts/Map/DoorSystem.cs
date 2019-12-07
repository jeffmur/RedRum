using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Example: Top_Door
 * Locked Door: Door Sprite w/ Planks
 * Unlocked Door: Door Sprite
 * Open Door: No Door Sprite
 * NOTE: Doors must be ordered in this way,
 *       Otherwise this script with FAIL
 */ 
public class DoorSystem : MonoBehaviour
{
    // Door STATES: 
    public bool LOCKED = true;      // 0
    public bool UNLOCKED = false;   // 1
    public bool OPEN = false;       // 2
    public List<GameObject> allDoors;
    private MMController sMMController;

    // Start is called before the first frame update
    void Start()
    {
        LockAll();
        GameObject temp = GameObject.Find("Level Generator");
        if(temp)
            sMMController = temp.GetComponent<MMController>();
    }
    
    /*
     * Door Status: 
     *  LOCKED  : 0
     *  UNLOCKED: 1
     *  OPEN    : 2
     */
    public int getStatus()
    {
        if (LOCKED)
            return 0;
        if (UNLOCKED)
            return 1;
        if (OPEN)
            return 2;
        return -1;
    }

    private static List<GameObject> GetAllChildren(GameObject mDoor)
    {
        List<GameObject> children = new List<GameObject>();
        for (int i = 0; i < mDoor.transform.childCount; i++)
            children.Add(mDoor.transform.GetChild(i).gameObject);
        return children;
    }

    private void UnlockDoor(GameObject mDoor)
    {
        List<GameObject> myChildren = GetAllChildren(mDoor);
        // show Door1
        myChildren[0].SetActive(false);
        // show Door2
        myChildren[1].SetActive(true);
        // hide Door3
        myChildren[2].SetActive(false);
    }

    private void LockDoor(GameObject mDoor)
    {
        List<GameObject> myChildren = GetAllChildren(mDoor);
        // show Door1
        myChildren[0].SetActive(true);
        // hide Door2
        myChildren[1].SetActive(false);
        // show Door3
        myChildren[2].SetActive(false);
    }

    private void OpenDoor(GameObject mDoor)
    {
        List<GameObject> myChildren = GetAllChildren(mDoor);
        // hide Door1, Door2 & Door 3
        myChildren[0].SetActive(false);
        myChildren[1].SetActive(false);
        myChildren[2].SetActive(false);
    }

    public void OpenAll()
    {
        OPEN = true;
        LOCKED = false;
        UNLOCKED = false;
        for (int i = 0; i < allDoors.Count; i++)
            if (isAvailable(i))
                OpenDoor(allDoors[i]);
    }

    public void LockAll()
    {
        LOCKED = true;
        UNLOCKED = false;
        OPEN = false;
        for (int i = 0; i < allDoors.Count; i++)
            if(allDoors[i] != null)
                LockDoor(allDoors[i]);
    }

    public void UnlockAll()
    {
        UNLOCKED = true;
        OPEN = false;
        LOCKED = false;
        for (int i = 0; i < allDoors.Count; i++)
            if(isAvailable(i))
                UnlockDoor(allDoors[i]);
    }
    
    private bool isAvailable(int index)
    {
        if (allDoors[index] == null) return false;
        if (sMMController == null) return false;
      //--------------------------------------------
        List<char> all = sMMController.availableDoors();
        if(all == null) { return false; }
        for(int j = 0; j < all.Count; j++)
        {
            if (allDoors[index].name[0] == all[j])
                return true;
        }
        return false;
    }

    /**
     * @param Top_door, Bottom_door, Right_door, Left_Door
     * @returns true/false
     * Iterates through allDoors & children
     *  If any child 0-3 are true, returned
     *  Otherwise, keeps getting set to false
     */
    public bool isOpen(string doorName)
    {
        bool open = true;
        foreach(GameObject door in allDoors)
        {
            if(doorName == door.name)
            {
                List<GameObject> myChildren = GetAllChildren(door);
                for (int i = 0; i < 3; i++)
                    open = myChildren[0].activeSelf;
            }
        }
        return open == false;
    }
}
