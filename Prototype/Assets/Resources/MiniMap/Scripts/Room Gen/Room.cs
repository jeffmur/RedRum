using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {

    public int roomType;
    public bool isVisited = false;
    public Vector2 roomPosition;
    // Start is called before the first frame update
    void Start()
    {
        //roomPosition = gameObject.transform.position; //once it is created it stores the value of where it is located
    }
    public void HasBeenVisited()
    {
        isVisited = true;
    }
    public bool AlreadyVisited()
    {
        return isVisited;
    }
    public void RoomDestruction() {

        Destroy(gameObject);
    }
}
