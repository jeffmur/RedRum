using UnityEngine;

public class PoisonShroom : PassiveItem
{
    protected override void setItemInfo()
    {
        itemID = 1;
        itemName = "Poison Shroom";
        caption = "A donkey's favorite food";
    }

    public override void modifyStats()
    {
        Vector3 ogScale = casper.transform.localScale;
        casper.transform.localScale = ogScale * 0.8f;
        foreach(Transform child in casper.transform)
        {
            child.localScale = ogScale / 8.0f;
        }
        casper.GetComponent<Casper>().changeMaxHealth(-1);
    }
}
