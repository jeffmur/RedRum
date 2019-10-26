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
    public bool LOCKED = true;      // 0
    public bool UNLOCKED = false;   // 1
    public bool OPEN = false;       // 2
    public List<GameObject> allDoors;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //updateState(); // for interactive bool toggling
    }

    private void updateState()
    {
        if (LOCKED)
        {
            UNLOCKED = false;
            OPEN = false;
        }
        if (UNLOCKED)
        {
            OPEN = false;
            LOCKED = false;
        }
        if (OPEN)
        {
            LOCKED = false;
            UNLOCKED = false;
        }
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
            OpenDoor(allDoors[i]);
    }

    public void LockAll()
    {
        LOCKED = true;
        UNLOCKED = false;
        OPEN = false;
        for (int i = 0; i < allDoors.Count; i++)
            LockDoor(allDoors[i]);
    }

    public void UnlockAll()
    {
        UNLOCKED = true;
        OPEN = false;
        LOCKED = false;
        Debug.Log(gameObject.name + " UNLOCKED");
        for (int i = 0; i < allDoors.Count; i++)
            UnlockDoor(allDoors[i]);
    }
}
