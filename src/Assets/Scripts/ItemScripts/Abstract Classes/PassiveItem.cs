public abstract class PassiveItem : Item
{
    public override void process()
    {
        base.process();
        modifyStats();
        Destroy(gameObject);
    }
    public abstract void modifyStats();
}

