using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public Sprite opened;
    public Sprite closed;
    private SpriteRenderer rend;
    private List<GameObject> items;
    public bool pickUp = false;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        items = GetComponentInParent<RoomManager>().Items;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (rend.sprite != opened)
        { 
            rend.sprite = opened;
            spawnRandomItem();
        }
            
    }

    private void spawnRandomItem()
    {
        int i = Random.Range(0, items.Count - 1);
        Instantiate(items[i], transform.position, Quaternion.identity);
    }
}
