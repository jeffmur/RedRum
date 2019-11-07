using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public Camera mCamera;
    public SizeController SIZE;

    // Start is called before the first frame update
    void Start()
    {
        SIZE = GetComponent<SizeController>();
        mCamera.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        adjustCamera();
        if(Input.GetKeyDown("m"))
            if(mCamera.gameObject.activeSelf)
                mCamera.gameObject.SetActive(false);
            else
                mCamera.gameObject.SetActive(true);

    }
    
    private void adjustCamera()
    {
        Vector2 min = new Vector2(SIZE.minX, 5);
        Vector2 max = new Vector2(SIZE.maxX, SIZE.minY);
        Vector2 target = (min + max) / 2f;

        mCamera.transform.position = new Vector3(target.x, target.y, -10f);
        switch (SIZE.Demensions[0])
        {
            case '5': // 5x5
                mCamera.orthographicSize = 30;
                break;
            case '6': // 6x6
                mCamera.orthographicSize = 35;
                break;
            default: // 4x4
                mCamera.orthographicSize = 25;
                break;
        }
        
    }
    /**
    public void setCamLocation()
    {
        #region Center Camera
        // min vector
        Vector2 min = new Vector2(minX, minY);
        // max vector
        Vector2 max = new Vector2(maxX, maxY);
        // center is exactly between both
        Vector2 target = (min + max) / 2f;
        // assign camera position
        mCamera.transform.position = new Vector3(target.x, target.y, -30f);
        #endregion
        #region Set Orthogonal
        // assign camera size
        float x = maxX - minX;
        float y = maxY - minY;
        // set camera zoom 
        if (miniTemp.childCount <= 12)
            mCamera.orthographicSize = miniTemp.childCount * 4f;
        else
        {
            // set size = largest difference
            if (x < y)
                mCamera.orthographicSize = y;
            else
                mCamera.orthographicSize = x;
        }
        #endregion
        #region MiniMap to Corner
        // Camera Info
        float h = 2f * mCamera.orthographicSize;
        float w = h * mCamera.aspect;
        Vector3 loc = mCamera.transform.position;

        // Bottom Left corner of mCamera in x,y,z
        Vector3 BL = new Vector3(loc.x - w / 2f, loc.y - h / 2f, loc.z);
        Vector2 mapEdge = new Vector2(minX, minY);

        // shifting values
        float sX = BL.x - mapEdge.x + 6f;
        float sY = BL.y - mapEdge.y + 6f;

        // shift camera to the left to be on window bound
        mCamera.transform.position = new Vector3(loc.x - sX, loc.y - sY, loc.z);
        #endregion
    }
    */
}
