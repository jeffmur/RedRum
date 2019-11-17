public class AnxietyAttack : ActivatedItem
{

    protected override void setItemInfo()
    {
        itemName = "Anxiety Attack";
        itemID = 10;
        caption = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA";
    }
    protected override void setItemEffectBehavior()
    {
        print("???????");
    } 

    protected override void setItemDurations()
    {
        cooldownDuration = 3;
        effectDuration = 3;
    }
}
