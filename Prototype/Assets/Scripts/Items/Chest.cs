using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public Sprite opened;
    public Sprite closed;
    private SpriteRenderer rend;
    public List<GameObject> myItems;
    public bool openChest = false;
    public int RoomIndex;
    private Transform casper;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        casper = GameObject.FindGameObjectWithTag("Player").transform;
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

    public void initChest(List<GameObject> items, int index) { 
        myItems = items; 
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
        if (myItems.Count == 0) return;

        int i = Random.Range(0, myItems.Count-1);

        if (myItems.Count == 1) i = 0;

        GameObject item = Instantiate(myItems[i], transform.position, Quaternion.identity);
        item.AddComponent<ItemBehavior>();
        item.AddComponent<RoomRegister>().RoomIndex = RoomIndex;
        item.transform.parent = gameObject.transform.parent;
    }
}
