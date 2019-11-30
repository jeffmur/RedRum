using UnityEngine;
public class EventManager : SceneSingleton<EventManager>
{
    public Casper casper;

    public delegate void onNotifyChangeDelegate(string notification);
    public event onNotifyChangeDelegate OnNotifyChange;

    public delegate void onHealthTriggerDelegate(int value);
    public event onHealthTriggerDelegate onHealthTrigger, onMaxHealthTrigger;

    public delegate void onAmmoChangeDelegate(int val);
    public event onAmmoChangeDelegate onAmmoChange;

    public delegate void onItemPickupTriggerDelegate(Item item);
    public event onItemPickupTriggerDelegate onItemPickupTrigger;

    public delegate void onItemActivateTriggerDelegate(ActivatedItem item);
    public event onItemActivateTriggerDelegate onItemActivateTrigger;

    protected override void Awake()
    {
        base.Awake();

        Casper.Instance.onHealthChange += TriggerHealthChange;
        Casper.Instance.weaponInventory.onWeaponUse += TriggerAmmoChange;
        Casper.Instance.onMaxHealthChange += TriggerMaxHealthChange;
        Casper.Instance.onItemPickup += TriggerItemPickup;
        Casper.Instance.onItemUse += TriggerItemActivate;
    }

    public void TriggerNotification(string notification)
    {
        OnNotifyChange?.Invoke(notification);
    }

    public void TriggerAmmoChange(int value)
    {
        onAmmoChange?.Invoke(value);
    }

    public void TriggerMaxHealthChange(int value)
    {
        onMaxHealthTrigger?.Invoke(value);
    }

    public void TriggerHealthChange(int CurentHealth)
    {
        onHealthTrigger?.Invoke(CurentHealth);
        FlashDamage();
    }

    private void TriggerItemPickup(Item item)
    {
        onItemPickupTrigger?.Invoke(item);
    }

    private void TriggerItemActivate(ActivatedItem item)
    {
        onItemActivateTrigger?.Invoke(item);
    }



    private float Timer;
    private bool FlashingBegan;
    private void FlashDamage()
    {
        FlashingBegan = true;
    }
    private void Update()
    {
        if (FlashingBegan)
        {
            Timer += Time.deltaTime;
            Casper.Instance.GetComponentInChildren<Light>().color = Color.red;
            Casper.Instance.GetComponentInChildren<Light>().range = 2f;
            Casper.Instance.GetComponentInChildren<Light>().intensity = 20f;

            if (Timer > .25f && Timer <= .5f)
            {
                Casper.Instance.GetComponentInChildren<Light>().intensity = 0f;
            }
            if (Timer > .5f)
            {
                Casper.Instance.GetComponentInChildren<Light>().intensity = 20f;
            }
            if (Timer > .75f)
            {
                Timer = 0f;
                FlashingBegan = false;
            }
        }
        else
        {
            Casper.Instance.GetComponentInChildren<Light>().intensity = 0f;
        }
    }
}
