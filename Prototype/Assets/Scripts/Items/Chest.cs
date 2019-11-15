using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public Sprite opened;
    public Sprite closed;
    private SpriteRenderer rend;
    private List<GameObject> items;
    public bool openChest = false;
    private Transform casper;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        items = GetComponentInParent<RoomManager>().Items;
        casper = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position, casper.position) <= 1f && Input.GetKeyDown("e"))
        {
            if (rend.sprite != opened)
            {
                rend.sprite = opened;
                spawnRandomItem();
            }
        }
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
        int i = Random.Range(0, items.Count - 1);
        Instantiate(items[i], transform.position, Quaternion.identity);
    }
}
