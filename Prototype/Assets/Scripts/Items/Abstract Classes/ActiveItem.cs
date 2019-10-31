public abstract class ActiveItem : Item
{
    protected float cooldownTimer;

    public abstract void activateItem();

    public override void process()
    {
        base.process();
        //player.
        //TODO: set player's active item to this one
    }
}