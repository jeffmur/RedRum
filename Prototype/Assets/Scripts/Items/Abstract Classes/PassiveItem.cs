public abstract class PassiveItem : Item
{
    public override void process()
    {
        base.process();
        setItemInfo();
        modifyStats();
        player.GetComponent<PlayerStats>().passiveItems.Add(this);
         
    }
    public abstract void modifyStats();
}

