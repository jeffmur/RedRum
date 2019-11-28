using UnityEngine;
public class EventManager : SceneSingleton<EventManager>
{
    private GameObject player;
    private Casper casper;

    public delegate void onNotifyChangeDelegate(string notification);
    public static event onNotifyChangeDelegate OnNotifyChange;

    public delegate void onHealthTriggerDelegate(int value);
    public event onHealthTriggerDelegate onHealthTrigger, onMaxHealthTrigger;

    public delegate void onAmmoChangeDelegate(int val);
    public event onAmmoChangeDelegate onAmmoChange;

    public delegate void onItemPickupTriggerDelegate(Item item);
    public event onItemPickupTriggerDelegate onItemPickupTrigger;

    public delegate void onItemActivateTriggerDelegate(ActivatedItem item);
    public event onItemActivateTriggerDelegate onItemActivateTrigger;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        casper = player.GetComponent<Casper>();

        casper.onHealthChange += triggerHealthChange;
        casper.onAmmoChange += triggerAmmoChange;
        casper.onMaxHealthChange += triggerMaxHealthChange;
        casper.onItemPickup += triggerItemPickup;
        casper.onItemUse += triggerItemActivate;
    }

    public static void TriggerNotification(string notification)
    {
        OnNotifyChange?.Invoke(notification);
    }

    public void triggerAmmoChange(int value)
    {
        onAmmoChange?.Invoke(value);
    }

    public void triggerMaxHealthChange(int value)
    {
        onMaxHealthTrigger?.Invoke(value);
    }

    public void triggerHealthChange(int CurentHealth)
    {
        //Debug.Log("Changing health by " + CurentHealth);
        onHealthTrigger?.Invoke(CurentHealth);
        FlashDamage();
    }

    private void triggerItemPickup(Item item)
    {
        onItemPickupTrigger?.Invoke(item);
    }

    private void triggerItemActivate(ActivatedItem item)
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
            player.GetComponentInChildren<Light>().color = Color.red;
            player.GetComponentInChildren<Light>().range = 2f;
            player.GetComponentInChildren<Light>().intensity = 20f;

            if (Timer > .25f && Timer <= .5f)
            {
                player.GetComponentInChildren<Light>().intensity = 0f;
            }
            if (Timer > .5f)
            {
                player.GetComponentInChildren<Light>().intensity = 20f;
            }
            if(Timer > .75f)
            {
                Timer = 0f;
                FlashingBegan = false;
            }
        }
        else
        {
            player.GetComponentInChildren<Light>().intensity = 0f;
        }
    }
}
