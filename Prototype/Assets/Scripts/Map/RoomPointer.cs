﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public partial class RoomPointer : MonoBehaviour
{
    private MMController mm;
    public DoorSystem fromRoom;
    public GameObject nextRoom;
    private GameObject mHero;
    private GameObject casperIcon;


    // Start is called before the first frame update
    void Start()
    {
        mm = GameObject.Find("Level Generator").GetComponent<MMController>();
        mHero = GameObject.Find("Casper");
    }
    
    private void RoomSwap(GameObject nextLevel, string doorSide)
    {
        RoomStats next = nextLevel.GetComponent<RoomStats>();
        if (mm.casperIcon.position != mm.allRooms[0].transform.position)
        {
            foreach (GameObject room in mm.allRooms) // for all the rooms
            {
                if (Vector2.Distance(room.transform.position, mm.casperIcon.position) < 2f) // find the room I am in 
                {
                    if (room.GetComponent<Room>() != null) //if it is actually a room
                    {
                        if (!room.GetComponent<Room>().isVisited) //if the room is not visited
                        {
                            room.GetComponent<Room>().isVisited = true; //mark it is visited
                            next.GetComponent<RoomManager>().Initialize(); //make the enemies spawn 
                        }
                    }
                }
            }
        }
        next.GetComponent<DoorSystem>().LockAll(); //Spawn Enemies
        // Move camera
        next.setCamLocation();
            // Move Hero
        mHero.transform.position = next.sendPlayerToDoor(doorSide);
    }

    private void bufferSwitch(string roomDir, string heroDir)
    {
        // Move Player Icon First
        mm.moveMMCasper(heroDir);

        // Base Case
        if (fromRoom.gameObject.name == "Swap_1")
            nextRoom = GameObject.Find("Swap_2");
        if(fromRoom.gameObject.name == "Swap_2")
            nextRoom = GameObject.Find("Swap_1");

        // Player Icon ON Entry Room
        if (mm.casperIcon.position == mm.allRooms[0].transform.position)
            nextRoom = GameObject.Find("Welcome");

        // Player Icon On Boss Icon
        float dist = Vector2.Distance(mm.casperIcon.position, mm.bossIcon.position);
        if (dist <= 1f)
            nextRoom = GameObject.Find("Boss Pool");

        // Re-locate real player location
        RoomSwap(nextRoom, roomDir);
        fromRoom.LockAll();
    }

    private string oppositeDir(string room)
    {
        switch (room)
        {
            case "UP":
                return "DOWN";
            case "DOWN":
                return "UP";
            case "LEFT":
                return "RIGHT";
            case "RIGHT":
                return "LEFT";
        }
        return null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if door(s) are open
        if (fromRoom.getStatus() == 2 && collision.name == "Casper")
        {
            //Debug.Log("Status is OPEN, " + fromRoom.name + " " + name);
            //Debug.Log("Going to room: " + nextRoom.name);
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
            }
            
        }
    }
}