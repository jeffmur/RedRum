using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Item
{
    private ElevatorBehavior EB;
    private Object elPrefab;
    private GameObject bossRoom;

    // Start is called before the first frame update
    void Start()
    {
        elPrefab = Resources.Load("Textures/Prefabs/elevator");
        bossRoom = GameObject.Find("Boss Pool");
        GameObject temp = Instantiate(elPrefab, bossRoom.transform) as GameObject;
        EB = temp.GetComponent<ElevatorBehavior>();
        EventManager.Instance.onItemPickup += showing;
    }

    private void showing()
    {
        EB.show();
        Camera.main.GetComponent<CameraShake>().ShakeCamera(10f, 10f);
        EventManager.Instance.onItemPickup -= showing;
    }
    protected override void setItemInfo()
    {
        itemID = 4;
        itemName = "Key";
        caption = "Level Complete";
    }
}
