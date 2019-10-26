using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Organize list automatically?
public class RoomStats : MonoBehaviour
{
    public Camera mCamera;
    public List<GameObject> mWalls;
    private float maxY;
    private float minY;
    private float maxX;
    private float minX;
    // Start is called before the first frame update
    void Start()
    {
        setWallDemensions();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /**
     * ASSUMPTION:
     * List of Walls must in order as follows:
     * Left
     * Right
     * Top
     * Bottom
     */
    private void setWallDemensions()
    {
        // Left - Min X
        minX = mWalls[0].transform.position.x;
        // Right - Max X
        maxX = mWalls[1].transform.position.x;
        // Top - Max Y
        maxY = mWalls[2].transform.position.y;
        // Bottom - Min Y
        minY = mWalls[3].transform.position.y;

    }
    public Vector2 getHeightDem()
    {
        return new Vector2(minY, maxY);
    }

    public Vector2 getWidthDem()
    {
        return new Vector2(minX, maxX);
    }

    public void setCamLocation()
    {
        float x = transform.position.x;
        float y = transform.position.y;
        mCamera.transform.position = new Vector3(x, y, -10);
    }

    public Vector2 sendPlayerToDoor(string side)
    {
        Vector2 location = new Vector2(0,0);
        float tHeight = getHeightDem().x + getHeightDem().y;
        float tWidth = getWidthDem().x + getWidthDem().y;
        switch (side)
        {
            case "TOP":
                location = new Vector2(tWidth / 2, getHeightDem().y);
                break;
            case "BOTTOM":
                location = new Vector2(tWidth / 2, getHeightDem().x + 1f);
                break;
            case "RIGHT":
                location = new Vector2(getWidthDem().y, tHeight / 2);
                break;
            case "LEFT":
                location = new Vector2(getWidthDem().x, tHeight / 2);
                break;
        }
        return location;
    }
}
