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
        mCamera.orthographicSize = miniTemp.childCount * 2.5f;
    }
}
