using UnityEngine;

public class PoisonShroom : PassiveItem
{
    protected override void setItemInfo()
    {
        itemID = 1;
        itemName = "Poison Shroom";
        caption = "GOTY since 1976";
    }

    public override void modifyStats()
    {
        player.transform.localScale = new Vector3(0.8f, 0.8f, 1); //may affect weapon
        player.GetComponent<PlayerStats>().changeMaxHealth(-1);
    }
}
