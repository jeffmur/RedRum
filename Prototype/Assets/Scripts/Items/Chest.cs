using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public Sprite opened;
    public Sprite closed;
    private SpriteRenderer rend;
    private List<GameObject> myItems;
    public bool openChest = false;
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
        if(Vector2.Distance(transform.position, casper.position) <= 3f && Input.GetKeyDown("e")) { openChest = true;  }
        if(openChest && rend.sprite != opened)
        {
            rend.sprite = opened;
            spawnRandomItem();
        }
    }

    public void initChest(List<GameObject> items) { myItems = items; gameObject.SetActive(false); }

    // NEED TO SAVE STATE || Access saved state
    public void reStock()
    {
        if(rend.sprite == closed || !openChest) { return; }
        rend.sprite = closed;
        openChest = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (rend.sprite != opened)
        {
            openChest = true;
        }
        else
            openChest = false;
            
    }

    private void spawnRandomItem()
    {
        int i = Random.Range(0, myItems.Count - 1);
        GameObject item = Instantiate(myItems[i], transform.position, Quaternion.identity);
        item.AddComponent<ItemBehavior>();
    }
}
