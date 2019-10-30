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
        // Update MiniMap
        //gameManager.

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if doors are unlocked or open
        if (fromRoom.getStatus() >= 1)
        {
            switch (name)
            {
                case "Top_Door":
                    Debug.Log("Collided with " + collision.name + " at " + gameObject.name);
                    RoomSwap(nextRoom, "BOTTOM");
                    break;
                case "Bottom_Door":
                    Debug.Log("Collided with " + collision.name + " at " + gameObject.name);
                    RoomSwap(nextRoom, "TOP");
                    break;
                case "Right_Door":
                    Debug.Log("Collided with " + collision.name + " at " + gameObject.name);
                    RoomSwap(nextRoom, "LEFT");
                    break;
                case "Left_Door":
                    Debug.Log("Collided with " + collision.name + " at " + gameObject.name);
                    RoomSwap(nextRoom, "RIGHT");
                    break;
            }
            fromRoom.LockAll();
        }
    }
}