using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomPointer : MonoBehaviour
{
    private GameObject mDoors;
    public GameObject nextRoom;
    public GameObject mHero;
    public DoorSystem sDoors;
    public Camera mCamera;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    
    private void RoomSwap(GameObject nextLevel, string doorSide)
    {
        RoomStats next = nextLevel.GetComponent<RoomStats>();
        Vector2 height = next.getHeightDem();
        Vector2 width = next.getWidthDem();
        //Debug.Log("Height = " + height + "Width = " + width);
        // Move camera
        next.setCamLocation();
        // Move Hero
        mHero.transform.position = next.sendPlayerToDoor(doorSide);
        // Spawn enemies?

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if doors are unlocked or open
        if (sDoors.getStatus() >= 1)
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
            sDoors.LockAll();
        }
    }
}