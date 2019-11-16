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
        Vector3 ogScale = player.transform.localScale;
        player.transform.localScale = ogScale * 0.8f;
        foreach(Transform child in player.transform)
        {
            child.localScale = ogScale / 8.0f;
        }
        player.GetComponent<PlayerStats>().changeMaxHealth(-1);
    }
}
