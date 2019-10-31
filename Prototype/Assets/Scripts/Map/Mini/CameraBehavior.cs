using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public Camera mCamera;
    private Transform miniTemp;
    float maxX = -100f;
    float maxY = -100f;
    float minX = 100f;
    float minY = 100f;

    // Start is called before the first frame update
    void Start()
    {
        miniTemp = GameObject.Find("Mini Template").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        traverseChildren();
        setCamLocation();
    }

    private void traverseChildren()
    {
        foreach(Transform child in miniTemp)
        {
            if (child.position.x < minX)
                minX = child.position.x;
            else if (child.position.x > maxX)
                maxX = child.position.x;
            else if (child.position.y < minY)
                minY = child.position.y;
            else if (child.position.y > maxY)
                maxY = child.position.y;
        }
    }

    public void setCamLocation()
    {
        // min vector
        Vector2 min = new Vector2(minX, minY);
        // max vector
        Vector2 max = new Vector2(maxX, maxY);
        // center is exactly between both
        Vector2 target = (min + max) / 2f;
        // assign camera position
        mCamera.transform.position = new Vector3(target.x, target.y, -30f);
        // assign camera size
        float x = maxX - minX;
        float y = maxY - minY;
        // set camera zoom 
        if (x < y)
            mCamera.orthographicSize = y / 1.5f;
        else
        mCamera.orthographicSize = x / 1.5f;
        // Camera Info
        float h = 2f * mCamera.orthographicSize;
        float w = h * mCamera.aspect;
        Vector3 loc = mCamera.transform.position;

        // Bottom Left corner of mCamera in x,y,z
        Vector3 BL = new Vector3(loc.x - w / 2f, loc.y - h / 2f, loc.z);
        Vector2 mapEdge = new Vector2(minX, minY);

        // shifting values
        float sX = BL.x - mapEdge.x + 5.5f;
        float sY = BL.y - mapEdge.y + 5.5f;

        // shift camera to the left to be on window bound
        mCamera.transform.position = new Vector3(loc.x - sX, loc.y - sY, loc.z);
    }
}
