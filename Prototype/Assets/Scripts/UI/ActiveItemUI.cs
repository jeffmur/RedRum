using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ActiveItemUI : MonoBehaviour
{
    public Image itemImage, cooldownBar;
    private float cooldownTime, waitTime, cooldownTimeElapsed;
    private bool onCooldown = false;

    private void Update()
    {
        if (cooldownTimeElapsed > 0)
        {
            echoCooldown();
        }
        else
        {
            onCooldown = false;
        }
    }

    public void displayActiveItem(Item item)
    {
        if (item is ActivatedItem)
        {
            SpriteRenderer rend = item.GetComponent<SpriteRenderer>();
            itemImage.sprite = rend.sprite;
            ActivatedItem actItem = (ActivatedItem)item;
            cooldownTime = actItem.getCooldownDuration();
            waitTime = actItem.getEffectDuration();
        }
    }

    public void updateItemOnActivate(ActivatedItem item)
    {
        if (item.isOneTimeUse())
        {
            itemImage.sprite = null;
        }
        else if (!onCooldown)
        {
            cooldownBar.fillAmount = 0;
            StartCoroutine(initCooldownBar());
        }
    }

    private IEnumerator initCooldownBar()
    {
        yield return new WaitForSeconds(waitTime);
        cooldownTimeElapsed = cooldownTime;
        onCooldown = true;
    }

    private void echoCooldown()
    {
        cooldownTimeElapsed -= Time.deltaTime;
        float cdBarPercentage = cooldownTimeElapsed / cooldownTime;
        cooldownBar.fillAmount = 1 - cdBarPercentage;
    }
}