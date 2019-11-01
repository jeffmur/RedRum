using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public partial class RoomPointer : MonoBehaviour
{
    private GameWorld gameManager;
    public DoorSystem fromRoom;
    public GameObject nextRoom;
    private GameObject mHero;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameWorld>();
        mHero = GameObject.Find("Casper");
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    private void RoomSwap(GameObject nextLevel, string doorSide)
    {
        RoomStats next = nextLevel.GetComponent<RoomStats>();
        // Move camera
        next.setCamLocation();
        // Move Hero
        mHero.transform.position = next.sendPlayerToDoor(doorSide);
    }

    private void bufferSwitch(string roomDir, string heroDir)
    {
        MMController mm = gameManager.GetComponent<MMController>();
        // Move Player Icon First
        mm.moveMMCasper(heroDir);

        // Player Icon ON Entry Room
        if (mm.getCurrentRoom().name == "Entry Room")
            nextRoom = GameObject.Find("Welcome");

        // Player Icon On Boss Icon
        float dist = Vector2.Distance(mm.playerIcon.position, mm.bossIcon.position);
        if (dist <= 1f)
            nextRoom = GameObject.Find("Boss Pool");

        // Re-locate real player location
        RoomSwap(nextRoom, roomDir);
        fromRoom.LockAll();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if door(s) are open
        if (fromRoom.getStatus() == 2)
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