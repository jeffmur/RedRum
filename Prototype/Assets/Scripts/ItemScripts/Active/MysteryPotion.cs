using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysteryPotion : ActivatedItem
{
    private bool crRunning = false;

    protected override void setItemInfo()
    {
        itemName = "Potion of Mysteries";
        itemID = 8;
        caption = "My greatest creation";
    }

    protected override void doItemEffect()
    {
        if (!crRunning)
        {
            int effectIndex = Random.Range(0, 5);
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
                case (4):
                    StartCoroutine(shiftRealities());
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
        casperData.changeMaxHealth(Random.Range(0, 3));
        casperData.MoveSpeed += Random.Range(0f, 0.5f);
        casperData.FireRate /= 1.2f;
    }

    protected override void setItemDurations()
    {
        effectDuration = 180;
        cooldownDuration = -1;
    }

    private IEnumerator turnInvisible()
    {
        crRunning = true;
        casperData.IsEtherial = true;
        yield return new WaitForSecondsRealtime(Random.Range(2, 6));
        casperData.IsEtherial = false;
        crRunning = false;
    }

    private IEnumerator sonicSpeed()
    {
        crRunning = true;
        float prevSpeed = casperData.MoveSpeed;
        casperData.MoveSpeed = 20;
        yield return new WaitForSecondsRealtime(Random.Range(2, 6));
        casperData.MoveSpeed = prevSpeed;
        crRunning = false;
    }

    private IEnumerator bigBoy()
    {
        crRunning = true;
        Vector3 prevSize = casper.transform.localScale;
        casper.transform.localScale *= 2;
        yield return new WaitForSecondsRealtime(Random.Range(2, 6));
        casper.transform.localScale = prevSize;
        crRunning = false;
    }

    private IEnumerator smallBoi()
    {
        crRunning = true;
        Vector3 prevSize = casper.transform.localScale;
        casper.transform.localScale /= 3;
        yield return new WaitForSecondsRealtime(Random.Range(2, 6));
        casper.transform.localScale = prevSize;
        crRunning = false;
    }

    private IEnumerator shiftRealities()
    {
        crRunning = true;
        float timer = (float)Random.Range(2, 6);
        while (timer > 0)
        {
            Vector3 rotation = new Vector3(Random.Range(-180f, 180f), Random.Range(-180f, 180f), Random.Range(-180f, 180f));
            casper.transform.eulerAngles = rotation;
            yield return new WaitForSeconds(.25f);
            timer -= .25f;
        }
        casper.transform.eulerAngles = Vector3.zero;
        crRunning = false;
    }
}
