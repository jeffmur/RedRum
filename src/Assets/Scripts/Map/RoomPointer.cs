﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public partial class RoomPointer : MonoBehaviour
{
    private MMController mm;
    public Dictionary<int, List<GameObject>> map;
    public DoorSystem fromRoom;
    public GameObject nextRoom;
    private GameObject mHero;
    private GameObject casperIcon;

    // Start is called before the first frame update
    void Start()
    {
        GameObject temp = GameObject.Find("Level Generator");
        if(temp)
            mm = temp.GetComponent<MMController>();

        mHero = GameObject.Find("Casper");
    }

    private void RoomSwap(GameObject nextLevel, string doorSide)
    {
        if(mm == null) { noMiniMapSwap(nextLevel, doorSide); return; }
        RoomStats next = nextLevel.GetComponent<RoomStats>();
        nextLevel.GetComponent<DoorSystem>().LockAll();
        foreach (GameObject room in mm.allRooms) // for all the rooms
        {
            if (Vector2.Distance(room.transform.position, mm.casperIcon.position) < 2f) // find the room I am in
            {
                if (room.GetComponent<Room>() != null) //if it is actually a room
                {
                    int index = room.GetComponent<Room>().RoomIndex;
                    next.GetComponent<RoomManager>().RoomIndex = index;
                    if (!room.GetComponent<Room>().isVisited && index != 0) //if the room is not visited
                    {
                        GlobalControl.Instance.savedPlayerData.roomsCleared += 1;
                        room.GetComponent<Room>().isVisited = true; //mark it is visited
                        next.GetComponent<RoomManager>().Initialize(); //make the enemies spawn
                        // Hide all (if any) items in new room
                        Transform real = next.transform;
                        foreach (Transform item in real)
                        {
                            // Check roomIndex matches - Hide all items
                            if(item.tag == "Item" || item.tag == "Weapon")
                            {
                                item.GetComponent<BoxCollider2D>().enabled = false;
                                item.GetComponent<SpriteRenderer>().enabled = false;
                            }
                        }
                    }
                    else // Visited Room (more likely to have items)
                    {
                        Transform real = next.transform;
                        foreach (Transform item in real)
                        {
                            if (item.tag == "Item" || item.tag == "Weapon")
                            {
                                if (index == item.GetComponent<RoomRegister>().RoomIndex)
                                {
                                    item.GetComponent<BoxCollider2D>().enabled = true;
                                    item.GetComponent<SpriteRenderer>().enabled = true;
                                }
                                else
                                {
                                    item.GetComponent<BoxCollider2D>().enabled = false;
                                    item.GetComponent<SpriteRenderer>().enabled = false;
                                }
                            }
                        }
                    }
                }
            }
        }
        // Move camera
        next.setCamLocation();
            // Move Hero
        mHero.transform.position = next.sendPlayerToDoor(doorSide);
    }
    // FOR LEVEL2
    private void noMiniMapSwap(GameObject nextLevel, string doorSide)
    {
        RoomStats next = nextLevel.GetComponent<RoomStats>();
        // Move camera
        next.setCamLocation();
        // Move Hero
        mHero.transform.position = next.sendPlayerToDoor(doorSide);
    }

    private void bufferSwitch(string roomDir, string heroDir)
    {
        // Move Player Icon First
        if (mm != null)
        {
            // Swap Method
            mm.moveMMCasper(heroDir);

            // Base Case
            if (fromRoom.gameObject.name == "Swap_1")
                nextRoom = GameObject.Find("Swap_2");
            if (fromRoom.gameObject.name == "Swap_2")
                nextRoom = GameObject.Find("Swap_1");

            //Player Icon ON Entry Room
            if (mm.casperIcon.position == mm.allRooms[0].transform.position)
                nextRoom = GameObject.Find("Welcome");

            // Player Icon On Boss Icon
            float dist = Vector2.Distance(mm.casperIcon.position, mm.bossIcon.position);
            if (dist <= 1f)
                nextRoom = GameObject.Find("Boss Pool");

        }

        // Re-locate real player location
        RoomSwap(nextRoom, roomDir);
        fromRoom.LockAll();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if door(s) are open
        if (fromRoom.getStatus() == 2 && collision.name == "Casper")
        {
            switch (name)
            {
                case "Top_Door":
                    if (!fromRoom.isOpen(name)) { break; }
                    bufferSwitch("BOTTOM", "UP");
                    break;
                case "Bottom_Door":
                    if (!fromRoom.isOpen(name)) { break; }
                    bufferSwitch("TOP", "DOWN");
                    break;
                case "Right_Door":
                    if (!fromRoom.isOpen(name)) { break; }
                    bufferSwitch("LEFT", "RIGHT");
                    break;
                case "Left_Door":
                    if (!fromRoom.isOpen(name)) { break; }
                    bufferSwitch("RIGHT", "LEFT");
                    break;
                case "Right_1":
                case "Right_2":
                    bufferSwitch("LEFT", "RIGHT");
                    break;
                case "Left_1":
                case "Left_2":
                    bufferSwitch("RIGHT", "LEFT");
                    break;
            }
            //Debug.Log("Status is OPEN, " + fromRoom.name + " " + name);
            //Debug.Log("Going to room: " + nextRoom.name);
        }
    }
}
