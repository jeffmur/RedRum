using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    protected int itemID;
    protected string itemName;
    protected string caption;
    protected bool alreadySpawned = false;
    protected GameObject player;

    protected virtual void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        tag = "Item";
        itemName = "UNTITLED ITEMNAME";
        caption = "UNTITLED CAPTION";
        gameObject.AddComponent<BoxCollider2D>();
        GetComponent<BoxCollider2D>().isTrigger = true;

    }

    public virtual void process()
    {
        string message = itemName + "\n" + caption;
        GameWorld.triggerNotification(message);
        gameObject.SetActive(false);
    }

    public void triggerSpawn()
    {
        alreadySpawned = true;
    }

    public bool getAlreadySpawned() { return alreadySpawned; }
}