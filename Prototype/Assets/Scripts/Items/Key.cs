using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Item
{
    private ElevatorBehavior el;
    private Object elPrefab;
    private GameObject bossRoom;

    // Start is called before the first frame update
    void Start()
    {
        elPrefab = Resources.Load("Textures/Prefabs/elevator");
        bossRoom = GameObject.Find("Boss Pool");
        GameObject temp = Instantiate(elPrefab, bossRoom.transform) as GameObject;
        el = temp.GetComponent<ElevatorBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            el.show();
            Camera.main.GetComponent<CameraShake>().ShakeCamera(10f, 10f);
        }
    }
    protected override void setItemInfo()
    {
        itemID = 4;
        itemName = "Key";
        caption = "Level Complete";
    }
}
