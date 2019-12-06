using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ActiveItemUI : MonoBehaviour
{
    public Image itemImage, cooldownBar;
    private float barIncreaseTime, barDecreaseTime;

    private void Update()
    {
        //if (cooldownTimeElapsed > 0)
        //{
        //    echoCooldown();
        //}
        //else
        //{
        //    onCooldown = false;
        //}
    }

    public void displayActiveItem(Item item)
    {
        if (item is ActivatedItem)
        {
            SpriteRenderer rend = item.GetComponent<SpriteRenderer>();
            itemImage = GetComponent<Image>();
            itemImage.sprite = rend.sprite;
            ActivatedItem actItem = (ActivatedItem)item;
            barIncreaseTime = actItem.getCooldownDuration();
            barDecreaseTime = actItem.getEffectDuration();
        }
    }

    public void updateItemOnActivate(ActivatedItem item)
    {
        if (item.isOneTimeUse())
        {
            itemImage.sprite = null;
        }
        else
        {
            StartCoroutine(StartCooldown());
        }
    }

    private IEnumerator StartCooldown()
    {
        float timer = barDecreaseTime;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            float cdBarPercentage = timer / barDecreaseTime;
            cooldownBar.fillAmount = cdBarPercentage;
            yield return null;
        }
        StartCoroutine(echoCooldown());
    }

    private IEnumerator echoCooldown()
    {
        float timer = barIncreaseTime;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            float cdBarPercentage = timer / barIncreaseTime;
            cooldownBar.fillAmount = 1 - cdBarPercentage;
            yield return null;
        }
    }
}