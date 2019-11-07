using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public Camera mCamera;
    private SizeController SIZE;

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
   
}
