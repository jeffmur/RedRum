using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysteryPotion : ActivatedItem
{
    private bool crRunning = false;

    protected override void doItemEffect()
    {
        if (!crRunning)
        {
            int effectIndex = Random.Range(0, 4);
            switch(effectIndex)
            {
                case (0):
                    StartCoroutine(turnInvisible());
                    break;
                case (1):
                    StartCoroutine(sonicSpeed());
                    break;
                case (2):
                    StartCoroutine(bigBoy());
                    break;
                case (3):
                    StartCoroutine(smallBoi());
                    break;
            }
        }
    }

    protected override void endItemEffect()
    {
        return;
    }

    protected override void setActivateItemBehavior()
    {
        return;
    }

    protected override void setItemDurations()
    {
        effectDuration = 180;
        cooldownDuration = 0;
    }

    private IEnumerator turnInvisible()
    {
        crRunning = true;
        stats.IsEtherial = true;
        Color blah = player.GetComponent<SpriteRenderer>().color;
        blah.a = 0.25f;
        player.GetComponent<SpriteRenderer>().color = blah;

        yield return new WaitForSecondsRealtime(Random.Range(2, 6));

        stats.IsEtherial = false;
        Color color = player.GetComponent<SpriteRenderer>().color;
        color.a = 1f;
        player.GetComponent<SpriteRenderer>().color = color;
        crRunning = false;
    }

    private IEnumerator sonicSpeed()
    {
        crRunning = true;
        float prevSpeed = stats.MoveSpeed;
        stats.MoveSpeed = 20;
        yield return new WaitForSecondsRealtime(Random.Range(2, 6));
        stats.MoveSpeed = prevSpeed;
        crRunning = false;
    }

    private IEnumerator bigBoy()
    {
        crRunning = true;
        Vector3 prevSize = player.transform.localScale;
        player.transform.localScale *= 3;
        yield return new WaitForSecondsRealtime(Random.Range(2, 6));
        player.transform.localScale = prevSize;
        crRunning = false;
    }

    private IEnumerator smallBoi()
    {
        crRunning = true;
        Vector3 prevSize = player.transform.localScale;
        player.transform.localScale /= 3;
        yield return new WaitForSecondsRealtime(Random.Range(2, 6));
        player.transform.localScale = prevSize;
        crRunning = false;
    }

    protected override void setItemInfo()
    {
        itemName = "Potion of Mysteries";
        itemID = 8;
        caption = "My greatest creation";
    }
}
