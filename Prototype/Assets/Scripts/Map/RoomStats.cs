using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomStats : MonoBehaviour
{
    private Camera mCamera;
    public List<GameObject> mDoors;
    private float maxY;
    private float minY;
    private float maxX;
    private float minX;
    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform folder in this.transform)
        {
            if (folder.name == "Doors")
                foreach (Transform doors in folder)
                    mDoors.Add(doors.gameObject);
        }
        mCamera = Camera.main;
        setDemensions();
        mCamera.ResetAspect();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public Vector2 spawnEnemyInBounds(Vector2 loc)
    {
        // Is within bounds
        if (!isInRoom(loc))
        {
            // Check each side
            if (maxX <= loc.x)
            {
                float offset = loc.x - maxX - 2;
                return new Vector2(loc.x - offset, loc.y);
            }
            if (minX >= loc.x || loc.x - minX <= 5)
            {
                float offset = minX - loc.x + 2;
                return new Vector2(loc.x + offset, loc.y);
            }
            if (maxY <= loc.x)
            {
                float offset = loc.y - maxY - 2;
                return new Vector2(loc.x, loc.y - offset);
            }
            if (minY >= loc.x)
            {
                float offset = minY - loc.y + 2;
                return new Vector2(loc.x, loc.y + offset);
            }
        }
        return loc;

        // See OnTriggerEnter2D for enemy overlap
    }

    public bool isInRoom(Vector2 location)
    {
        return maxX > location.x
            && minX < location.x
            && maxY > location.y
            && minY < location.y;
    }

    public Vector2 spawnOnSide(string side)
    {
        float distance = getHeightDem() * 2 + 30f;
        Vector2 myLoc = transform.position;
        Vector2 location = new Vector2(0, 0);
        switch (side)
        {
            case "TOP":
                location = new Vector2(myLoc.x, myLoc.y + distance);
                break;
            case "BOTTOM":
                location = new Vector2(myLoc.x, myLoc.y - distance);
                break;
            case "LEFT":
                location = new Vector2(myLoc.x - distance, myLoc.y);
                break;
            case "RIGHT":
                location = new Vector2(myLoc.x + distance, myLoc.y);
                break;
        }
        return location;
    }
    private void setDemensions()
    {
        for (int i = 0; i < mDoors.Count; i++)
        {
            string name = mDoors[i].name;
            switch (name)
            {
                case "Top_Door":
                    maxY = mDoors[i].transform.position.y;
                    break;
                case "Right_Door":
                    maxX = mDoors[i].transform.position.x;
                    break;
                case "Bottom_Door":
                    minY = mDoors[i].transform.position.y;
                    break;
                case "Left_Door":
                    minX = mDoors[i].transform.position.x;
                    break;
            }
        }
    }
    private float getHeightDem()
    {
        return maxY - minY;
    }

    private float getWidthDem()
    {
        return maxX - minX;
    }

    public void setCamLocation()
    {
        // min vector
        Vector2 x = new Vector2(minX, minY);
        // max vector
        Vector2 y = new Vector2(maxX, maxY);
        // center is exactly between both
        Vector2 target = (x + y) / 2f;
        // assign camera position
        mCamera.transform.position = new Vector3(target.x, target.y, -30f);
    }

    public Vector2 sendPlayerToDoor(string side)
    {
        Vector2 center = transform.position;
        switch (side)
        {
            case "TOP":
                center = new Vector2(center.x, maxY - 2f);
                break;
            case "BOTTOM":
                center = new Vector2(center.x, minY + 3f);
                break;
            case "RIGHT":
                center = new Vector2(maxX - 2f, center.y);
                break;
            case "LEFT":
                center = new Vector2(minX + 2f, center.y);
                break;
        }
        return center;
    }
}
