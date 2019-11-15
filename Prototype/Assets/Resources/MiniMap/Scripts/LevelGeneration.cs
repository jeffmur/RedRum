using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour {

    public Transform[] startingPositions;
    private SizeController SIZE;
    public GameObject[] rooms;
    public GameObject extraRooms;
    public GameObject criticalPath;
    public Transform playerIcon;
    public Transform bossIcon;

    private int direction;
    public bool stopGeneration;
    private int downCounter;

    public float moveIncrement;
    private float timeBtwSpawn;
    public float startTimeBtwSpawn;

    public LayerMask whatIsRoom;
    

    private void Start()
    {
        // Initial starting position (0 - 4) ALWAYS
        int randStartingPos = Random.Range(0, startingPositions.Length);
        transform.position = startingPositions[randStartingPos].position;

        // Starting room & Player Icon
        Instantiate(rooms[1], transform.position, Quaternion.identity);
        Instantiate(playerIcon, transform.position, Quaternion.identity);
        
        // Set up Camera, Demensions (Scenes.cs)
        // @param 4x4, 5x5, 6x6
        SIZE = GetComponent<SizeController>();
        SIZE.setUp(Scenes.getDem());
        direction = Random.Range(1, 6);
    }

    private void Update()
    {
        // TODO: Change to demo scene for alpha Testing
        // Testing Level Generation
        if (Input.GetKeyDown("4"))
            Scenes.Load("Alpha", "4x4");
        if (Input.GetKeyDown("5"))
            Scenes.Load("Alpha", "5x5");
        if (Input.GetKeyDown("6"))
            Scenes.Load("Alpha", "6x6");

        if (timeBtwSpawn <= 0 && stopGeneration == false)
        {
            Move();
            timeBtwSpawn = startTimeBtwSpawn;
        }
        else {
            timeBtwSpawn -= Time.deltaTime;
        }
    }

    /** ROOM INDEX **
     * 0 --> closed
     * 1 --> LR
     * 2 --> LRB
     * 3 --> LRT
     * 4 --> LRBT
     */
    private void Move()
    {

        if (direction == 1 || direction == 2)
        { // Move right !
          
            if (transform.position.x < SIZE.maxX)
            {
                downCounter = 0;
                Vector2 pos = new Vector2(transform.position.x + moveIncrement, transform.position.y);
                transform.position = pos;

                int randRoom = Random.Range(1, 4);
                Instantiate(rooms[randRoom], transform.position, Quaternion.identity);
                // Makes sure the level generator doesn't move left !
                direction = Random.Range(1, 6);
                if (direction == 3)
                    direction = 1;

                else if (direction == 4)
                    direction = 5;
            }
            else 
                direction = 5;
        }
        else if (direction == 3 || direction == 4)
        { // Move left !
           
            if (transform.position.x > SIZE.minX)
            {
                downCounter = 0;
                Vector2 pos = new Vector2(transform.position.x - moveIncrement, transform.position.y);
                transform.position = pos;

                int randRoom = Random.Range(1, 4);
                Instantiate(rooms[randRoom], transform.position, Quaternion.identity);

                direction = Random.Range(3, 6);
            }
            else
                direction = 5;
           
        }
        else if (direction == 5)
        { // MoveDown
            downCounter++;
            if (transform.position.y > SIZE.minY)
            {
                // Now I must replace the room BEFORE going down with a room that has a DOWN opening, so type 3 or 5
                Collider2D previousRoom = Physics2D.OverlapCircle(transform.position, 1, whatIsRoom);

                if (previousRoom.GetComponent<Room>().roomType != 4 && previousRoom.GetComponent<Room>().roomType != 2)
                {

                    // My problem : if the level generation goes down TWICE in a row, there's a chance that the previous room is just 
                    // a LRB, meaning there's no TOP opening for the other room ! 

                    if (downCounter >= 2)
                    {
                        previousRoom.GetComponent<Room>().RoomDestruction();
                        Instantiate(rooms[4], transform.position, Quaternion.identity);
                    }
                    else
                    {
                        previousRoom.GetComponent<Room>().RoomDestruction();
                        int randRoomDownOpening = Random.Range(2, 5);
                        if (randRoomDownOpening == 3)
                        {
                            randRoomDownOpening = 2;
                        }
                        Instantiate(rooms[randRoomDownOpening], transform.position, Quaternion.identity);
                    }

                }
                
               
  
                Vector2 pos = new Vector2(transform.position.x, transform.position.y - moveIncrement);
                transform.position = pos;

                // Makes sure the room we drop into has a TOP opening !
                int randRoom = Random.Range(3, 5);
                Instantiate(rooms[randRoom], transform.position, Quaternion.identity);

                direction = Random.Range(1, 6);
            }
            else {
                Instantiate(bossIcon, transform.position, Quaternion.identity);
                stopGeneration = true;
            }
            
        }
    }
}
