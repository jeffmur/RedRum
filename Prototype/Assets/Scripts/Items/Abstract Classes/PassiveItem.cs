public abstract class PassiveItem : Item
{
    public override void process()
    {
        base.process();
        modifyStats();
        //player.GetComponent<PlayerStats>().passiveItems.Add(this);    
        Destroy(gameObject);
    }
    public abstract void modifyStats();
}

