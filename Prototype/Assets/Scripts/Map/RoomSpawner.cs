using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public int openingDirection;
    // 1--> need Bottom Door
    // 2--> need Top Door
    // 3--> need Right Door
    // 4--> need Left Door

        
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(openingDirection == 1)
        {
            //need to spawn a room with a Bottom Door
        }else if(openingDirection == 2)
        {
            //need to spawn a room with a TOP door
        }
        else if (openingDirection == 3)
        {
            //need to spawn a room with a Left door
        }
         else if(openingDirection == 4)
        {
            //need to spawn a room with a Right door
        }
    }
}
