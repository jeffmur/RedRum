using UnityEngine;

public abstract class Item : MonoBehaviour
{
    protected int itemID;
    protected string itemName;
    protected string caption;
    private string hintPickUp;
    protected GameObject player;
    protected PlayerStats stats;

    protected GameObject FlotingPointPrefab;
    private float distanceToPlayer;
    protected virtual void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        stats = player.GetComponent<PlayerStats>();
        setDefaultInfo();
        gameObject.AddComponent<BoxCollider2D>();
        GetComponent<BoxCollider2D>().isTrigger = true;
        setItemInfo();

        FlotingPointPrefab = Resources.Load<GameObject>("UI/flotingText");
    }

    public virtual void Update()
    {
        distanceToPlayer = Vector2.Distance(player.transform.position, transform.position);
        if (distanceToPlayer < 1f)
        {
            if(GetComponent<SpriteRenderer>().enabled == true)
                showFloatingText();
        }

    }

    public virtual void process()
    {
        string message = "Obtained: " + itemName;
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
        hintPickUp = "Press E to pick up";
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

    public void showFloatingText()
    {
        Quaternion stay = new Quaternion(0, 0, 0, 0);
        var text = Instantiate(FlotingPointPrefab, transform.position, stay);

        text.transform.localScale = new Vector3(0.5f, 0.5f, 0);

        text.GetComponent<TMPro.TextMeshPro>().text = (hintPickUp);
        Destroy(text, .1f);
    }
}