using System.Collections;
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
    private GameObject key;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
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
        GameObject item;
        if (transform.parent.name != "Boss Pool")
            item = Instantiate(itemManager.SpawnRandomItem(), transform.position, Quaternion.identity);
        else
            item = Instantiate(key, transform.position, Quaternion.identity);

        item.AddComponent<ItemSpawnBehavior>(); // rotate and "float up"
        item.AddComponent<RoomRegister>().RoomIndex = RoomIndex; // assigns item to roomIndex
        item.transform.parent = gameObject.transform.parent; // child of room           
    }
}
