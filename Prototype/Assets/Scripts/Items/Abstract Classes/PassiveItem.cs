public abstract class PassiveItem : Item
{
    public override void process()
    {
        base.process();
        modifyStats();
        player.GetComponent<PlayerStats>().passiveItems.Add(this);    
    }
    public abstract void modifyStats();
}

