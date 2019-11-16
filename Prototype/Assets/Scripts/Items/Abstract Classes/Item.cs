using UnityEngine;

public abstract class Item : MonoBehaviour
{
    protected int itemID;
    protected string itemName;
    protected string caption;
    protected GameObject player;
    protected PlayerStats stats;

    protected virtual void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        stats = player.GetComponent<PlayerStats>();
        tag = "Item";
        itemName = "UNTITLED ITEMNAME";
        caption = "UNTITLED CAPTION";
        gameObject.AddComponent<BoxCollider2D>();
        GetComponent<BoxCollider2D>().isTrigger = true;
        setItemInfo();
    }

    public virtual void process()
    {
        string message = itemName + "\n" + caption;
        EventManager.TriggerNotification(message);
        gameObject.SetActive(false);
    }

    protected abstract void setItemInfo();
}