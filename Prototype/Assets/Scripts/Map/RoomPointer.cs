using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public partial class RoomPointer : MonoBehaviour
{
    private GameWorld gameManager;
    public DoorSystem fromRoom;
    public GameObject nextRoom;
    public GameObject mHero;
    public Camera mCamera;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameWorld>();
        Debug.Assert(gameManager != null);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if doors are unlocked or open
        if (fromRoom.getStatus() >= 1)
        {
            switch (name)
            {
                case "Top_Door":
                    if (!fromRoom.isOpen(name)) { break; }
                    RoomSwap(nextRoom, "BOTTOM");
                    gameManager.GetComponent<MMController>().moveMMCasper("UP");
                    fromRoom.LockAll();
                    break;
                case "Bottom_Door":
                    if (!fromRoom.isOpen(name)) { break; }
                    RoomSwap(nextRoom, "TOP");
                    gameManager.GetComponent<MMController>().moveMMCasper("DOWN");
                    fromRoom.LockAll();
                    break;
                case "Right_Door":
                    if (!fromRoom.isOpen(name)) { break; }
                    RoomSwap(nextRoom, "LEFT");
                    gameManager.GetComponent<MMController>().moveMMCasper("RIGHT");
                    fromRoom.LockAll();
                    break;
                case "Left_Door":
                    if (!fromRoom.isOpen(name)) { break; }
                    RoomSwap(nextRoom, "RIGHT");
                    gameManager.GetComponent<MMController>().moveMMCasper("LEFT");
                    fromRoom.LockAll();
                    break;
            }
        }
    }
}