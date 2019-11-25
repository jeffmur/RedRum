﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public Sprite opened;
    public Sprite closed;
    private SpriteRenderer rend;
    public ItemManager itemManager;
    public bool openChest = false;
    public int RoomIndex;
    private Transform casper;
    private GameObject key;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        casper = GameObject.FindGameObjectWithTag("Player").transform;
        itemManager = GameObject.Find("ItemManager").GetComponent<ItemManager>();
        key = Resources.Load("Textures/Prefabs/Items/Key") as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //if(Vector2.Distance(transform.position, casper.position) <= 3f && Input.GetKeyDown("e")) { openChest = true;  }
        if(openChest && rend.sprite != opened)
        {
            rend.sprite = opened;
            GetComponent<BoxCollider2D>().isTrigger = true;
            GetComponentInParent<DoorSystem>().OpenAll();
            spawnRandomItem();
        }
    }

    public void initChest(int index) { 
        //myItems = items; 
        gameObject.SetActive(false); 
        openChest = false;
        RoomIndex = index;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (rend.sprite != opened)
        {
            openChest = true;
        }
            
    }

    private void spawnRandomItem()
    {
        bool gun = false;
        //if (myItems.Count == 0) return;

        //int i = Random.Range(0, myItems.Count-1);

        //if (myItems.Count == 1) i = 0;

        //Vector3 size = myItems[i].transform.localScale;

        //if (myItems[i].name == "RayGun")
        //    size = new Vector3(3, 3, 1); gun = true;
        //if (myItems[i].name == "Flashlight")
        //    size = new Vector3(2, 2, 1); gun = true;
        //if (myItems[i].name == "Shotgun")
        //    size = new Vector3(1, 1, 1); gun = true;
        GameObject item;
        if (transform.parent.name != "Boss Pool")
            item = Instantiate(itemManager.SpawnRandomItem(), transform.position, Quaternion.identity);
        else
            item = Instantiate(key, transform.position, Quaternion.identity);
        //item.transform.localScale = size;
        item.AddComponent<ItemBehavior>(); // rotate and "float up"
        item.AddComponent<RoomRegister>().RoomIndex = RoomIndex; // assigns item to roomIndex
        item.transform.parent = gameObject.transform.parent; // child of room

        if (gun)
            item.AddComponent<BoxCollider2D>().isTrigger = true;            
    }
}
