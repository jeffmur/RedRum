using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMController : MonoBehaviour
{
    // Setting up Rooms (placing them into child folders)
    bool once = false;
    public GameObject[] allRooms;
    public GameObject[] allIcons;

    // CHILD FOLDERS
    private GameObject criticalPath;
    private GameObject extraRooms;
    private GameObject icons;

    public Transform casperIcon;
    public Transform bossIcon;
    public Transform entryRoom;

    private SizeController SIZE;
    private List<Room> RSS; //saving the states of all rooms (visited and location)

    // Start is called before the first frame update
    void Start()
    {
        criticalPath = transform.GetChild(0).gameObject;
        extraRooms = transform.GetChild(1).gameObject;
        icons = transform.GetChild(2).gameObject;
        SIZE = GetComponent<SizeController>();
    }

    // Update is called once per frame
    void Update()
    {
        // waits until map is spawned and only called once
        if (!once && GetComponent<LevelGeneration>().stopGeneration)
        {
            // Add all dangling (not organized rooms) to Critical Path
            allRooms = GameObject.FindGameObjectsWithTag("Rooms");
            foreach (GameObject room in allRooms)
                room.transform.parent = criticalPath.transform;


            // All icons to Icons
            allIcons = GameObject.FindGameObjectsWithTag("Icon");
            foreach (GameObject icon in allIcons)
                icon.transform.parent = icons.transform;

            // Set Icons
            casperIcon = icons.transform.GetChild(0);
            bossIcon = icons.transform.GetChild(1);
            entryRoom = getRoom(casperIcon.position);

            // Only run once
            once = true;
        }
    }

    /**
     * @param Vector2 Location (usually casperIcon)
     * @returns Transform Room, otherwise null
     * Checks both critical path and extra based on location
     */
    public Transform getRoom(Vector2 atLocation)
    {
        Transform result = null;
        // In Critical Path
        foreach(Transform child in criticalPath.transform)
        {
            if (atLocation == (Vector2)child.position)
                result = child;
        }
        // In Extra Room
        if(result != null) { return result; }
        foreach (Transform child in extraRooms.transform)
        {
            if (atLocation == (Vector2)child.position)
                result = child;
        }
        // Not in a room??
        return result;
    }

    /**
     * @param Vector2 location (usually casperIcon)
     * @returns string roomName without (Clone)
     * Otherwise, empty if non-existant (out of bound)
     */
    public string getRoomName(Vector2 atLocation)
    {
        Transform room = getRoom(atLocation);
        if(room != null)
            return room.name.Replace("(Clone)", "");
        return "";
    }

    /**
     * Uses casperIcon location
     * Checks bounds of map
     * If greater than bounds then 
     * @return string without letter of bad dir
     */ 
    private string adjOutOfBounds(string roomName)
    {
        string result = roomName;
        // Top y == maxY
        if (casperIcon.transform.position.y >= SIZE.maxY)
            result = result.Replace("T", "");
        // Bottom y == minY
        if (casperIcon.transform.position.y <= SIZE.minY)
            result = result.Replace("B", "");
        // Left x == minX
        if (casperIcon.transform.position.x + 5 <= SIZE.minX)
            result = result.Replace("L", "");
        // Right x == maxX
        if (casperIcon.transform.position.x - 5 >= SIZE.maxX)
            result = result.Replace("R", "");
        // BOTH - YES
        return result;
    }

    /**
     * Checks all rooms around to see if they contain adj door (letter)
     * @param string roomName (me) and Vector2 location
     * @return adjusted string if true, otherwise remains unchanged
     */
    private string adjacencyCheck(string me, Vector2 atLoc)
    {
        string L = getRoomName(new Vector2(atLoc.x - 10, atLoc.y));
        string R = getRoomName(new Vector2(atLoc.x + 10, atLoc.y));
        string T = getRoomName(new Vector2(atLoc.x, atLoc.y + 10));
        string B = getRoomName(new Vector2(atLoc.x, atLoc.y - 10));

        if (!L.Contains("R") && L != "")
            me = me.Replace("L", "");
        if (!R.Contains("L") && R != "")
            me = me.Replace("R", "");
        if (!T.Contains("B") && T != "")
            me = me.Replace("T", "");
        if (!B.Contains("T") && B != "")
            me = me.Replace("B", "");

        return me;
    }
   /**
    * Moves Casper Icon one room in direction
    * Like a chess piece
    * @param UP, DOWN, LEFT, RIGHT
    */
    public void moveMMCasper(string direction)
    {
        Transform casper = casperIcon;
        float distance = 10f;
        Vector3 iconPos = casper.position;

        switch (direction)
        {
            case "UP":
                casper.position = new Vector3(iconPos.x, iconPos.y + distance, iconPos.z);
                break;
            case "DOWN":
                casper.position = new Vector3(iconPos.x, iconPos.y - distance, iconPos.z);
                break;
            case "LEFT":
                casper.position = new Vector3(iconPos.x - distance, iconPos.y, iconPos.z);
                break;
            case "RIGHT":
                casper.position = new Vector3(iconPos.x + distance, iconPos.y, iconPos.z);
                break;
        }
    }

    /**
     * @returns List of characters for door openings
     * Removes doors that lead to out of bounds
     * Or lead to rooms without adjacent door
     */
    public List<char> availableDoors()
    {
        if(casperIcon == null) { return null; }
        string roomName = getRoomName(casperIcon.position);

        // Border Check
        roomName = adjOutOfBounds(roomName);
        // Adjacent Check
        roomName = adjacencyCheck(roomName, casperIcon.position);

        return nameToList(roomName);
    }

    /**
     * @param string roomName
     * @returns <'T', 'B', 'R', 'L'> 
     * Used to open doors
     */
    private List<char> nameToList(string roomName)
    {
        roomName.Replace("(Clone)", "");
        switch (roomName.Length)
        {
            case 1:
                return new List<char> { roomName[0] };
            case 2:
                return new List<char> { roomName[0], roomName[1] };
            case 3:
                return new List<char> { roomName[0], roomName[1], roomName[2] };
            case 4:
                return new List<char> { roomName[0], roomName[1], roomName[2], roomName[3] };
            default:
                return null;
        }
    }
}
