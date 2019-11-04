using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Example: Top_Door
 * Door1: Non-colored door (left side)
 * Door2: Green colored (Not active as default)
 * Door3: Red colored (Active Initally)
 * Backside: Black (opened)
 * Border: Frame of the door
 * NOTE: Doors must be ordered in this way,
 *       Otherwise this script with FAIL
 */ 
public class DoorSystem : MonoBehaviour
{
    // Door STATES: 
    private bool LOCKED = true;      // 0
    private bool UNLOCKED = false;   // 1
    private bool OPEN = false;       // 2
    public List<GameObject> allDoors;
    private MMController sMMController;

    // Start is called before the first frame update
    void Start()
    {
        LockAll();
        sMMController = GameObject.Find("GameManager").GetComponent<MMController>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    /*
     * Door Status: 
     *  LOCKED  : 0
     *  UNLOCKED: 1
     *  OPEN    : 2
     */
    public int getStatus()
    {
        if (UNLOCKED)
            return 1;
        if (OPEN)
            return 2;
        // most common status: LOCKED
        return 0;
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
        myChildren[0].SetActive(true);
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
        myChildren[2].SetActive(true);
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
        if (allDoors[index] == null)
            return false;
        List<char> all = sMMController.availableDoors();
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
