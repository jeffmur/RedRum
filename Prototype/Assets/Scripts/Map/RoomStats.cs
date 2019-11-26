using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomStats : MonoBehaviour
{
    //public Camera mCamera;
    public List<GameObject> mWalls;
    private float maxY;
    private float minY;
    private float maxX;
    private float minX;
    // Start is called before the first frame update
    void Start()
    {
        setWallDemensions();
      //  mCamera.ResetAspect();
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
    private void setWallDemensions()
    {
        for(int i = 0; i < mWalls.Count; i++)
        {
            string name = mWalls[i].name;
            switch (name)
            {
                case "left_wall":
                    minX = mWalls[0].transform.position.x;
                    break;
                case "right_wall":
                    maxX = mWalls[1].transform.position.x;
                    break;
                case "top_wall":
                    maxY = mWalls[2].transform.position.y;
                    break;
                case "bot_wall":
                    minY = mWalls[3].transform.position.y;
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


    public Vector2 getHeightVec()
    {
        return new Vector2(minY, maxY);
    }

    public Vector2 getWidthVec()
    {
        return new Vector2(minX, maxX);
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
        //mCamera.transform.position = new Vector3(target.x, target.y, -30f);
    }

    public Vector2 sendPlayerToDoor(string side)
    {
        Vector2 location = new Vector2(0,0);
        float tHeight = getHeightVec().x + getHeightVec().y;
        float tWidth = getWidthVec().x + getWidthVec().y;
        switch (side)
        {
            case "TOP":
                location = new Vector2(tWidth / 2, getHeightVec().y - 2f);
                break;
            case "BOTTOM":
                location = new Vector2(tWidth / 2, getHeightVec().x + 2f);
                break;
            case "RIGHT":
                location = new Vector2(getWidthVec().y - 2f, tHeight / 2);
                break;
            case "LEFT":
                location = new Vector2(getWidthVec().x + 2f, tHeight / 2);
                break;
        }
        return location;
    }
}
