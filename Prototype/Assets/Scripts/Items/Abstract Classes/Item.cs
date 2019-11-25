using UnityEngine;

public abstract class Item : MonoBehaviour
{
    protected int itemID;
    protected string itemName;
    protected string caption;
    protected GameObject player;
    protected PlayerStats stats;

    protected GameObject FlotingPointPrefab;

    protected virtual void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        stats = player.GetComponent<PlayerStats>();
        setDefaultInfo();
        gameObject.AddComponent<BoxCollider2D>();
        GetComponent<BoxCollider2D>().isTrigger = true;
        setItemInfo();

    }

    public virtual void process()
    {
        string message = "Obtained: " + itemName + '\n' + '\n' + caption;
        EventManager.TriggerNotification(message);
        hideItem();
        GlobalControl.Instance.savedPlayerData.itemsPickedUp += 1;
    }

    protected abstract void setItemInfo();

    private void setDefaultInfo()
    {
        tag = "Item";
        itemName = "UNTITLED ITEMNAME";
        caption = "UNTITLED CAPTION";
    }

    public void hideItem()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }

    public void showItem()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }
}