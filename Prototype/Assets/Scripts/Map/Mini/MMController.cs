using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMController : MonoBehaviour
{
    private Transform miniMap;
    private Transform playerIcon;
    private Transform entryRoom;
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
        if (!once && miniMap.GetComponent<RoomTemplates>().waitTime <= -1f)
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
        foreach (Transform child in folder)
        {
             miniRooms.Add(child);
        }
        playerIcon = miniRooms[0];
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
        Transform room = getCurrentRoom();
        if (room == entryRoom)
        {
            return new List<char> { 'T', 'B', 'R', 'L' };
        }
        return nameToList(room.name.Replace("(Clone)",""));
    }
    private Transform getCurrentRoom()
    {
        // Entry Room is not a child must be checked first
        if (playerIcon.position == entryRoom.position)
            return entryRoom;

        for (int i = 1; i < miniRooms.Count; i++)
        {
            float dist = Vector2.Distance(playerIcon.position, miniRooms[i].position);
            // Otherwise, check all children
            if (dist <= 1f && dist >= 0f)
                return miniRooms[i];
        }
        return null;
    }

    private List<char> nameToList(string roomName)
    {
        if (roomName.Length == 1)
            return new List<char> { roomName[0] };
        else
            return new List<char> { roomName[0], roomName[1] };
    }
}
