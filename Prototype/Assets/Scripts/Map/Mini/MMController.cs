using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMController : MonoBehaviour
{
    private Transform miniMap;
    public Transform playerIcon;
    public Transform bossIcon;
    private Transform entryRoom;
    public Transform closedRoom;
    public List<Transform> miniRooms;
    bool once = false;

    // Start is called before the first frame update
    void Start()
    {
        miniMap = GameObject.Find("Mini Template").GetComponent<Transform>();
        entryRoom = GameObject.Find("Entry Room").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        // waits until map is spawned and only called once
        if (!once && miniMap.GetComponent<RoomTemplates>().waitTime < -3f)
        {
            setUp(miniMap);
            once = true;
        }
        
    }
    public void updateMiniMap(string side)
    {
        switch (side)
        {
            case "TOP":
                //moveIcon(450f);
                break;
            case "BOTTOM":
                break;
            case "LEFT":
                break;
            case "RIGHT":
                break;
        }
    }

    public void setUp(Transform folder)
    {
        foreach (Transform i in folder)
        {
            miniRooms.Add(i);
        }
        playerIcon = miniRooms[0];
        bossIcon = miniRooms[miniRooms.Count - 1];
        manageDups();
    }

    private void manageDups()
    {
        foreach(Transform i in miniMap)
        {
            if(i == playerIcon || i == bossIcon) { continue; }
            foreach(Transform j in miniMap)
            {
                if(j == playerIcon || j == bossIcon) { continue; }
                if(i.position == j.position && i != j)
                {
                    string iName = i.name.Replace("(Clone)", "");
                    string jName = j.name.Replace("(Clone)", "");
                    if (iName.Length == 1)
                        if (jName.Contains(jName))
                            continue;
                    if (jName.Length == 1)
                        if (iName.Contains(jName))
                            continue;

                    // Two Rooms overlapping (creates 3 doors)
                    hideWalls(i);
                    hideWalls(j);
                    Debug.Log(i.name +" & " + j.name +" are overlapping!");
                }                       
            }
        }
    }

    private void hideWalls(Transform room)
    {
        if (room != playerIcon || room != bossIcon)
        {
            Transform all = room.GetChild(0); // Walls (empty gameObject - "folder")
            string R = getCurrentRoomName();
            foreach (Transform wall in all)
            {
                // Walls are labeled L, R, T, B 
                // Hide walls that match the room description
                for (int i = 0; i < R.Length; i++)
                {
                    // Found matching & do not hide door openings
                    if (wall.name == R[i].ToString() && wall.name[0] != 'W')
                        wall.gameObject.SetActive(false);
                }
                
                
            }
        }
    }

    /**
     * Moves Casper Icon one room in direction
     * Like a chess piece
     * @param UP, DOWN, LEFT, RIGHT
     */
    public void moveMMCasper(string direction)
    {
        Transform casper = miniRooms[0]; // should be static
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

    public List<char> availableDoors()
    {
        return nameToList(getCurrentRoomName());
    }

    public string getCurrentRoomName()
    {
        // Entry Room is not a child must be checked first
        if (playerIcon.position == entryRoom.position)
            return "Entry Room";

        string result = "";
        for (int i = 1; i < miniRooms.Count; i++)
        {
            float dist = Vector2.Distance(playerIcon.position, miniRooms[i].position);
            // Otherwise, check all children
            if (dist <= 1f && dist >= 0f)
                result += miniRooms[i].name.Replace("(Clone)", "");
        }
        removeDuplicates(result);
        return result;
    }

    private List<char> nameToList(string roomName)
    {
        roomName.Replace("(Clone)", "");
        if(roomName == "Entry Room")
            return new List<char> { 'T', 'L', 'R', 'B' };
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

    private void removeDuplicates(string init)
    {
        for(int i = 0; i < init.Length; i++)
        {
            for(int j = 0; j < init.Length; j++)
            {
                if (init[i] == init[j])
                    init.Remove(i);
            }
        }
    }
}
